using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SportsStore.DAL.Context;
using SportsStore.Domain.Models.Base.Interfaces;
using SportsStore.Interfaces.Base;

namespace SportsStore.Services.Base.InSQL
{
    public abstract class RepositoryInSQL<T> : IRepository<T> where T : class, IEntity
    {
        private readonly SportStoreDB _Context;

        private DbSet<T> ItemsSet => _Context.Set<T>();

        public virtual IQueryable<T> Query => ItemsSet;

        public IEnumerable<T> Items => Query.AsEnumerable();

        protected RepositoryInSQL(SportStoreDB Context) => _Context = Context;

        public void Add(T item)
        {
            ItemsSet.Add(item);
            _Context.SaveChanges();
        }

        public void Update(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            var db_item = ItemsSet.Find(item.Id);
            if (db_item is null) return;
            Update(db_item, item);
            _Context.SaveChanges();
        }

        protected abstract void Update(T DbItem, T Item);

        public void UpdateAll(params T[] items)
        {
            //ItemsSet.UpdateRange(items);

            var items_dictionary = items.ToDictionary(p => p.Id);
            var db_items = ItemsSet.Where(p => items_dictionary.Keys.Contains(p.Id));
            foreach (var db_item in db_items)
                Update(db_item, items_dictionary[db_item.Id]);

            _Context.SaveChanges();
        }

        public void Delete(T item)
        {
            ItemsSet.Remove(item);
            _Context.SaveChanges();
        }
    }
}
