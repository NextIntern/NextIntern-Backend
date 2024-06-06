using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;
using SWD.NextIntern.Repository.IRepositories;


namespace SWD.NextIntern.Service.Auth.ForgotPassword
{
    public class ForgotPasswordCommandHandler
    {
        private readonly IDistributedCache _cache;
        private readonly IInternRepository _internRepository;
        private readonly IConfiguration _configuration;

        public ForgotPasswordCommandHandler(IDistributedCache cache, IInternRepository internRepository, IConfiguration configuration)
        {
            _cache = cache;
            _internRepository = internRepository;
            _configuration = configuration;
        }

        public async Task Handle(ForgotPasswordQuery request, CancellationToken cancellationToken)
        {
            if (_internRepository.FindAsync(i => i.Email.Equals(request.Email)) == null)
            {
                throw new Exception("Email is not registed.");
            }

            var otp = GenerateOTP();
            await _cache.SetStringAsync(request.Email, otp, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });

            string resetLink = $"nextintern://database.nextintern.tech/resetpassword?email={request.Email}";

            string emailBody = $"Your OTP is {otp}. Click the following link to reset your password: {resetLink}";

            await SendAsync(request.Email, "Reset Your Password", emailBody);

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
