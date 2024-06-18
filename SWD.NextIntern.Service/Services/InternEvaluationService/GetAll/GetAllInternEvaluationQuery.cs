using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.InternEvaluationService.GetAllInternEvaluation
{
    public class GetAllInternEvaluationQuery : IRequest<ResponseObject<List<InternEvaluationDto>>>, IQuery
    {
    }
}
