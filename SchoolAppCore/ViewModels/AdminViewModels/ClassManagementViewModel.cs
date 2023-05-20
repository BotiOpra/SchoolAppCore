using CommunityToolkit.Mvvm.ComponentModel;
using SchoolAppCore.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppCore.ViewModels.AdminViewModels
{
    class ClassManagementViewModel : ObservableObject
    {
		private readonly AdminNavigationStore _adminNavigationStore;

		public ClassManagementViewModel(AdminNavigationStore adminNavigationStore)
		{
			_adminNavigationStore = adminNavigationStore;
		}
	}
}
