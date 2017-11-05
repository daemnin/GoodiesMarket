using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodiesMarket.Model
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Note { get; set; }

        public float Total { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public virtual ICollection<OrderProduct> Products { get; set; }

        public Guid SellerId { get; set; }
        public Seller Seller { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }
    }
}