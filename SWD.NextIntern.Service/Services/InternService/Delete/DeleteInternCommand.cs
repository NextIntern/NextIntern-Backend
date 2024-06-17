
using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.InternService.Delete
{
    public class DeleteInternCommand : IRequest<ResponseObject<string>>, ICommand
    {
        public string Id { get; set; }

        public DeleteInternCommand(string id)
        {
            Id = id;
        }
    }
}
