using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SportsStore.Interfaces.Base;

namespace SportsStore.Services
{
    internal class PagedEnumerable<T> : IPagedEnumerable<T>
    {
        private readonly IQueryable<T> _Query;

        public int Page { get; }

        public int PageSize { get; }

        public PagedEnumerable(IQueryable<T> Query, int Page, int PageSize)
        {
            _Query = Query?.Skip(Page * PageSize).Take(PageSize) ?? throw new ArgumentNullException();
            this.Page = Page;
            this.PageSize = PageSize;
        }

        public IEnumerator<T> GetEnumerator() => _Query.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        Type IQueryable.ElementType => _Query.ElementType;

        Expression IQueryable.Expression => _Query.Expression;

        IQueryProvider IQueryable.Provider => _Query.Provider;
    }
}