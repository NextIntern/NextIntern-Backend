using Microsoft.Extensions.Caching.Distributed;
using SWD.NextIntern.Repository.IRepositories;
using SWD.NextIntern.Service.Auth.ForgotPassword;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Auth.ResetPassword
{
    public class ResetPasswordCommandHandler
    {
        private readonly IDistributedCache _cache;
        private readonly IInternRepository _internRepository;

        public ResetPasswordCommandHandler(IDistributedCache cache, IInternRepository internRepository)
        {
            _cache = cache;
            _internRepository = internRepository;
        }

        public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var storedOTP = await _cache.GetStringAsync(request.Email);
            if (storedOTP != request.OTP)
            {
                throw new Exception("Invalid or expired OTP.");
            }

            if (request.NewPassword != request.ConfirmPassword)
            {
                throw new Exception("Confirm password is not match.");
            }

            var user = await _internRepository.FindAsync(u => u.Email.Equals(request.Email));

            user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

            await _internRepository.SaveChangesAsync();

            await _cache.RemoveAsync(request.Email);
        }
    }
}
