using AutoMapper;
using NextIntern.Domain.Entities;
using NextIntern.Domain.IRepositories;
using NextIntern.Infrastructure.Persistence;

namespace NextIntern.Infrastructure.Repositories
{
    public class InternRepository : RepositoryBase<Intern, Intern, AppDbContext>, IInternRepository
    {
        public InternRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
