using MediatR;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.InternService.GetByCampaignId
{
    public class GetInternByCampaignIdQuery : IRequest<ResponseObject<PagedListResponse<InternDto>>>
    {
        public string CampaignId { get; set; }
        public int PageSize { get; set; }
        public int PageNo { get; set; }

        public GetInternByCampaignIdQuery(string campaignId, int pageNo, int pageSize)
        {
            CampaignId = campaignId;
            PageNo = pageNo;
            PageSize = pageSize;
        }
    }
}
