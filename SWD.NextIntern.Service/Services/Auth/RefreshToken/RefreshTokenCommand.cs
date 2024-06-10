using MediatR;
using SWD.NextIntern.Service.DTOs.Responses;
using ICommand = SWD.NextIntern.Service.Common.Interfaces.ICommand;

namespace SWD.NextIntern.Service.Services.Auth.RefreshToken
{
    public class RefreshTokenCommand : IRequest<TokenResponse>, ICommand
    {
        public RefreshTokenCommand(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
