using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SchoolAppCore.Models;
using SchoolAppCore.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppCore.ViewModels.TeacherViewModels
{
	public partial class GradeFormViewModel : ObservableObject
	{
		private readonly TeacherPageViewModel _parent;
		private readonly ModalNavigationStore _navigation;

		public GradeFormViewModel(TeacherPageViewModel parent, ModalNavigationStore navigation)
		{
			_parent = parent;
			_navigation = navigation;

		}

		[ObservableProperty]
		string enteredGrade;

		[RelayCommand]
		public void Add()
		{
			var grade = new Grade()
			{
				GradeVal = int.Parse(enteredGrade),
				StudentId = _parent.SelectedStudent.StudentId,
				SubjectId = _parent.SelectedSubject.SubjectId
			};

			using (var context = new SchoolDbContext())
			{


				context.Grades.Add(grade);
				context.SaveChanges();
			}

			_parent.StudentGrades.Add(grade);
			_parent.SelectedStudent.Grades.Add(grade);

			_parent.LoadGradesFromDatabase();

			_navigation.CloseModal();
		}

		[RelayCommand]
		public void Cancel()
		{
			_navigation.CloseModal();
		}
	}
}
