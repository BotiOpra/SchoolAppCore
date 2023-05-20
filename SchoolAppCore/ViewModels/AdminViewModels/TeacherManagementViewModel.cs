using CommunityToolkit.Mvvm.ComponentModel;
using SchoolAppCore.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppCore.ViewModels.AdminViewModels
{
	public class TeacherManagementViewModel : ObservableObject
	{
		private readonly AdminNavigationStore _adminNavigationStore;

		public TeacherManagementViewModel(AdminNavigationStore adminNavigationStore)
		{
			_adminNavigationStore = adminNavigationStore;
		}


	}
}
