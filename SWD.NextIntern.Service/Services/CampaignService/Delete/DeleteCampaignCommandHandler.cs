using MediatR;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;
namespace SWD.NextIntern.Service.Services.CampaignService.Delete
{
    public class DeleteCampaignCommandHandler : IRequestHandler<DeleteCampaignCommand, ResponseObject<string>>
    {
        private readonly ICampaignRepository _campaignRepository;

        public DeleteCampaignCommandHandler(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<ResponseObject<string>> Handle(DeleteCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaign = await _campaignRepository.FindAsync(c => c.CampaignId.ToString().Equals(request.Id), cancellationToken);
            if (campaign == null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"Campaign with id {request.Id} does not exist!");
            }

            _campaignRepository.Remove(campaign);
            return await _campaignRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.OK, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
