using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodiesMarket.Model
{
    public class Comment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string Message { get; set; }

        public int Score { get; set; }

        public DateTime Timestamp { get; set; }

        public long OrderId { get; set; }
        public Order Order { get; set; }

        public Guid SenderId { get; set; }
        public User Sender { get; set; }

        public Guid ReceiverId { get; set; }
        public User Receiver { get; set; }
    }
}
