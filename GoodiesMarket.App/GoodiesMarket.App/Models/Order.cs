using GoodiesMarket.Components.Models;
using Prism.Mvvm;

namespace GoodiesMarket.App.Models
{
    public class Order : BindableBase
    {
        private string buyer;
        public string Buyer
        {
            get { return buyer; }
            set { SetProperty(ref buyer, value); }
        }
        private long id;
        public long Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }
        private float total;
        public float Total
        {
            get { return total; }
            set { SetProperty(ref total, value); }
        }
        private string seller;
        public string Seller
        {
            get { return seller; }
            set { SetProperty(ref seller, value); }
        }
        private StatusType status;
        public StatusType Status
        {
            get { return status; }
            set { SetProperty(ref status, value); }
        }
    }
}
