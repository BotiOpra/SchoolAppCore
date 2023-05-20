using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SchoolAppCore.Models;
using SchoolAppCore.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SchoolAppCore.ViewModels.AdminViewModels
{
    public partial class SubjectManagementViewModel : ObservableObject
    {
		private readonly AdminNavigationStore _adminNavigationStore;

		[ObservableProperty]
		Subject selectedSubject;

		public SubjectManagementViewModel(AdminNavigationStore adminNavigationStore)
		{
			_adminNavigationStore = adminNavigationStore;
		}

		[RelayCommand(CanExecute = nameof(IsSubjectSelected))]
		private void DeleteSubject()
		{
			MessageBox.Show("Deleted User");
		}

		[RelayCommand(CanExecute = nameof(IsSubjectSelected))]
		private void ModifySubject()
		{
			MessageBox.Show("Modified User");
		}

		private bool IsSubjectSelected()
		{
			return SelectedSubject != null;
		}
	}
}
