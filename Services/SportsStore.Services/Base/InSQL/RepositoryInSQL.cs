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
            ItemsSet.Update(item);
            _Context.SaveChanges();
        }
    }
}
