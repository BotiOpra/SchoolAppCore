using CommunityToolkit.Mvvm.ComponentModel;
using SchoolAppCore.Stores;
using SchoolAppCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppCore.Services
{
	public class NavigationService
	{
		private readonly NavigationStore _navigationStore;
		private readonly Func<ObservableObject> _createViewModel;

		public NavigationService(NavigationStore navigationStore, Func<ObservableObject> createViewModel)
		{
			_navigationStore = navigationStore;
			_createViewModel = createViewModel;
		}

		public void Navigate()
		{
			_navigationStore.CurrentViewModel = _createViewModel();
		}
	}
}
