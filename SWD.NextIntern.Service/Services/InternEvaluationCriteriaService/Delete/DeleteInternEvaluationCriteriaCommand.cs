
using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.InternEvaluationCriteriaService.Delete
{
    public class DeleteInternEvaluationCriteriaCommand : IRequest<ResponseObject<string>>, ICommand
    {
        public string Id { get; set; }

        public DeleteInternEvaluationCriteriaCommand(string id)
        {
            Id = id;
        }
    }
}
