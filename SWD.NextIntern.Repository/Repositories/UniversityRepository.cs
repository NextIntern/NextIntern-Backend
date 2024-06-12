using AutoMapper;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Persistence;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Repository.Repositories
{
    public class UniversityRepository : RepositoryBase<University, University, AppDbContext>, IUniversityRepository
    {
        public UniversityRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
