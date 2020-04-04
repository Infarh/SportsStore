using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SportsStore.Interfaces.Base;

namespace SportsStore.Services
{
    internal class PagedItems<T> : IPagedItems<T>
    {
        private readonly IEnumerable<T> _Items;

        public PagedItems(IEnumerable<T> Items) => _Items = Items;

        public IPagedEnumerable<T> Page(int PageNumber, int PageSize) => new PagedEnumerable<T>(_Items, PageNumber, PageSize);

        public IEnumerable<IPagedEnumerable<T>> GetPages(int PageSize)
        {
            var total_count = _Items.Count();
            var pages_count = (int) Math.Ceiling(total_count / (double) PageSize);
            for (var page_number = 0; page_number < pages_count; page_number++)
                yield return Page(page_number, PageSize);
        }

        public IEnumerator<T> GetEnumerator() => _Items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) _Items).GetEnumerator();
    }
}
