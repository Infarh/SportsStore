using System.Collections.Generic;

namespace SportsStore.Interfaces.Base
{
    public interface IPagedEnumerable<out T> : IEnumerable<T>
    {
        int Page { get; }

        int PageSize { get; }
    }
}