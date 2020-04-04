using System;
using System.Collections.Generic;
using System.Text;
using SportsStore.Domain.Models.Base;

namespace SportsStore.Domain.Models.Orders
{
    public class Order : BaseEntity
    {
        public string CustomerName { get; set; }

        public string Address { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public bool Shipped { get; set; }

        public IEnumerable<OrderLine> Lines { get; set; }
    }
}
