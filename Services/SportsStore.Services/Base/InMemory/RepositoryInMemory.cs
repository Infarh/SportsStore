using System.Collections.Generic;
using System.Linq;
using SportsStore.Interfaces.Base;

namespace SportsStore.Services.Base.InMemory
{
    public abstract class RepositoryInMemory<T> : IRepository<T>
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

        #endregion
    }
}
