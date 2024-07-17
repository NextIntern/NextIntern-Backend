using MediatR;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.InternEvaluationService.FilterInternEvaluation
{
    public class FilterInternEvaluationQuery : IRequest<ResponseObject<PagedListResponse<InternEvaluationDto>>>
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }

        public FilterInternEvaluationQuery(int pageNo, int pageSize)
        {
            PageNo = pageNo;
            PageSize = pageSize;
        }
    }
}
