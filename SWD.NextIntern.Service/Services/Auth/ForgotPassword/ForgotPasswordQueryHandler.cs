using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;
using SWD.NextIntern.Repository.IRepositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using MediatR;


namespace SWD.NextIntern.Service.Auth.ForgotPassword
{
    public class ForgotPasswordQueryHandler : IRequestHandler<ForgotPasswordQuery, string>
    {
        private readonly IDistributedCache _cache;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public ForgotPasswordQueryHandler(IDistributedCache cache, IUserRepository userRepository, IConfiguration configuration)
        {
            _cache = cache;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<string> Handle(ForgotPasswordQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(i => request.Email.Equals(i.Email));
            if (user == null)
            {
                throw new Exception("Email is not registed.");
            }

            var otp = GenerateOTP();

            var cachedValue = await _cache.GetStringAsync(request.Email);
            if (cachedValue != null) await _cache.RemoveAsync(request.Email);

            await _cache.SetStringAsync(request.Email, otp, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });

            string resetLink = $"https://nextintern.tech/resetpassword";
            //string resetLink = $"https://api-gateway.nextintern.tech/api/v1/auth/resetpassword";
            //string resetLink = $"https://localhost:7205/api/v1/auth/resetpassword";

            string emailBody = $"Your OTP is {otp}. Click the following link to reset your password: {resetLink}";

            await SendAsync(request.Email, "Reset Your Password", emailBody);

            return "Success!";
        }

        private string GenerateOTP()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var otpData = new byte[4];
                rng.GetBytes(otpData);
                int otp = BitConverter.ToInt32(otpData, 0) % 1000000;
                return Math.Abs(otp).ToString("D6");
            }
        }

        public async Task SendAsync(string toEmail, string subject, string body)
        {
            var smtpClient = new SmtpClient(_configuration["Smtp:Host"])
            {
                Port = int.Parse(_configuration["Smtp:Port"]),
                Credentials = new NetworkCredential(_configuration["Smtp:Username"], _configuration["Smtp:Password"]),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["Smtp:FromEmail"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
