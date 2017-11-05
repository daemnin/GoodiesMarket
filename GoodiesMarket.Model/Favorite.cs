using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodiesMarket.Model
{
    public class Favorite
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Index("UQ_User_Seller", 1, IsUnique = true)]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [Index("UQ_User_Seller", 2, IsUnique = true)]
        public Guid SellerId { get; set; }
        public Seller Seller { get; set; }
    }
}
