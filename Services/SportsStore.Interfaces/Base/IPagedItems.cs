using System.Collections.Generic;

namespace SportsStore.Interfaces.Base
{
    public interface IPagedItems<out T> : IEnumerable<T>
    {
        IPagedEnumerable<T> Page((int Page, int Size) Options) => Page(Options.Page, Options.Size);

        IPagedEnumerable<T> Page(int PageNumber, int PageSize);

        IEnumerable<IPagedEnumerable<T>> GetPages(int PageSize);
    }
}