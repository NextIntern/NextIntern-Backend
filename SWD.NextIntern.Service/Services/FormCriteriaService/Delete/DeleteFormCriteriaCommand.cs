
using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.FormCriteriaService.Delete
{
    public class DeleteFormCriteriaCommand : IRequest<ResponseObject<string>>, ICommand
    {
        public string Id { get; set; }

        public DeleteFormCriteriaCommand(string id)
        {
            Id = id;
        }
    }
}
