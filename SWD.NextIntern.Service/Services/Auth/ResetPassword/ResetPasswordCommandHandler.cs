using Microsoft.Extensions.Caching.Distributed;
using SWD.NextIntern.Repository.IRepositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.Auth.ForgotPassword;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Auth.ResetPassword
{
    public class ResetPasswordCommandHandler
    {
        private readonly IDistributedCache _cache;
        private readonly IUserRepository _userRepository;

        public ResetPasswordCommandHandler(IDistributedCache cache, IUserRepository userRepository)
        {
            _cache = cache;
            _userRepository = userRepository;
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

            var user = await _userRepository.FindAsync(u => request.Email.Equals(u.Email));

            user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            await _cache.RemoveAsync(request.Email);
        }
    }
}
