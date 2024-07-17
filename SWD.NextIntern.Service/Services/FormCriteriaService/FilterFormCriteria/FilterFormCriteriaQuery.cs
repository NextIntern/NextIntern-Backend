using MediatR;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.FormCriteriaService.FilterFormCriteria
{
    public class FilterFormCriteriaQuery : IRequest<ResponseObject<PagedListResponse<FormCriteriaDto>>>
    {
        public int PageSize { get; set; }
        public int PageNo { get; set; }

        public FilterFormCriteriaQuery(int pageSize, int pageNo)
        {
            PageSize = pageSize;
            PageNo = pageNo;
        }
    }
}
