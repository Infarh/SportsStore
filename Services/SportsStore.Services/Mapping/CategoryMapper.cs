using System;
using SportsStore.Domain.Models;

namespace SportsStore.Services.Mapping
{
    public static class CategoryMapper
    {
        public static Category CopyTo(this Category Source, Category Destination)
        {
            if (ReferenceEquals(Source, Destination)) return Source;
            if(Source is null) throw new ArgumentNullException(nameof(Source));
            if(Destination is null) throw new ArgumentNullException(nameof(Destination));

            Destination.Name = Source.Name;
            Destination.Description = Source.Description;

            return Destination;
        }
    }
}