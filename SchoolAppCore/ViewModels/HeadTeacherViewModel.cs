using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using SchoolAppCore.Models;
using SchoolAppCore.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

			Students = new ObservableCollection<Student>(_teacher.Class.Students);

			Grades = new ObservableCollection<Grade>();
			Absences = new ObservableCollection<Absence>();
		}

		public ObservableCollection<Student> Students { get; set; }

		[ObservableProperty]
		[NotifyPropertyChangedFor(nameof(Grades))]
		[NotifyPropertyChangedFor(nameof(Absences))]
		Student selectedStudent;

		[ObservableProperty]
		ObservableCollection<Grade> grades;

		[ObservableProperty]
		ObservableCollection<Absence> absences;

		[ObservableProperty]
		Grade selectedGrade;

		[ObservableProperty]
		Absence selectedAbsence;

		partial void OnSelectedStudentChanged(Student value)
		{
			if(value != null)
			{
				Grades = new ObservableCollection<Grade>(value.Grades);
				Absences = new ObservableCollection<Absence>(value.Absences);
			}
		}

		[RelayCommand]
		public void RemoveAbsence()
		{
			if(SelectedAbsence != null)
			{
				SelectedStudent.Absences.Remove(SelectedAbsence);
				_context.Database.ExecuteSql($"RemoveAbsence {SelectedAbsence.StudentId}");

				Absences.Remove(SelectedAbsence);
			}
		}
	}
}
