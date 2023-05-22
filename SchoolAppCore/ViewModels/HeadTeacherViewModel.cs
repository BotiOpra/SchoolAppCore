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
	public partial class HeadTeacherViewModel : ObservableObject
	{
		private readonly ModalNavigationStore _navigationStore;
		private readonly HeadTeacher _teacher;

		private readonly SchoolDbContext _context;

		public HeadTeacherViewModel(HeadTeacher teacher, ModalNavigationStore navigationStore)
		{
			_navigationStore = navigationStore;
			_teacher = teacher;

			_context = new SchoolDbContext();
		}
	}
}
