using MediatR;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using System.Linq.Expressions;

namespace SWD.NextIntern.Service.Services.CampaignService.Update
{
    public class UpdateCampaignCommandHandler : IRequestHandler<UpdateCampaignCommand, string>
    {
        private readonly ICampaignRepository _campaignRepository;

        public UpdateCampaignCommandHandler(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<string> Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
        {
            Expression<Func<Campaign, bool>> queryFilter = (Campaign c) => c.CampaignId.ToString().Equals(request.Id) && c.DeletedDate == null;

            //check university is not null

            var campaign = await _campaignRepository.FindAsync(queryFilter, cancellationToken);

            if (campaign is null) return $"Campaign voi id {request.Id} khong ton tai";

            campaign.StartDate = request.StartDate;
            campaign.EndDate = request.EndDate;
            campaign.CampaignName = request.CampaignName;
            campaign.UniversityId = Guid.Parse(request.UniversityId);

            _campaignRepository.Update(campaign);

            return await _campaignRepository.UnitOfWork.SaveChangesAsync() > 0 ? "Thanh cong!" : "That bai!";
        }
    }
}
