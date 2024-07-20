using MediatR;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.CampaignService.GetByUniversityId
{
    public class GetCampaignByUniversityIdQuery : IRequest<ResponseObject<PagedListResponse<CampaignDto>>>
    {
        public int PageSize { get; set; }
        public int PageNo { get; set; }
        public string UniversityId { get; set; }

        public GetCampaignByUniversityIdQuery(int pageSize, int pageNo, string universityId)
        {
            PageSize = pageSize;
            PageNo = pageNo;
            UniversityId = universityId;
        }
    }
}
