using AutoMapper;
using NextIntern.Infrastructure.Repositories;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Persistence;
using SWD.NextIntern.Repository.Repositories.IRepositories;

namespace SWD.NextIntern.Repository.Repositories
{
    public class InternRepository : RepositoryBase<Intern, Intern, AppDbContext>, IInternRepository
    {
        public InternRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
