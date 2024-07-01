using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.RoleService;

namespace SWD.NextIntern.Service.Services.RoleService.GetAll
{
    public class GetAllQuery : IRequest<ResponseObject<List<RoleDto>>>, IQuery
    {
    }
}
