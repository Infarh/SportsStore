using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SportsStore.Interfaces.Base;

namespace SportsStore.Services
{
    internal class PagedEnumerable<T> : IPagedEnumerable<T>
    {
        private readonly IEnumerable<T> _Items;

        public int Page { get; }

        public int PageSize { get; }

        public PagedEnumerable(IEnumerable<T> Items, int Page, int PageSize)
        {
            _Items = Items ?? throw new ArgumentNullException();
            this.Page = Page;
            this.PageSize = PageSize;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var items = _Items;
            return items is IQueryable<T> query
                ? query.Skip(Page * PageSize).Take(PageSize).GetEnumerator()
                : items.Skip(Page * PageSize).Take(PageSize).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}