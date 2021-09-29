using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Entities
{
    public partial class Store
    {
        public Store()
        {
            Beers = new HashSet<Beer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }

        public virtual ICollection<Beer> Beers { get; set; }
    }
}
