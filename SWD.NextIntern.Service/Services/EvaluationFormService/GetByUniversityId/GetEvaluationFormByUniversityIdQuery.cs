using MediatR;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.EvaluationFormService.GetByUniversityId
{
    public class GetEvaluationFormByUniversityIdQuery : IRequest<ResponseObject<PagedListResponse<EvaluationFormDto>>>
    {
        public string UniversityId { get; set; }
        public int PageSize { get; set; }
        public int PageNo { get; set; }

        public GetEvaluationFormByUniversityIdQuery(string universityId, int pageNo, int pageSize)
        {
            UniversityId = universityId;
            PageNo = pageNo;
            PageSize = pageSize;
        }
    }
}
