using SWD.NextIntern.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.DTOs.Responses
{
    public class PagedListResponse<T>
    {
        public PagedList<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageCount { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
}
