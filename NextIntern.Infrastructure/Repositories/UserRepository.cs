using Microsoft.EntityFrameworkCore;
using NextIntern.Domain.Entities;
using NextIntern.Domain.IRepositories;
using NextIntern.Infrastructure.Persistence;

namespace NextIntern.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateInternAsync(Intern intern)
        {
            _context.Interns.Add(intern);
            await _context.SaveChangesAsync();
        }

        public async Task<Intern?> GetUserByUsernameAsync(string username)
        {
            return await _context.Interns.FirstOrDefaultAsync(u => u.Username == username);
        }
    }

}
