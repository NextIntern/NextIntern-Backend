using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;

namespace SWD.NextIntern.Service.Services.Quartzs
{
    internal class UpdateInternStateJob : IJob
    {
        private readonly ILogger<UpdateCampaignStateJob> _logger;
        private readonly IUserRepository _userRepository;

        public UpdateInternStateJob(ILogger<UpdateCampaignStateJob> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogWarning("Updating Intern state...");

            var queryOptions = (IQueryable<User> query) =>
            {
                return query
                    .Include(u => u.Campaign);
            };

            var interns = await _userRepository.FindAllAsync(u => u.DeletedDate == null);

            foreach (var intern in interns)
            {
                if (intern.Campaign?.CampaignState != 0 && intern.State != 2)
                {
                    intern.State = 2; // Set state to completed
                    _userRepository.Update(intern);
                    _logger.LogInformation($"Updated Intern state with id {intern.UserId}");
                }
            }

            await _userRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
