using SWD.NextIntern.Repository.Entities;

using System.Linq.Expressions;

namespace SWD.NextIntern.Repository.Repositories.IRepositories
{
    public interface IInternRepository : IEFRepository<User, User>
    {
        Task<User> FindAsync(Expression<Func<User, bool>> predicate);
        Task AddAsync(User user);
        Task SaveChangesAsync();
    }
}
