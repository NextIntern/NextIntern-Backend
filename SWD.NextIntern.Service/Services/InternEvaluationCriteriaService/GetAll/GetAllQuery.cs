using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.InternEvaluationCriteriaService.GetAll
{
    public class GetAllQuery : IRequest<ResponseObject<List<InternEvaluationCriteriaDto>>>, IQuery
    {
    }
}
