using NextIntern.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextIntern.Domain.IRepositories
{
    public interface IInternRepository : IEFRepository<Intern, Intern>
    {
    }
}
