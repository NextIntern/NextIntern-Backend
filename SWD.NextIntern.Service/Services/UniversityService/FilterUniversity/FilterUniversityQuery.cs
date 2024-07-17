using MediatR;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.UniversityService.FilterUniversity
{
    public class FilterUniversityQuery : IRequest<ResponseObject<PagedListResponse<UniversityDto>>>
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }

        public FilterUniversityQuery(int pageNo, int pageSize)
        {
            PageNo = pageNo;
            PageSize = pageSize;
        }
    }
}
