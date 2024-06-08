using Microsoft.Extensions.Caching.Distributed;
using SWD.NextIntern.Repository.IRepositories;
using SWD.NextIntern.Service.Auth.ForgotPassword;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Auth.ResetPassword
{
    public class ResetPasswordQueryHandler
    {
        private readonly IDistributedCache _cache;
        private readonly IInternRepository _internRepository;

        public ResetPasswordQueryHandler(IDistributedCache cache, IInternRepository internRepository)
        {
            _cache = cache;
            _internRepository = internRepository;
        }

        public async Task Handle(ResetPasswordQuery query, CancellationToken cancellationToken)
        {
            var storedOTP = await _cache.GetStringAsync(query.Email);
            if (storedOTP != query.OTP)
            {
                throw new Exception("Invalid or expired OTP.");
            }

            if (query.NewPassword != query.ConfirmPassword)
            {
                throw new Exception("Confirm password is not match.");
            }

            var user = await _internRepository.FindAsync(u => u.Email.Equals(query.Email));

            user.Password = BCrypt.Net.BCrypt.HashPassword(query.NewPassword);

            await _internRepository.SaveChangesAsync();

            await _cache.RemoveAsync(query.Email);
        }
    }
}
