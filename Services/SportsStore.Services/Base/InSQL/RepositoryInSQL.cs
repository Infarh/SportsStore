using System;
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

        protected virtual DbSet<T> ItemsSet => _Context.Set<T>();

        public virtual IQueryable<T> Query => ItemsSet;

        public virtual IPagedItems<T> Items => new PagedItems<T>(Query);

        public IPagedEnumerable<T> Get(int Page, int PageSize) => new PagedEnumerable<T>(Query, Page, PageSize);

        protected RepositoryInSQL(SportStoreDB Context) => _Context = Context;

        public virtual void Add(T item)
        {
            ItemsSet.Add(item);
            //_Context.Entry(item).State = EntityState.Added;
            SaveChanges();
        }

        public virtual void Update(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            var db_item = ItemsSet.Find(item.Id);
            if (db_item is null) return;
            Update(db_item, item);
            SaveChanges();
        }

        protected virtual void Update(T DbItem, T Item) => throw new NotImplementedException();

        public virtual void UpdateAll(params T[] items)
        {
            //ItemsSet.UpdateRange(items);

            var items_dictionary = items.ToDictionary(p => p.Id);
            var db_items = ItemsSet.Where(p => items_dictionary.Keys.Contains(p.Id));
            foreach (var db_item in db_items)
                Update(db_item, items_dictionary[db_item.Id]);

            SaveChanges();
        }

        public virtual void Delete(T item)
        {
            ItemsSet.Remove(item);
            SaveChanges();
        }

        public virtual void SaveChanges() => _Context.SaveChanges();
    }
}
