﻿using System.Linq;
using Microsoft.EntityFrameworkCore;
using SportsStore.DAL.Context;
using SportsStore.Domain.Models;
using SportsStore.Interfaces.Products;
using SportsStore.Services.Base.InSQL;
using SportsStore.Services.Mapping;

namespace SportsStore.Services.Products.InSQL
{
    internal class ProductsRepositoryInSQL : RepositoryInSQL<Product>, IProductsRepository
    {
        public override IQueryable<Product> Query => base.Query
           .Include(product => product.Category);

        public ProductsRepositoryInSQL(SportStoreDB Context) : base(Context) { }

        protected override void Update(Product DbItem, Product Item) => Item.CopyTo(DbItem);
    }
}
