using SWD.NextIntern.Repository.Entities;
using System.Linq.Expressions;

namespace SWD.NextIntern.Repository.IRepositories
{
    public interface IInternRepository : IEFRepository<Intern, Intern>
    {
        Task<Intern> FindAsync(Expression<Func<Intern, bool>> predicate);
        Task AddAsync(Intern intern);
        Task SaveChangesAsync();
    }
}
