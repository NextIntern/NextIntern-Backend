using AutoMapper;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Persistence;
using SWD.NextIntern.Repository.Repositories.IRepositories;

namespace SWD.NextIntern.Repository.Repositories
{
    public class EvaluationFormRepository : RepositoryBase<EvaluationForm, EvaluationForm, AppDbContext>, IEvaluationFormRepository
    {
        public EvaluationFormRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
