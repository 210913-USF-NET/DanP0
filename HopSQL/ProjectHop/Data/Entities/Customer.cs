﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public bool? IsManager { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
