
using System.Linq.Expressions;

namespace SWD.NextIntern.Repository.Repositories.IRepositories
{
    public interface IInternRepository : IEFRepository<Intern, Intern>
    {
        Task<Intern> FindAsync(Expression<Func<Intern, bool>> predicate);
        Task AddAsync(Intern intern);
        Task SaveChangesAsync();
    }
}
