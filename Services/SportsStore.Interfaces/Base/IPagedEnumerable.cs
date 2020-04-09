using System.Linq;

namespace SportsStore.Interfaces.Base
{
    public interface IPagedEnumerable<out T> : IQueryable<T>
    {
        int Page { get; }

        int PageSize { get; }

        int PagesCount { get; }
    }
}