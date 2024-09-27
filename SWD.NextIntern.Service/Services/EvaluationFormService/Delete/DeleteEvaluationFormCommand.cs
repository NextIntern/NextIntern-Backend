
using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.EvaluationFormService.Delete
{
    public class DeleteEvaluationFormCommand : IRequest<ResponseObject<string>>, ICommand
    {
        public string Id { get; set; }

        public DeleteEvaluationFormCommand(string id)
        {
            Id = id;
        }
    }
}
