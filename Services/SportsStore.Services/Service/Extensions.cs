using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SportsStore.Services.Service
{
    internal static class Extensions
    {
        public static DbContext ThrowIfHasChanges(this DbContext context)
        {
            if (context.ChangeTracker.HasChanges())
                throw new InvalidOperationException("В контексте БД есть незафиксированные изменения");
            return context;
        }

        public static DbContext GetContext<T>(this DbSet<T> Set) where T : class
        {
            var infrastructure = (IInfrastructure<IServiceProvider>)Set;
            var service_provider = infrastructure.Instance;
            var context_service = (ICurrentDbContext)service_provider.GetService(typeof(ICurrentDbContext));
            return context_service.Context;
        }

        public static string GetTableName<T>(this DbSet<T> Set) where T : class
        {
            var context = Set.GetContext();
            var model = context.Model;
            var entity_types = model.GetEntityTypes();
            var entity_type = entity_types.First(type => type.ClrType == typeof(T));
            var table_name_annotation = entity_type.GetAnnotation("Relational:TableName");
            return table_name_annotation.Value.ToString();
        }

        public static void TruncateTable<T>(this DbSet<T> Set) where T : class
        {
            var db = Set.GetContext().ThrowIfHasChanges().Database;

            var table = Set.GetTableName();
            db.ExecuteSqlRaw("TRUNCATE TABLE {0}", $"[{table}]");
        }
    }
}
