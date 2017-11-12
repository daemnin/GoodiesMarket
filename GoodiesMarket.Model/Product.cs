using System;
using System.ComponentModel.DataAnnotations;

namespace GoodiesMarket.Model
{
    public class Product
    {
        [Key]
        public long Id { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public float Price { get; set; }

        [Required,
        StringLength(255)]
        public string Name { get; set; }

        public int? Stock { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public Guid SellerId { get; set; }
        public Seller Seller { get; set; }
    }
}
