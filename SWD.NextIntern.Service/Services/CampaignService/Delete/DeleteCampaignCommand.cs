using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;

namespace SWD.NextIntern.Service.Services.CampaignService.Delete
{
    public class DeleteCampaignCommand : IRequest<string>, ICommand
    {
        public string Id { get; set; }

        public DeleteCampaignCommand(string id)
        {
            Id = id;
        }
    }
}
