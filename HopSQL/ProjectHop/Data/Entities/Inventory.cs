using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Entities
{
    public partial class Inventory
    {
        public int Id { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public int BeersId { get; set; }

        public virtual Beer Beers { get; set; }
    }
}
