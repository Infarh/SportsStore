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

        public virtual IPagedItems<T> Items => new PagedItems<T>(((IRepository<T>)this).Query);

        public IPagedEnumerable<T> Get(int Page, int PageSize) => new PagedEnumerable<T>(((IRepository<T>)this).Query, Page, PageSize);

        public virtual void Add(T item)
        {
            if (_Items.Contains(item)) return;
            _Items.Add(item);
        }

        public virtual void Update(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            var db_item = ((IRepository<T>) this).Get(item.Id);
            if (db_item is null || ReferenceEquals(db_item, item)) return;
            Update(db_item, item);
        }

        protected virtual void Update(T DbItem, T Item) => throw new NotImplementedException();

        public virtual void Delete(T item) => _Items.Remove(item);

        public virtual void Clear() => _Items.Clear();

        #endregion
    }
}
