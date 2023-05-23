using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using SchoolAppCore.Models;
using SchoolAppCore.Stores;
using SchoolAppCore.ViewModels.TeacherViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppCore.ViewModels
{
	public partial class TeacherPageViewModel : ObservableObject
	{
		private readonly ModalNavigationStore _navigationStore;
		private readonly Professor _professor;

		private readonly SchoolDbContext _context;

		public TeacherPageViewModel(Professor professor, ModalNavigationStore navigationStore)
		{
			_navigationStore = navigationStore;
			_navigationStore.CurrentViewModelChanged += OnCurrentModalChanged;

			_professor = professor;

			_context = new SchoolDbContext();

			Subjects = new ObservableCollection<Subject>(_professor.Subjects);
		}

		private void OnCurrentModalChanged()
		{
			OnPropertyChanged(nameof(CurrentModal));
			OnPropertyChanged(nameof(IsModalOpen));
		}

		public ObservableObject CurrentModal => _navigationStore.CurrentViewModel;

		public bool IsModalOpen => _navigationStore.IsOpen;

		[ObservableProperty]
		ObservableCollection<Subject> subjects;

		[ObservableProperty]
		ObservableCollection<Class> classes;

		[ObservableProperty]
		ObservableCollection<Student> selectedClassStudents;

		[ObservableProperty]
		ObservableCollection<Absence> absences;

		[ObservableProperty]
		ObservableCollection<Grade> studentGrades;

		private IEnumerable<Absence> LoadAbsences()
		{
			if(SelectedStudent == null)
				return Enumerable.Empty<Absence>();

			return SelectedStudent.Absences.Where(a=>a.SubjectId == SelectedSubject.SubjectId);
		}


		private IEnumerable<Grade> LoadGrades()
		{
			if (SelectedStudent == null)
				return Enumerable.Empty<Grade>();

			return SelectedStudent.Grades.Where(g=>g.SubjectId==SelectedSubject.SubjectId);
		}

		[ObservableProperty]
		[NotifyPropertyChangedFor(nameof(Classes))]
		[NotifyPropertyChangedFor(nameof(SelectedClassStudents))]
		[NotifyPropertyChangedFor(nameof(StudentGrades))]
		[NotifyPropertyChangedFor(nameof(Absences))]
		Subject selectedSubject;

		partial void OnSelectedSubjectChanged(Subject value)
		{
			if(value != null)
			{
				SelectedClass = null;
				Classes = new ObservableCollection<Class>(DetermineClasses());
				SelectedClassStudents = new ObservableCollection<Student>();
				Absences = new ObservableCollection<Absence>();
				StudentGrades = new ObservableCollection<Grade>();
			}
		}

		[ObservableProperty]
		[NotifyPropertyChangedFor(nameof(SelectedClassStudents))]
		[NotifyPropertyChangedFor(nameof(StudentGrades))]
		[NotifyPropertyChangedFor(nameof(Absences))]
		Class selectedClass;

		partial void OnSelectedClassChanged(Class value)
		{
			if (value != null)
			{
				SelectedClassStudents = new ObservableCollection<Student>(DetermineStudents());
				Absences = new ObservableCollection<Absence>();
				StudentGrades = new ObservableCollection<Grade>();
			}
		}

		[ObservableProperty]
		[NotifyPropertyChangedFor(nameof(StudentGrades))]
		[NotifyPropertyChangedFor(nameof(Absences))]
		Student selectedStudent;

		partial void OnSelectedStudentChanged(Student value)
		{
			if (value != null)
			{
				Absences = new ObservableCollection<Absence>(LoadAbsences());
				Absences.CollectionChanged += OnAbsencesChanged;

				StudentGrades = new ObservableCollection<Grade>(LoadGrades());
				StudentGrades.CollectionChanged += OnGradesChanged;
			}
		}

		private void OnGradesChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
            Console.WriteLine();
        }

		private void OnAbsencesChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
            Console.WriteLine();
		}

		[ObservableProperty]
		Absence selectedAbsence;

		[ObservableProperty]
		Grade selectedGrade;

		private IEnumerable<Student> DetermineStudents()
		{
			if (SelectedSubject == null || SelectedClass == null)
				return Enumerable.Empty<Student>();

			return _context.Students.Include(s=>s.Absences).Include(s=>s.Grades).Where(s => s.ClassId == SelectedClass.ClassId);
		}

		private IEnumerable<Class> DetermineClasses()
		{
			if(SelectedSubject == null)
				return Enumerable.Empty<Class>();

			return _context.ClassSubjects
				.Include(cs=>cs.Class)
				.ThenInclude(c=>c.Spec)
				.Include(cs=>cs.Subject)
				.Where(cs => cs.SubjectId == SelectedSubject.SubjectId).Select(cs => cs.Class);
		}


		[RelayCommand]
		public void OpenAddAbsence()
		{
			_navigationStore.CurrentViewModel = new AbsenceFormViewModel(this, _navigationStore); 
		}

		[RelayCommand]
		public void OpenAddGrade()
		{
			_navigationStore.CurrentViewModel = new GradeFormViewModel(this, _navigationStore);
		}

		public void LoadAbsencesFromDatabase()
		{
			OnPropertyChanged(nameof(Absences));
		}

		public void LoadGradesFromDatabase()
		{
			OnPropertyChanged(nameof(StudentGrades));
		}
	}
}
