using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SportsStore.DAL.Context;
using SportsStore.Infrastructure.Extensions;
using SportsStore.Interfaces.Products;

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
        public IActionResult CreateSeedData(int Count, [FromServices] SportStoreDB context)
        {
            _Logger.LogInformation("Инициализция БД данными в количестве {0} элементов", Count);
            ClearData(context);
            if (Count <= 0)
                return RedirectToAction(nameof(Index));

            var db = context.Database;
            db.SetCommandTimeout(TimeSpan.FromMinutes(10));

            var timer = Stopwatch.StartNew();
            db.ExecuteSqlRaw("DROP PROCEDURE IF EXISTS CreateSeedData");
            timer.Stop();
            _Logger.LogInformation("Удаление хранимой процедуры инициализации БД - {0}мс", timer.ElapsedMilliseconds);

            timer.Restart();
            // ReSharper disable StringLiteralTypo
            db.ExecuteSqlRaw(@"
                CREATE PROCEDURE CreateSeedData
                    @RowCount decimal
                AS
                    BEGIN
                        SET NOCOUNT ON
                        DECLARE @i INT = 1;
                        DECLARE @catId BIGINT;
                        DECLARE @CatCount INT = @RowCount / 10;
                        DECLARE @pprice DECIMAL(5,2);
                        DECLARE @rprice DECIMAL(5,2);
                        BEGIN TRANSACTION
                            WHILE @i <= @CatCount
                                BEGIN
                                    INSERT INTO [Categories] (Name, Description)
                                    VALUES (CONCAT('Category-', @i), 'Test Data Category');
                                    SET @catId = SCOPE_IDENTITY();
                                    DECLARE @j INT = 1;
                                    WHILE @j <= 10
                                        BEGIN
                                            SET @pprice = RAND()*(500 - 5 + 1);
                                            SET @rprice = (1 + RAND())*@pprice;
                                            INSERT INTO [Products] (Name, CategoryId, PurchasePrice, RetailPrice)
                                            VALUES (CONCAT('Product', @i, '-', @j), @catId, @pprice, @rprice);
                                            SET @j = @j + 1
                                        END
                                    SET @i = @i + 1
                                END
                            COMMIT
                        END
                ");
            // ReSharper restore StringLiteralTypo
            timer.Stop();
            _Logger.LogInformation("Создание новой хранимой процедуры инициализации БД - {0}мс", timer.ElapsedMilliseconds);

            using (db.BeginTransaction())
            {
                timer.Restart();
                db.ExecuteSqlRaw("EXEC CreateSeedData @RowCount = {0}", Count);
                db.CommitTransaction();
                timer.Stop();
            }
            _Logger.LogInformation("Инициализация БД данными в количестве {0} - {1}мс", Count, timer.ElapsedMilliseconds);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ClearData([FromServices] SportStoreDB context)
        {
            _Logger.LogInformation("Запуск очистки БД...");
            var timer = Stopwatch.StartNew();

            var db = context.Database;
            db.SetCommandTimeout(TimeSpan.FromMinutes(10));
            using var transaction = db.BeginTransaction();
            db.ExecuteSqlRaw("DELETE FROM Orders");
            db.ExecuteSqlRaw("DELETE FROM Categories");
            transaction.Commit();

            timer.Stop();
            _Logger.LogInformation("Выполнение очистки БД заняло {0} мс", timer.ElapsedMilliseconds);
            return RedirectToAction(nameof(Index));
        }
    }
}