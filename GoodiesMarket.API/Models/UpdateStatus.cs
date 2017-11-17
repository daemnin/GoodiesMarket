using GoodiesMarket.Components.Models;
using System.ComponentModel.DataAnnotations;

namespace GoodiesMarket.API.Models
{
    public class UpdateStatus
    {
        [Required(ErrorMessage = "El estado es requerido")]
        public StatusType Status { get; set; }
    }
}