using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.InternEvaluationService.GetInternEvaluationById
{
    public class GetInternEvaluationByIdQuery : IRequest<ResponseObject<InternEvaluationDto>>, IQuery
    {
        public string Id { get; set; }

        public GetInternEvaluationByIdQuery(string id)
        {
            Id = id;
        }
    }
}
