using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using SchoolAppCore.Models;
using SchoolAppCore.Stores;
using SchoolAppCore.ViewModels.EntityViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppCore.ViewModels
{
	public partial class StudentPageViewModel : ObservableObject
	{
		private readonly ModalNavigationStore _navigationStore;
		private readonly Student _student;

		private readonly SchoolDbContext _context;
		public StudentPageViewModel(Student student, ModalNavigationStore navigationStore) 
		{
			_navigationStore = navigationStore;
			_student = student;
			_context = new SchoolDbContext(); 
			Courses = new ObservableCollection<Subject>(_context.Subjects.FromSqlRaw("EXEC GetSubjectsByClassId {0}", _student.ClassId)
			.ToList());
		}

		public ObservableCollection<Subject> Courses { get; set; }

		[ObservableProperty]
		[NotifyPropertyChangedFor(nameof(Grades))]
		[NotifyPropertyChangedFor(nameof(Absences))]
		Subject selectedCourse;

		public ObservableCollection<Grade> Grades => new ObservableCollection<Grade>(_student.Grades.Where(g => g.SubjectId == SelectedCourse?.SubjectId));
		public ObservableCollection<Absence> Absences => new ObservableCollection<Absence>(_student.Absences.Where(g => g.SubjectId == SelectedCourse?.SubjectId));

		public string StudentName => _student.FirstName + ' ' + _student.LastName;
	}
}
