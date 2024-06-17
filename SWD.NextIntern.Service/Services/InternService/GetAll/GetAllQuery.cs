using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.InternService.GetAll
{
    public class GetAllQuery : IRequest<ResponseObject<List<InternDto>>>, IQuery
    {
    }
}
