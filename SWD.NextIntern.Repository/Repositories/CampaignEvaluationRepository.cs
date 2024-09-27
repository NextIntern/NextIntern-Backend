using AutoMapper;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Persistence;
using SWD.NextIntern.Repository.Repositories.IRepositories;

namespace SWD.NextIntern.Repository.Repositories
{
    public class CampaignEvaluationRepository : RepositoryBase<CampaignEvaluation, CampaignEvaluation, AppDbContext>, ICampaignEvaluationRepository
    {
        public CampaignEvaluationRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
