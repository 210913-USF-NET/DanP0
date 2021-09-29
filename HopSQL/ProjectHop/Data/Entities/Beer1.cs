using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Entities
{
    public partial class Beer1
    {
        public Beer1()
        {
            Inventories = new HashSet<Inventory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int StoresId { get; set; }

        public virtual Store Stores { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
    }
}
