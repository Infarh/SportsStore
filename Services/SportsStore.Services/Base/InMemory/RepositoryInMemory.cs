using System;
using System.Collections.Generic;
using System.Linq;
using SportsStore.Domain.Models.Base.Interfaces;
using SportsStore.Interfaces.Base;

namespace SportsStore.Services.Base.InMemory
{
    public abstract class RepositoryInMemory<T> : IRepository<T> where T : class, IEntity
    {
        private readonly List<T> _Items;

        protected RepositoryInMemory(List<T> Items) => _Items = Items;

        protected RepositoryInMemory(IEnumerable<T> items) : this(items.ToList()) { }

        protected RepositoryInMemory() : this(new List<T>()) { }

        #region IRepository<T>

        IEnumerable<T> IRepository<T>.Items => _Items.AsReadOnly();

        void IRepository<T>.Add(T item)
        {
            if (_Items.Contains(item)) return;
            _Items.Add(item);
        }

        void IRepository<T>.Update(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            var db_item = ((IRepository<T>) this).Get(item.Id);
            if (db_item is null || ReferenceEquals(db_item, item)) return;
            Update(db_item, item);
        }

        protected abstract void Update(T DbItem, T Item);

        #endregion
    }
}
