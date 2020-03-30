using System.ComponentModel.DataAnnotations.Schema;
using SportsStore.Domain.Models.Base;

namespace SportsStore.Domain.Models
{
    public class Product : NamesEntity
    {
        public string Category { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchasePrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal RetailPrice { get; set; }
    }
}
