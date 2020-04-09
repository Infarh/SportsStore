using System.ComponentModel.DataAnnotations.Schema;
using SportsStore.Domain.Models.Base;
using SportsStore.Domain.Models.Base.Interfaces;

namespace SportsStore.Domain.Models
{
    public class Product : NamesEntity, IDescriptioned
    {
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchasePrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal RetailPrice { get; set; }

        public long CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
