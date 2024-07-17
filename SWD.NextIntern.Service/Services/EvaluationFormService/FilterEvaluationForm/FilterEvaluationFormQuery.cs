using MediatR;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.EvaluationFormService.FilterEvaluationForm
{
    public class FilterEvaluationFormQuery : IRequest<ResponseObject<PagedListResponse<EvaluationFormDto>>>
    {
        public int PageSize { get; set; }
        public int PageNo { get; set; }

        public FilterEvaluationFormQuery(int pageSize, int pageNo)
        {
            PageSize = pageSize;
            PageNo = pageNo;
        }
    }
}
