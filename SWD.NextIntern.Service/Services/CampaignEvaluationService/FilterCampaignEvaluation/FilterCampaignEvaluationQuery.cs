using MediatR;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.CampaignEvaluationService.FilterCampaignEvaluation
{
    public class FilterCampaignEvaluationQuery : IRequest<ResponseObject<PagedListResponse<CampaignEvaluationDto>>>
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }

        public FilterCampaignEvaluationQuery(int pageSize, int pageNo)
        {
            PageSize = pageSize;
            PageNo = pageNo;
        }
    }
}
