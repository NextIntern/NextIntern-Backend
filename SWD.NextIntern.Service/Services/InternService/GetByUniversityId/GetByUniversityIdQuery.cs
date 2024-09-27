using MediatR;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.InternService.GetByUniversityId
{
    public class GetByUniversityIdQuery : IRequest<ResponseObject<PagedListResponse<InternDto>>>
    {
        public string UniversityId { get; set; }
        public int PageSize { get; set; }
        public int PageNo { get; set; }

        public GetByUniversityIdQuery(string universityId, int pageNo, int pageSize)
        {
            UniversityId = universityId;
            PageNo = pageNo;
            PageSize = pageSize;
        }
    }
}
