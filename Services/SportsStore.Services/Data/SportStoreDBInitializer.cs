using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportsStore.DAL.Context;

namespace SportsStore.Services.Data
{
    public class SportStoreDBInitializer
    {
        private readonly SportStoreDB _Context;

        public SportStoreDBInitializer(SportStoreDB Context) => _Context = Context;

        public void Initialize() => InitializeAsync().GetAwaiter().GetResult();

        public async Task InitializeAsync()
        {
            await _Context.Database.MigrateAsync().ConfigureAwait(false);
        }
    }
}
