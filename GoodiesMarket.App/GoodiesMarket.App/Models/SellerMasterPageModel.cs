﻿using Prism.Mvvm;

namespace GoodiesMarket.App.Models
{
    public class SellerMasterPageModel : BindableBase
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private string reach;
        public string Reach
        {
            get { return reach; }
            set { SetProperty(ref reach, value); }
        }

        private string profilePicture;
        public string ProfilePicture
        {
            get { return profilePicture; }
            set { SetProperty(ref profilePicture, value); }
        }
    }
}
