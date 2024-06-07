using MediatR;
using SWD.NextIntern.Repository.Repositories.IRepositories;

namespace SWD.NextIntern.Service.Services.CampaignService.Delete
{
    public class DeleteCampaignCommandHandler : IRequestHandler<DeleteCampaignCommand, string>
    {
        private readonly ICampaignRepository _campaignRepository;

        public DeleteCampaignCommandHandler(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<string> Handle(DeleteCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaign = await _campaignRepository.FindAsync(c => c.CampaignId.ToString().Equals(request.Id), cancellationToken);
            if (campaign == null)
            {
                return $"Campaign voi id {request.Id} khong ton tai";
            }

            _campaignRepository.Remove(campaign);
            return await _campaignRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Thanh cong!" : "That bai!";
        }
    }
}
