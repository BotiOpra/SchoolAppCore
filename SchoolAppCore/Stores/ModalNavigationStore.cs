using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppCore.Stores
{
	public class ModalNavigationStore
	{
		private ObservableObject _currentViewModel;

		public ObservableObject CurrentViewModel
		{
			get { return _currentViewModel; }
			set
			{
				_currentViewModel = value;
				OnCurrentViewModelChanged();
			}
		}

		public bool IsOpen => _currentViewModel != null;

		public void CloseModal()
		{
			CurrentViewModel = null;
		}

		public event Action CurrentViewModelChanged;

		private void OnCurrentViewModelChanged()
		{
			CurrentViewModelChanged?.Invoke();
		}
	}
}
