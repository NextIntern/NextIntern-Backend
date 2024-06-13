using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Persistence;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using System.Linq.Expressions;

namespace SWD.NextIntern.Repository.Repositories
{
    public class RoleRepository : RepositoryBase<Role, Role, AppDbContext>, IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _context = dbContext;
        }

        public async Task<Role> FindAsync(Expression<Func<Role, bool>> predicate)
        {
            return await _context.Roles.Include(i => i.Users).FirstOrDefaultAsync(predicate);
        }
    }
}
