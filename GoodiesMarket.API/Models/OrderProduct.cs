using System.ComponentModel.DataAnnotations;

namespace GoodiesMarket.API.Models
{
    public class OrderProduct
    {
        [Required(ErrorMessage = "La cantidad es requerida.")]
        public int Quantity { get; set; }

        [Required,
        Range(1, long.MaxValue, ErrorMessage = "Id de producto inválido.")]
        public long Id { get; set; }
    }
}