using GoodiesMarket.Components.Models;
using System.ComponentModel.DataAnnotations;

namespace GoodiesMarket.Security.API.Models
{
    public class Register
    {
        [Required(ErrorMessage = "El email es requerido."),
        EmailAddress(ErrorMessage = "Correo inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida."),
        StringLength(60, MinimumLength = 6, ErrorMessage = "La contraseña debe de ser de almenos 6 carácteres.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El rol es un campo requerido.")]
        public RoleType RoleType { get; set; }
    }
}