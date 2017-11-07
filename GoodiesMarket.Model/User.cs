using System;
using System.ComponentModel.DataAnnotations;

namespace GoodiesMarket.Model
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public int Reach { get; set; }

        public float Score { get; set; }

        public string PictureUrl { get; set; }
    }
}
