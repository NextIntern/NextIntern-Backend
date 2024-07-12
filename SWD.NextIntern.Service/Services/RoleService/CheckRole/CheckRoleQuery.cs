using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.RoleService;

namespace SWD.NextIntern.Service.Services.RoleService.GetAll
{
    public class CheckRoleQuery : IRequest<ResponseObject<string>>, IQuery
    {
        public CheckRoleQuery(string? token)
        {
            Token = token;
        }

        public string? Token { get;}
    }
}
