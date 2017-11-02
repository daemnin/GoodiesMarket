using GoodiesMarket.App.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;

namespace GoodiesMarket.App.ViewModels
{
	public class RegistrationUserNameViewModel : ViewModelBase
	{
		private RegistrationModel model;
		public RegistrationModel Model
		{
			get { return model; }
			set { SetProperty(ref model, value); }
		}
		public RegistrationUserNameViewModel()
		{

		}
		public override void OnNavigatingTo(NavigationParameters parameters)
		{
			if (parameters.ContainsKey("model"))
			{
				Model = (RegistrationModel)parameters["model"];
				System.Diagnostics.Debug.WriteLine(model.IsSeller);
			}

		}
	}
}
