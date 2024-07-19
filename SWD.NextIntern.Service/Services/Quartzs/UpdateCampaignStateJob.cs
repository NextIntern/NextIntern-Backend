using Microsoft.Extensions.Logging;
using Quartz;

namespace SWD.NextIntern.Service.Services.Quartzs
{
    public class UpdateCampaignStateJob : IJob
    {
        private readonly ILogger<UpdateCampaignStateJob> _logger;

        public UpdateCampaignStateJob(ILogger<UpdateCampaignStateJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogWarning("Update Campaign state...");

            return Task.CompletedTask;
        }
    }
}
