using CommunityToolkit.Mvvm.ComponentModel;
using SchoolAppCore.Models;
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
		private readonly NavigationStore _navigationStore;
		private readonly Professor _professor;

		public TeacherPageViewModel(Professor professor, NavigationStore navigationStore)
		{
			_navigationStore = navigationStore;
			_professor = professor;
		}
	}
}
