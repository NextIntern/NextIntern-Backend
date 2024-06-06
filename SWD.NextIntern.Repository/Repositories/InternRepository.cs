using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NextIntern.Domain.Entities;
using NextIntern.Domain.IRepositories;
using NextIntern.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace NextIntern.Infrastructure.Repositories
{
    public class InternRepository : RepositoryBase<Intern, Intern, AppDbContext>, IInternRepository
    {
        private readonly AppDbContext _context;

        public InternRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _context = dbContext;
        }

        public async Task<Intern> FindAsync(Expression<Func<Intern, bool>> predicate)
        {
            return await _context.Interns.Include(i => i.Role).FirstOrDefaultAsync(predicate);
        }

        public async Task AddAsync(Intern intern)
        {
            await _context.Interns.AddAsync(intern);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
