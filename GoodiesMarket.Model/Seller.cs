using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodiesMarket.Model
{
    public class Seller
    {
        [Key, ForeignKey("User")]
        public Guid Id { get; set; }

        public string Motto { get; set; }
        public string Restriction { get; set; }

        public User User { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}