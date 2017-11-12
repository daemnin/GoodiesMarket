using System.ComponentModel.DataAnnotations;

namespace GoodiesMarket.API.Models
{
    public class Product
    {
        [Required(ErrorMessage = "El nombre es requerido."),
        StringLength(255, ErrorMessage = "El nombre es demasiado largo.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "La descripción es demasiado largo.")]
        public string Description { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El número de invetario no es válido.")]
        public int? Stock { get; set; }

        [Required(ErrorMessage = "El precio es requerido."),
        Range(0, float.MaxValue, ErrorMessage = "El precio introducido es invalido.")]
        public float Price { get; set; }
    }
}