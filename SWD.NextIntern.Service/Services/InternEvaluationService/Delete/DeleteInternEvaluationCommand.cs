using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.InternEvaluationService.Delete
{
    public class DeleteInternEvaluationCommand : IRequest<ResponseObject<string>>, ICommand
    {
        public string Id { get; set; }

        public DeleteInternEvaluationCommand(string id)
        {
            Id = id;
        }
    }
}
