
using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.UniversityService.Delete
{
    public class DeleteUniversityCommand : IRequest<ResponseObject<string>>, ICommand
    {
        public string Id { get; set; }

        public DeleteUniversityCommand(string id)
        {
            Id = id;
        }
    }
}
