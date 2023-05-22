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
		private readonly AdminNavigationStore _adminNavigationStore;
		private readonly ModalNavigationStore _modalNavigationStore;

		public AdminPageViewModel(AdminNavigationStore adminNavigationStore, ModalNavigationStore modalNavigationStore)
		{
			_adminNavigationStore = adminNavigationStore;
			_modalNavigationStore = modalNavigationStore;

			_adminNavigationStore.CurrentViewModelChanged += OnCurrentManagementViewModelChanged;
		}

		public ObservableObject CurrentManagementViewModel => _adminNavigationStore.CurrentViewModel;

		private void OnCurrentManagementViewModelChanged()
		{
			OnPropertyChanged(nameof(CurrentManagementViewModel));
		}

		[RelayCommand]
		public void NavigateStudentManagement()
		{
			_adminNavigationStore.CurrentViewModel = new StudentManagementViewModel(_modalNavigationStore);
		}

		[RelayCommand]
		public void NavigateTeacherManagement()
		{
			_adminNavigationStore.CurrentViewModel = new TeacherManagementViewModel(_modalNavigationStore);
		}

		[RelayCommand]
		public void NavigateClassManagement()
		{
			_adminNavigationStore.CurrentViewModel = new ClassManagementViewModel(_modalNavigationStore);
		}

		[RelayCommand]
		public void NavigateSubjectManagement()
		{
			_adminNavigationStore.CurrentViewModel = new SubjectManagementViewModel(_modalNavigationStore);
		}

		[RelayCommand]
		public void NavigateSpecializationManagement()
		{
			_adminNavigationStore.CurrentViewModel = new SpecializationManagementViewModel(_modalNavigationStore);
		}
	}
}
