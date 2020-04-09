using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SportsStore.Infrastructure.Extensions;
using SportsStore.Interfaces.Products;
using SportsStore.Services.Data;

namespace SportsStore.Controllers
{
    public class DatabaseController : Controller
    {
        private readonly IProductsRepository _Products;
        private readonly ILogger<DatabaseController> _Logger;

        public DatabaseController(IProductsRepository Products, ILogger<DatabaseController> Logger)
        {
            _Products = Products;
            _Logger = Logger;
        }

        public IActionResult Index() => this
           .With(c => c.ViewBag.Count = c._Products.Query.Count())
           .View(_Products.Query.OrderBy(p => p.Id).Take(20));

        [HttpPost]
        public async Task<IActionResult> CreateSeedData(int Count, [FromServices] SportStoreDBInitializer db)
        {
            _Logger.LogInformation("Инициализция БД данными в количестве {0} элементов", Count);
            await ClearData(db);
            if (Count <= 0)
                return RedirectToAction(nameof(Index));

            var timer = Stopwatch.StartNew();
            await db.SeedTestDataAsync(Count);
            timer.Stop();
            _Logger.LogInformation("Инициализация БД данными в количестве {0} - {1}мс", Count, timer.ElapsedMilliseconds);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ClearData([FromServices] SportStoreDBInitializer db)
        {
            _Logger.LogInformation("Запуск очистки БД...");
            var timer = Stopwatch.StartNew();

            await db.ClearDatabaseAsync();

            timer.Stop();
            _Logger.LogInformation("Выполнение очистки БД заняло {0} мс", timer.ElapsedMilliseconds);
            return RedirectToAction(nameof(Index));
        }
    }
}