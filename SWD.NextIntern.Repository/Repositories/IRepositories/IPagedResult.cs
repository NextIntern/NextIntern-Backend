namespace SWD.NextIntern.Repository.Repositories.IRepositories
{
    public interface IPagedResult<out T> : IEnumerable<T>
    {
        int TotalCount { get; }
        int PageCount { get; }
        int PageNo { get; }
        int PageSize { get; }
    }
}
