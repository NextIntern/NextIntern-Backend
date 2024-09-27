using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.DashboardService.Items
{
    public class ItemsQuery : IRequest<ResponseObject<List<ItemsDto>>>, IQuery
    {
    }
}
