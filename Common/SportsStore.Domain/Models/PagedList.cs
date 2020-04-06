using System;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Domain.Models
{
    public class PagedList<T> : List<T>
    {
        public int Page { get; }
        public int PageSize { get; set; }
        public int PagesCount { get; }

        public bool HasPreviousPage => Page > 0;
        public bool HasNextPage => Page < PagesCount;

        public PagedList(IQueryable<T> query, PageOptions Options)
        {
            this.Page = Options.Page;
            PageSize = Options.Size;
            PagesCount = (int) Math.Ceiling(query.Count() / (double)PageSize);
            AddRange(Options.Items(query));
        }
    }

    public class PageOptions
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 10;
        public int PreviousPagesItemsCount => Page * Size;
        internal IQueryable<T> Items<T>(IQueryable<T> query) => query.Skip(PreviousPagesItemsCount).Take(Size);
    }
}
