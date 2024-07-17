using MediatR;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.CampaignService.FilterCampaign
{
    public class FilterCampaignQuery : IRequest<ResponseObject<PagedListResponse<CampaignDto>>>
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }

        public FilterCampaignQuery(int pageSize, int pageNo)
        {
            PageSize = pageSize;
            PageNo = pageNo;
        }
    }
}
