using NextIntern.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NextIntern.Domain.IRepositories
{
    public interface IInternRepository : IEFRepository<Intern, Intern>
    {
        Task<Intern> FindAsync(Expression<Func<Intern, bool>> predicate);
        Task AddAsync(Intern intern);
        Task SaveChangesAsync();
    }
}
