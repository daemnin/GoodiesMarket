using System.ComponentModel.DataAnnotations;

namespace GoodiesMarket.API.Models
{
    public class Search
    {
        [Required(ErrorMessage = "No se especificó un término para busqueda."),
        StringLength(255, ErrorMessage = "La búsqueda es muy específica.")]
        public string Query { get; set; }
    }
}