using SWD.NextIntern.Repository.Common;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Repository.Repositories.IRepositories
{
    public interface ICampaignRepository : IEFRepository<Campaign, Campaign>
    {
    }
}
