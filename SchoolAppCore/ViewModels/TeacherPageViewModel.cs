using CommunityToolkit.Mvvm.ComponentModel;
using SchoolAppCore.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppCore.ViewModels
{
	public class TeacherPageViewModel : ObservableObject
	{
		private readonly AdminNavigationStore _navigationStore;

		public TeacherPageViewModel(AdminNavigationStore navigationStore)
		{
			_navigationStore = navigationStore;
		}
	}
}
