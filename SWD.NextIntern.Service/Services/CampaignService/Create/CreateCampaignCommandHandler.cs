using MediatR;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;

namespace SWD.NextIntern.Service.Services.CampaignService.Create
{
    public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, string>
    {
        private readonly ICampaignRepository _campaignRepository;

        public CreateCampaignCommandHandler(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<string> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            //cần repo university để tham chieu

            var campaign = new Campaign
            {
                CampaignName = request.CampaignName,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                UniversityId = request.UniversityId,
            };

            _campaignRepository.Add(campaign);

            return await _campaignRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Thanh cong!" : "That bai!";
        }
    }
}
