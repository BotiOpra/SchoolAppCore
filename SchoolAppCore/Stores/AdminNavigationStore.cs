using CommunityToolkit.Mvvm.ComponentModel;
using SchoolAppCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppCore.Stores
{
	public class AdminNavigationStore
	{
		private ObservableObject _currentViewModel;
		public ObservableObject CurrentViewModel
		{
			get => _currentViewModel;
			set
			{
				if (_currentViewModel != value)
				{
					_currentViewModel = value;
					OnCurrentViewModelChanged();
				}
			}
		}

		public event Action CurrentViewModelChanged;

		private void OnCurrentViewModelChanged()
		{
			CurrentViewModelChanged?.Invoke();
		}
	}
}
