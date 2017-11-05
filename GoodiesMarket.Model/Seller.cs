using System.Collections.Generic;

namespace GoodiesMarket.Model
{
    public class Seller : User
    {
        public string Motto { get; set; }
        public string Restriction { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}