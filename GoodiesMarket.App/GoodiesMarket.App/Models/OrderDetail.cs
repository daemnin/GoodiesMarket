using Prism.Mvvm;

namespace GoodiesMarket.App.Models
{
    public class OrderDetail : BindableBase
    {
        private long id;
        public long Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }
    }
}
