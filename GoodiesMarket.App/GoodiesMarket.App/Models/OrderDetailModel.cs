using GoodiesMarket.Components.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GoodiesMarket.App.Models
{
    public class OrderDetailModel : BindableBase
    {
        private long id;
        public long Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }
        private string note;
        public string Note
        {
            get { return note; }
            set { SetProperty(ref note, value); }
        }
        private StatusType statusId;
        public StatusType StatusId
        {
            get
            {
                return statusId;
            }
            set { SetProperty(ref statusId, value); }
        }
        private float total;
        public float Total
        {
            get { return total; }
            set { SetProperty(ref total, value); }
        }
        private DateTime createdOn;
        public DateTime CreatedOn
        {
            get { return createdOn; }
            set { SetProperty(ref createdOn, value); }
        }
        private DateTime lastModification;
        public DateTime LastModification
        {
            get { return lastModification; }
            set { SetProperty(ref lastModification, value); }
        }
        private string buyer;
        public string Buyer
        {
            get { return buyer; }
            set { SetProperty(ref buyer, value); }
        }
        private string seller;
        public string Seller
        {
            get { return seller; }
            set { SetProperty(ref seller, value); }
        }
        public ObservableCollection<OrderDetail> Products { get; set; }
    }
}
