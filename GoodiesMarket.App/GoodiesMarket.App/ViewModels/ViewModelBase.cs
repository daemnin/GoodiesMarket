﻿using Prism.Mvvm;
using Prism.Navigation;

namespace GoodiesMarket.App.ViewModels
{
    public abstract class ViewModelBase : BindableBase, INavigationAware
    {
        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}
