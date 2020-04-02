using System;
using SportsStore.Domain.Models;

namespace SportsStore.Services.Mapping
{
    public static class ProductMapper
    {
        public static Product CopyTo(this Product Source, Product Destination)
        {
            if (ReferenceEquals(Source, Destination)) return Source;
            if(Source is null) throw new ArgumentNullException(nameof(Source));
            if(Destination is null) throw new ArgumentNullException(nameof(Destination));

            Destination.Name = Source.Name;
            Destination.Category = Source.Category;
            Destination.PurchasePrice = Source.PurchasePrice;
            Destination.RetailPrice = Source.RetailPrice;

            return Destination;
        }
    }
}
