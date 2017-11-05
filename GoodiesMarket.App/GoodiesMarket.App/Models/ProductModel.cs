using Prism.Mvvm;

namespace GoodiesMarket.App.Models
{
    public class ProductModel : BindableBase
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private int inventory;
        public int Inventory
        {
            get { return inventory; }
            set { SetProperty(ref inventory, value); }
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
    }
}
