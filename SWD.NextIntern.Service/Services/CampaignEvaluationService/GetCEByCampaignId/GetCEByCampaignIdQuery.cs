using MediatR;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.CampaignEvaluationService.GetCEByCampaignId
{
    public class GetCEByCampaignIdQuery : IRequest<ResponseObject<PagedListResponse<CampaignEvaluationDto>>>
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string CampaignId { get; set; }

        public GetCEByCampaignIdQuery(int pageSize, int pageNo, string campaignId)
        {
            PageSize = pageSize;
            PageNo = pageNo;
            CampaignId = campaignId;
        }
    }
}
