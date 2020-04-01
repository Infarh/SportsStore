using System;
using System.Collections.Generic;
using System.Linq;
using SportsStore.Domain.Models.Base.Interfaces;

namespace SportsStore.Interfaces.Base
{
    public interface IRepository<T> where T : class, IEntity
    {
        IQueryable<T> Query => Items.AsQueryable();

        IEnumerable<T> Items { get; }

        T this[long Id] => Get(Id);

        T Get(long id) => Query.FirstOrDefault(item => item.Id == id);

        void Add(T item);

        void Update(T item);

        void UpdateAll(params T[] items) => Array.ForEach(items, Update);

        void Delete(T item);
    }
}
