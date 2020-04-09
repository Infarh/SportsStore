using System.Collections.Generic;
using SportsStore.Domain.Models.Base;
using SportsStore.Domain.Models.Base.Interfaces;

namespace SportsStore.Domain.Models
{
    public class Category : NamesEntity, IDescriptioned
    {
        public string Description { get; set; }
    }
}
