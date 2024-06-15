using AutoMapper;
using SWD.NextIntern.Repository.Persistence;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace SWD.NextIntern.Repository
{
    public class UserRepository : RepositoryBase<User, User, AppDbContext>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
  }
}
