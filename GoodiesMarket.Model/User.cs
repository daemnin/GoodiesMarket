using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodiesMarket.Model
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Index("IX_Email", IsUnique = true)]
        [StringLength(255)]
        public string Email { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public float Score { get; set; }

        public string PictureUrl { get; set; }
    }
}
