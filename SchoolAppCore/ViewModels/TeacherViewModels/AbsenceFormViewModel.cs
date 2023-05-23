using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SchoolAppCore.Models;
using SchoolAppCore.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppCore.ViewModels.TeacherViewModels
{
	public partial class AbsenceFormViewModel : ObservableObject
	{
		private readonly TeacherPageViewModel _parent;
		private readonly ModalNavigationStore _navigation;

		public AbsenceFormViewModel(TeacherPageViewModel parent, ModalNavigationStore navigation)
		{
			_parent = parent;
			_navigation = navigation;

			EnteredDate = DateTime.Today;
		}

		[ObservableProperty]
		DateTime enteredDate;

		[RelayCommand]
		public void Add()
		{
			var absence = new Absence()
			{
				AbsenceDate = EnteredDate,
				StudentId = _parent.SelectedStudent.StudentId,
				SubjectId = _parent.SelectedSubject.SubjectId
			};
			
			using (var context = new SchoolDbContext())
			{	
				context.Absences.Add(absence);
				context.SaveChanges();
			}

			_parent.Absences.Add(absence);
			_parent.SelectedStudent.Absences.Add(absence);

			_parent.LoadAbsencesFromDatabase();

			_navigation.CloseModal();
		}

		[RelayCommand]
		public void Cancel()
		{
			_navigation.CloseModal();
		}
	}
}
