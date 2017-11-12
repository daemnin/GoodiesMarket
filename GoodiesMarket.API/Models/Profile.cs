using System.ComponentModel.DataAnnotations;

namespace GoodiesMarket.API.Models
{
    public class Profile
    {
        [Range(-90, 90, ErrorMessage = "La latitud es inválida.")]
        public double? Latitude { get; set; }

        [Range(-180, 180, ErrorMessage = "La longitud es inválida.")]
        public double? Longitude { get; set; }

        [Range(30, int.MaxValue, ErrorMessage = "La distancia mínima es de 30 metros.")]
        public int? Range { get; set; }

        [StringLength(255, ErrorMessage = "El lema es muy largo.")]
        public string Motto { get; set; }
    }
}