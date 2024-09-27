using Microsoft.Extensions.Logging;
using Quartz;
using SWD.NextIntern.Repository.Repositories.IRepositories;

namespace SWD.NextIntern.Service.Services.Quartzs
{
    public class UpdateCampaignStateJob : IJob
    {
        private readonly ILogger<UpdateCampaignStateJob> _logger;
        private readonly ICampaignRepository _campaignRepository;

        public UpdateCampaignStateJob(ILogger<UpdateCampaignStateJob> logger, ICampaignRepository campaignRepository)
        {
            _logger = logger;
            _campaignRepository = campaignRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            //_logger.LogWarning("Updating Campaign state...");
            //var campaigns = await _campaignRepository.FindAllAsync(c => c.DeletedDate == null);

            //foreach (var campaign in campaigns)
            //{
            //    if (campaign.EndDate.HasValue && campaign.EndDate.Value.ToDateTime(TimeOnly.MinValue) >= DateTime.Now && campaign.CampaignState != 0)
            //    {
            //        campaign.CampaignState = 0; // Set state to Closed
            //        _campaignRepository.Update(campaign);
            //        _logger.LogInformation($"Updated Campaign state with id {campaign.CampaignId}");
            //    }
            //}

            //await _campaignRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
