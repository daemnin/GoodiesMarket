using Prism.Mvvm;
using System.Collections.Generic;

namespace GoodiesMarket.App.Models
{
    public class PlaceOrderModel : BindableBase
    {
        private string sellerName;
        public string SellerName
        {
            get { return sellerName; }
            set { SetProperty(ref sellerName, value); }
        }
        private float total;
        public float Total
        {
            get { return total; }
            set { SetProperty(ref total, value); }
        }
        private string note;
        public string Note
        {
            get { return note; }
            set { SetProperty(ref note, value); }
        }
        private string purchaseRestriction;
        public string PurchaseRestriction
        {
            get { return purchaseRestriction; }
            set { SetProperty(ref purchaseRestriction, value); }
        }
        public List<OrderProduct> Products { get; set; }
    }
}
