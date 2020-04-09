using SportsStore.Domain.Models;
using SportsStore.Domain.Models.Base.Interfaces;
using SportsStore.Interfaces.Base;

namespace SportsStore.Infrastructure.Extensions
{
    internal static class RepositoryExtensions
    {
        public static QueryResult<T> GetQueryItems<T>(this IRepository<T> repository, QueryOptions Query) 
            where T : class, IEntity =>
            new QueryResult<T>(repository.Query, Query);
    }
}
