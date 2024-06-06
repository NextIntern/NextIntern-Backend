using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;

namespace SWD.NextIntern.Service.Services.CampaignService.GetAll
{
    public class GetAllQuery : IRequest<List<CampaignDto>>, IQuery
    {
    }
}
