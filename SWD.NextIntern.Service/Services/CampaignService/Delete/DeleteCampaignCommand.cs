using MediatR;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.CampaignService.Delete
{
    public class DeleteCampaignCommand : IRequest<ResponseObject<string>>, ICommand
    {
        public string Id { get; set; }

        public DeleteCampaignCommand(string id)
        {
            Id = id;
        }
    }
}
