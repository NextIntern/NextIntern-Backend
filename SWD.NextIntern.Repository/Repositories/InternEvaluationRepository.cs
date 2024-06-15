using AutoMapper;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Persistence;
using SWD.NextIntern.Repository.Repositories.IRepositories;

namespace SWD.NextIntern.Repository.Repositories
{
    public class InternEvaluationRepository : RepositoryBase<InternEvaluation, InternEvaluation, AppDbContext>, IInternEvaluationRepository
    {
        public InternEvaluationRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
