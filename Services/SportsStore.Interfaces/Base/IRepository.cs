using System.Collections.Generic;

namespace SportsStore.Interfaces.Base
{
    public interface IRepository<T>
    {
        IEnumerable<T> Items { get; }

        void Add(T item);
    }
}
