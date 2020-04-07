using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SportsStore.Domain.Models
{
    public class QueryResult<T> : IQueryable<T>
    {
        private readonly QueryOptions _Options;
        private readonly IQueryable<T> _Items;
        private readonly int _TotalItemsCount;

        private IQueryable<T> Items => _Items;

        public QueryOptions Options => _Options;

        public int Page => _Options?.Page ?? 0;
        public int PageSize => _Options?.Size ?? 10;
        public int PagesCount => (int) Math.Ceiling(_TotalItemsCount / (double) PageSize);

        public bool HasPreviousPage => Page > 0;
        public bool HasNextPage => Page < PagesCount;

        public QueryResult(IQueryable<T> query, QueryOptions Options = null) => 
            (_Items, _TotalItemsCount) = (_Options = Options)?.Items(query) ?? (query, query.Count());

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => Items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Items).GetEnumerator();

        Type IQueryable.ElementType => Items.ElementType;

        Expression IQueryable.Expression => Items.Expression;

        IQueryProvider IQueryable.Provider => Items.Provider;
    }

    public class QueryOptions
    {
        private static readonly MethodInfo __OrderBy = typeof(Queryable)
            .GetMethods()
            .Single(method => method.Name == nameof(Queryable.OrderBy)
                && method.IsGenericMethodDefinition
                && method.GetGenericArguments().Length == 2
                && method.GetParameters().Length == 2);

        private static readonly MethodInfo __OrderByDescending = typeof(Queryable)
            .GetMethods()
            .Single(method => method.Name == nameof(Queryable.OrderByDescending)
                && method.IsGenericMethodDefinition
                && method.GetGenericArguments().Length == 2
                && method.GetParameters().Length == 2);

        private static MethodInfo GetMethodInfo(Type Source, Type Result, bool Descending) =>
            (Descending ? __OrderByDescending : __OrderBy).MakeGenericMethod(Source, Result);

        private static readonly ConcurrentDictionary<(Type Source, Type Result, bool Descending), Delegate> __Selectors = new ConcurrentDictionary<(Type Source, Type Result, bool Descending), Delegate>();

        private static Func<IQueryable<T>, LambdaExpression, IQueryable<T>> GetMethod<T>(Type Result, bool Descending) =>
            (Func<IQueryable<T>, LambdaExpression, IQueryable<T>>) __Selectors.GetOrAdd((typeof(T), Result, Descending), info =>
            {
                var (source, result, descending) = info;
                var method = GetMethodInfo(source, result, descending);
                return method.CreateDelegate(typeof(Func<IQueryable<T>, LambdaExpression, IQueryable<T>>));
            });

        public int Page { get; set; } = 0;
        public int Size { get; set; } = 10;

        public string OrderProperty { get; set; }
        public bool OrderByDescending { get; set; }

        public string SearchProperty { get; set; }
        public string SearchTerm { get; set; }

        internal (IQueryable<T> Query, int TotalCount) Items<T>(IQueryable<T> query)
        {
            var items = Order(Search(query));
            return (items.Skip(Page * Size).Take(Size), items.Count());
        }

        private static Expression GetProperty(Expression x, string PropertyQuery) => PropertyQuery
           .Split('.')
           .Aggregate(x, Expression.Property);

        private IQueryable<T> Order<T>(IQueryable<T> query)
        {
            if (string.IsNullOrWhiteSpace(OrderProperty)) return query;

            var x = Expression.Parameter(typeof(T), "x");
            var value = GetProperty(x, OrderProperty);
            var criteria = Expression.Lambda(typeof(Func<,>).MakeGenericType(typeof(T), value.Type), value, x);

            //return GetMethodInfo(typeof(T), value.Type, OrderByDescending)
            //   .Invoke(null, new object[] { query, criteria })
            //    as IQueryable<T>;
            return GetMethod<T>(value.Type, OrderByDescending)(query, criteria);
        }

        private IQueryable<T> Search<T>(IQueryable<T> query)
        {
            if (string.IsNullOrWhiteSpace(SearchProperty) || string.IsNullOrEmpty(SearchTerm)) return query;

            var x = Expression.Parameter(typeof(T), "x");

            var value = GetProperty(x, SearchProperty);
            if (value.Type == typeof(string))
                value = Expression.Call(value, "ToString", Type.EmptyTypes);

            var body = Expression.Call(value, "Contains", Type.EmptyTypes, Expression.Constant(SearchTerm));
            var condition = Expression.Lambda<Func<T, bool>>(body, x);

            return query.Where(condition);
        }
    }
}