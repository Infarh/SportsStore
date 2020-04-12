using System;
using System.Collections.Generic;
using System.Linq;
using SportsStore.Domain.Models;
using SportsStore.Domain.Models.Base.Interfaces;
using SportsStore.Interfaces.Service;

namespace SportsStore.Interfaces.Base
{
    public interface IRepository<T> where T : class, IEntity
    {
        IQueryable<T> Query => Items.AsQueryable();

        IPagedItems<T> Items { get; }

        IPagedEnumerable<T> Get(int Page, int PageSize);

        T this[long Id] => Get(Id);

        QueryResult<T> GetQuery(QueryOptions Options) => new QueryResult<T>(Query, Options);

        T Get(long id) => Query.FirstOrDefault(item => item.Id == id);

        void Add(T item);

        void AddRange(IEnumerable<T> items) => items.ForEach(Add);

        void Update(T item);

        void UpdateAll(params T[] items) => Array.ForEach(items, Update);

        void Delete(T item);

        void Clear() => Items.ForEach(Delete);
    }
}
