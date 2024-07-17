using MediatR;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.InternEvaluationCriteriaService.FilterInternEvaluationCriteria
{
    public class FilterInternEvaluationCriteriaQuery : IRequest<ResponseObject<PagedListResponse<InternEvaluationCriteriaDto>>>
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }

        public FilterInternEvaluationCriteriaQuery(int pageNo, int pageSize)
        {
            PageNo = pageNo;
            PageSize = pageSize;
        }
    }
}
