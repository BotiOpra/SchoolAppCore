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
	public partial class StudentManagementViewModel : ObservableObject
	{
		private readonly AdminNavigationStore _adminPageViewModel;

		public StudentManagementViewModel(AdminNavigationStore adminPageViewModel)
		{
			_adminPageViewModel = adminPageViewModel;
		}
	}
}
