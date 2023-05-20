using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SchoolAppCore.Stores;
using SchoolAppCore.ViewModels.AdminViewModels;
using SchoolAppCore.Views.AdminViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppCore.ViewModels
{
	public partial class AdminPageViewModel : ObservableObject
	{
		private readonly NavigationStore _navigationStore;
		private readonly AdminNavigationStore _adminNavigationStore;

		public ObservableObject CurrentManagementViewModel => _adminNavigationStore.CurrentViewModel;

		public AdminPageViewModel(NavigationStore navigationStore, AdminNavigationStore adminNavigationStore)
		{
			_navigationStore = navigationStore;
			_adminNavigationStore = adminNavigationStore;

			_adminNavigationStore.CurrentViewModelChanged += OnCurrentManagementViewModelChanged;
		}

		private void OnCurrentManagementViewModelChanged()
		{
			OnPropertyChanged(nameof(CurrentManagementViewModel));
		}

		[RelayCommand]
		public void NavigateStudentManagement()
		{
			_adminNavigationStore.CurrentViewModel = new StudentManagementViewModel(_adminNavigationStore); 
		}

		[RelayCommand]
		public void NavigateTeacherManagement()
		{
			_adminNavigationStore.CurrentViewModel = new TeacherManagementViewModel(_adminNavigationStore);
		}

		[RelayCommand]
		public void NavigateClassManagement()
		{
			_adminNavigationStore.CurrentViewModel = new ClassManagementViewModel(_adminNavigationStore);
		}

		[RelayCommand]
		public void NavigateSubjectManagement()
		{
			_adminNavigationStore.CurrentViewModel = new SubjectManagementViewModel(_adminNavigationStore);
		}
	}
}
