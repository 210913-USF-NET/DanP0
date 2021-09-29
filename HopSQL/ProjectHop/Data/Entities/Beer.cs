using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Entities
{
    public partial class Beer
    {
        public Beer()
        {
            Inventories = new HashSet<Inventory>();
            LineItems = new HashSet<LineItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int StoresId { get; set; }

        public virtual Store Stores { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<LineItem> LineItems { get; set; }
    }
}
