using MediatR;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.InternService.GetByUniversityId
{
    public class GetByUniversityIdQuery : IRequest<ResponseObject<List<InternDto>>>
    {
        public string UniversityId { get; set; }

        public GetByUniversityIdQuery(string universityId)
        {
            UniversityId = universityId;
        }
    }
}
