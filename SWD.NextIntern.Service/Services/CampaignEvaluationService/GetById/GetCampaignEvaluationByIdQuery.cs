using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.CampaignEvaluationService.GetById
{
    public class GetCampaignEvaluationByIdQuery : IRequest<ResponseObject<CampaignEvaluationDto>>, IQuery
    {
        public string Id {  get; set; }

        public GetCampaignEvaluationByIdQuery(string id)
        {
            Id = id;
        }
    }
}
