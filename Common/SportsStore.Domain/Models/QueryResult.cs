using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SportsStore.Domain.Models
{
    public class QueryResult<T> : IQueryable<T>
    {
        private readonly IQueryable<T> _Items;

        public int Page { get; }
        public int PageSize { get; }
        public int PagesCount { get; }

        public bool HasPreviousPage => Page > 0;
        public bool HasNextPage => Page < PagesCount;

        public QueryResult(IQueryable<T> query, QueryOptions Options = null)
        {
            Page = Options?.Page ?? 0;
            PageSize = Options?.Size ?? 10;
            PagesCount = (int)Math.Ceiling(query.Count() / (double)PageSize);
            _Items = Options?.Items(query) ?? query;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => _Items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) _Items).GetEnumerator();

        Type IQueryable.ElementType => _Items.ElementType;

        Expression IQueryable.Expression => _Items.Expression;

        IQueryProvider IQueryable.Provider => _Items.Provider;
    }

    public class QueryOptions
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 10;
        internal IQueryable<T> Items<T>(IQueryable<T> query) => query.Skip(Page * Size).Take(Size);
    }
}