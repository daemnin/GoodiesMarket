using Prism.Mvvm;
using System.Collections.Generic;

namespace GoodiesMarket.App.Models
{
    public class SellerProfileModel : BindableBase
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

        private string motto;
        public string Motto
        {
            get { return motto; }
            set { SetProperty(ref motto, value); }
        }

        private float score;
        public float Score
        {
            get { return score; }
            set { SetProperty(ref score, value); }
        }

        private int? range;
        public int? Range
        {
            get { return range; }
            set { SetProperty(ref range, value); }
        }

        private string starUrl;
        public string StarUrl
        {
            get { return starUrl; }
            set { SetProperty(ref starUrl, value); }
        }

        private List<ProductModel> products;
        public List<ProductModel> Products
        {
            get { return products; }
            set { SetProperty(ref products, value); }
        }
    }
}
