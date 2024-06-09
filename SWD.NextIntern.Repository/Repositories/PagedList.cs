using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Repositories.IRepositories;

namespace SWD.NextIntern.Repository.Repositories
{
    public class PagedList<T> : List<T>, IPagedResult<T>
    {
        public PagedList(IQueryable<T> source, int pageNo, int pageSize)
        {
            TotalCount = source.Count();
            PageCount = GetPageCount(pageSize, TotalCount);
            PageNo = pageNo;
            PageSize = pageSize;
            var skip = ((PageNo - 1) * PageSize);

            AddRange(
                source
                    .Skip(skip)
                    .Take(PageSize)
                    .ToList());
        }

        public PagedList(int totalCount, int pageNo, int pageSize, List<T> results)
        {
            TotalCount = totalCount;
            PageCount = GetPageCount(pageSize, TotalCount);
            PageNo = pageNo;
            PageSize = pageSize;
            AddRange(results);
        }

        public int TotalCount { get; private set; }
        public int PageCount { get; private set; }
        public int PageNo { get; private set; }
        public int PageSize { get; private set; }

        private int GetPageCount(int pageSize, int totalCount)
        {
            if (pageSize == 0)
            {
                return 0;
            }
            var remainder = totalCount % pageSize;
            return (totalCount / pageSize) + (remainder == 0 ? 0 : 1);
        }

        public static async Task<IPagedResult<T>> CreateAsync(
            IQueryable<T> source,
            int pageNo,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var count = await source.CountAsync(cancellationToken);
            var skip = ((pageNo - 1) * pageSize);

            var results = await source
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
            return new PagedList<T>(count, pageNo, pageSize, results);
        }
    }
}
