using AutoMapper;
using SWD.NextIntern.Repository.Persistence;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;

namespace SWD.NextIntern.Repository
{
    public class UserRepository : RepositoryBase<User, User, AppDbContext>, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _context = dbContext;
        }

        public async Task<User> FindAsync(Expression<Func<User, bool>> predicate)
        {
            return await _context.Users.Include(i => i.Role).FirstOrDefaultAsync(predicate);
        }

        //public async Task AddAsync(User intern)
        //{
        //    await _context.Users.AddAsync(intern);
        //}

        //public async Task SaveChangesAsync()
        //{
        //    await _context.SaveChangesAsync();
        //}
    }
}
