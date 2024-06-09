using AutoMapper;

using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Persistence;
using SWD.NextIntern.Repository.Entities;
using System.Linq.Expressions;
using SWD.NextIntern.Repository.IRepositories;

namespace SWD.NextIntern.Repository
{
    public class InternRepository : RepositoryBase<User, User, AppDbContext>, IInternRepository
    {
        private readonly AppDbContext _context;

        public InternRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _context = dbContext;
        }

        public async Task<User> FindAsync(Expression<Func<User, bool>> predicate)
        {
            return await _context.Users.Include(i => i.Role).FirstOrDefaultAsync(predicate);
        }

        public async Task AddAsync(User intern)
        {
            await _context.Users.AddAsync(intern);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
  }
}
