using SportsStore.Domain.Models;
using SportsStore.Domain.Models.Base.Interfaces;
using SportsStore.Interfaces.Base;

namespace SportsStore.Infrastructure.Extensions
{
    internal static class RepositoryExtensions
    {
        public static PagedList<T> GetPagedItems<T>(this IRepository<T> repository, PageOptions Page) 
            where T : class, IEntity =>
            new PagedList<T>(repository.Query, Page);
    }
}
