using AutoMapper;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Persistence;
using SWD.NextIntern.Repository.Repositories.IRepositories;

namespace SWD.NextIntern.Repository.Repositories
{
    public class CampaignQuestionResponseRepository : RepositoryBase<CampaignQuestionResponse, CampaignQuestionResponse, AppDbContext>, ICampaignQuestionResponseRepository
    {
        public CampaignQuestionResponseRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
