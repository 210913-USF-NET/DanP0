using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Entities
{
    public partial class LineItem
    {
        public LineItem()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public int? Quantity { get; set; }
        public int BeersId { get; set; }

        public virtual Beer Beers { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
