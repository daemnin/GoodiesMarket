using Prism.Mvvm;

namespace GoodiesMarket.App.Models
{
    public class OrderProduct : BindableBase
    {
        private string pictureUrl;
        public string PictureUrl
        {
            get { return pictureUrl; }
            set { SetProperty(ref pictureUrl, value); }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        private float price;
        public float Price
        {
            get { return price; }
            set { SetProperty(ref price, value); }
        }
        private int amount;
        public int Amount
        {
            get { return amount; }
            set { SetProperty(ref amount, value); }
        }
        private int stock;
        public int Stock
        {
            get { return stock; }
            set { SetProperty(ref stock, value); }
        }
    }
}
