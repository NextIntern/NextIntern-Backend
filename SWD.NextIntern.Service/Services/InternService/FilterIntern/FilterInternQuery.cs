using MediatR;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.InternService.FilterIntern
{
    public class FilterInternQuery : IRequest<ResponseObject<PagedListResponse<InternDto>>>
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }

        public FilterInternQuery(int pageNo, int pageSize)
        {
            PageNo = pageNo;
            PageSize = pageSize;
        }
    }
}
