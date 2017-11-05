using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodiesMarket.Model
{
    public class OrderProduct
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public int Quantity { get; set; }

        public long ProductId { get; set; }
        public Product Product { get; set; }

        public long OrderId { get; set; }
        public Order Order { get; set; }
    }
}
