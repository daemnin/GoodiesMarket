using GoodiesMarket.Components.Models;

namespace GoodiesMarket.API.Models
{
    public class Register
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleType RoleType { get; set; }
    }
}