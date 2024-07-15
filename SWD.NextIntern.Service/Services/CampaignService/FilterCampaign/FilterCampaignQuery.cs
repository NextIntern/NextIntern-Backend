using MediatR;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.CampaignService.FilterCampaign
{
    public class FilterCampaignQuery : IRequest<ResponseObject<IPagedResult<CampaignDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public FilterCampaignQuery(int pageSize, int pageNumber)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
}
