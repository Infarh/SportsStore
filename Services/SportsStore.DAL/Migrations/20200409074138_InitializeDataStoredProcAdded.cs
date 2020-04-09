using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsStore.DAL.Migrations
{
    public partial class InitializeDataStoredProcAdded : Migration
    {
        private const string __StoreProcedureName = "CreateSeedData";

        private const string __CategoriesTableName = "Categories";
        private const string __CategoriesTableRowName = "Name";
        private const string __CategoriesTabledRowDescription = "Description";

        private const string __ProductsTableName = "Products";
        private const string __ProductsTableRowName = "Name";
        private const string __ProductsTableRowCategoryId = "CategoryId";
        private const string __ProductsTableRowPurchasePrice = "PurchasePrice";
        private const string __ProductsTableRowRetailPrice = "RetailPrice";

        private const string __StoredProcedure = 
@"CREATE PROCEDURE [{0}]
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
                    INSERT INTO [{1}] ({2}, {3})
                    VALUES (CONCAT('Category-', @i), 'Test Data Category');
                    SET @catId = SCOPE_IDENTITY();
                    DECLARE @j INT = 1;
                    WHILE @j <= 10
                        BEGIN
                            SET @pprice = RAND()*(500 - 5 + 1);
                            SET @rprice = (1 + RAND())*@pprice;
                            INSERT INTO [{4}] ({5}, {6}, {7}, {8})
                            VALUES (CONCAT('Product', @i, '-', @j), @catId, @pprice, @rprice);
                            SET @j = @j + 1
                        END
                    SET @i = @i + 1
                END
        COMMIT
    END
";

        protected override void Up(MigrationBuilder db)
        {
            if (!db.IsSqlServer()) return;
            db.Sql(string.Format(__StoredProcedure,
                __StoreProcedureName,
                __CategoriesTableName, __CategoriesTableRowName, __CategoriesTabledRowDescription,
                __ProductsTableName, __ProductsTableRowName, __ProductsTableRowCategoryId, __ProductsTableRowPurchasePrice, __ProductsTableRowRetailPrice));
        }

        protected override void Down(MigrationBuilder db)
        {
            if (!db.IsSqlServer()) return;
            db.Sql($"DROP PROCEDURE IF EXISTS [{__StoreProcedureName}]");
        }
    }
}
