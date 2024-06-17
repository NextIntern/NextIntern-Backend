using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.CampaignEvaluationService.GetAll
{
    public class GetAllCampaignEvaluationQuery : IRequest<ResponseObject<List<CampaignEvaluationDto>>>, IQuery
    {
    }
}
