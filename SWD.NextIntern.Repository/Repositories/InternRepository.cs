using AutoMapper;

using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Persistence;
using SWD.NextIntern.Repository.Entities;
using System.Linq.Expressions;
using SWD.NextIntern.Repository.IRepositories;

namespace SWD.NextIntern.Repository
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
