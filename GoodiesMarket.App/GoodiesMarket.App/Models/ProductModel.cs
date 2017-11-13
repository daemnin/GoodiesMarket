using Prism.Mvvm;

namespace GoodiesMarket.App.Models
{
    public class ProductModel : BindableBase
    {
        public long Id { get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private int? stock;
        public int? Stock
        {
            get { return stock; }
            set { SetProperty(ref stock, value); }
        }

        private float price;
        public float Price
        {
            get { return price; }
            set { SetProperty(ref price, value); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        private string imageUrl;
        public string ImageUrl
        {
            get { return imageUrl; }
            set { SetProperty(ref imageUrl, value); }
        }
    }
}
