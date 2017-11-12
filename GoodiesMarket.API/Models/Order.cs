using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoodiesMarket.API.Models
{
    public class Order
    {
        [StringLength(500, ErrorMessage = "El comentario es demasiado largo.")]
        public string Note { get; set; }

        public IEnumerable<OrderProduct> Products { get; set; }

        [Required]
        public Guid SellerId { get; set; }
    }
}