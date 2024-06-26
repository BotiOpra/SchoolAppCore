﻿using CommunityToolkit.Mvvm.ComponentModel;
using SchoolAppCore.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppCore.ViewModels
{
	public partial class MainViewModel : ObservableObject
	{

		private readonly NavigationStore _navigationStore;

		public ObservableObject CurrentViewModel => _navigationStore.CurrentViewModel;

		public MainViewModel(NavigationStore navigationStore)
		{
			_navigationStore = navigationStore;

			_navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
		}

		private void OnCurrentViewModelChanged()
		{
			OnPropertyChanged(nameof(CurrentViewModel));
		}
	}
}
