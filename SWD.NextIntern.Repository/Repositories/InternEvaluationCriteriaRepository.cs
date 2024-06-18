using AutoMapper;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Persistence;
using SWD.NextIntern.Repository.Repositories.IRepositories;

namespace SWD.NextIntern.Repository.Repositories
{
    public class InternEvaluationCriteriaRepository : RepositoryBase<InternEvaluationCriterion, InternEvaluationCriterion, AppDbContext>, IInternEvaluationCriteriaRepository
    {
        public InternEvaluationCriteriaRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
