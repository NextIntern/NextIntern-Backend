using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.CampaignEvaluationService.Delete
{
    public class DeleteCampaignEvaluationCommand : IRequest<ResponseObject<string>>, ICommand
    {
        public string Id { get; set; }

        public DeleteCampaignEvaluationCommand(string id)
        {
            Id = id;
        }
    }
}
