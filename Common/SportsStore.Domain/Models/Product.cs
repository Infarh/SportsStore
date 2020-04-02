using System.ComponentModel.DataAnnotations.Schema;
using SportsStore.Domain.Models.Base;

namespace SportsStore.Domain.Models
{
    public class Product : NamesEntity
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchasePrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal RetailPrice { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
