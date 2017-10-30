using System.ComponentModel.DataAnnotations;

namespace GoodiesMarket.Security.API.Models
{
    public class Login
    {
        [Required,
        Display(Name = "Username"),
        DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required,
        StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6),
        DataType(DataType.Password),
        Display(Name = "Password")]
        public string Password { get; set; }
    }
}