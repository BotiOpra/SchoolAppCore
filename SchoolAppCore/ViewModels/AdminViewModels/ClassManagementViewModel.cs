using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
using System.Windows;

namespace SchoolAppCore.ViewModels.AdminViewModels
{
	public partial class ClassManagementViewModel : ObservableObject
	{
		private SchoolDbContext _context;
		private readonly ModalNavigationStore _modalNavigationStore;

		public ClassManagementViewModel(ModalNavigationStore modalNavigationStore)
		{
			_modalNavigationStore = modalNavigationStore;
			_context = new SchoolDbContext();

			_modalNavigationStore.CurrentViewModelChanged += OnCurrentModalViewModelChanged;

			Classes = new ObservableCollection<ClassVM>(
				_context.Classes.Include(c => c.Prof)
				.Include(c => c.Spec)
				.Include(c => c.ClassSubjects)
				.Select(c => new ClassVM(c)).ToList());

			Specializations = new ObservableCollection<Specialization>(_context.Specializations);
			Professors = new ObservableCollection<ProfessorVM>(_context.Professors.Select(p => new ProfessorVM(p)));
			ClassSubjects = new ObservableCollection<ClassSubject>();
		}

		public bool IsModalOpen => _modalNavigationStore.IsOpen;

		public ObservableObject CurrentModal => _modalNavigationStore.CurrentViewModel;

		private void OnCurrentModalViewModelChanged()
		{
			OnPropertyChanged(nameof(CurrentModal));
			OnPropertyChanged(nameof(IsModalOpen));
		}

		[ObservableProperty]
		ObservableCollection<ClassVM> classes;

		[ObservableProperty]
		ObservableCollection<Specialization> specializations;

		[ObservableProperty]
		ObservableCollection<ProfessorVM> professors;

		[ObservableProperty]
		ObservableCollection<ClassSubject> classSubjects;

		[ObservableProperty]
		[NotifyPropertyChangedFor(nameof(IsSubjectSelected))]
		[NotifyCanExecuteChangedFor(nameof(RemoveSubjectFromClassCommand))]
		ClassSubject selectedClassSubject;

		[ObservableProperty]
		[NotifyPropertyChangedFor(nameof(SelectedProfessor))]
		[NotifyPropertyChangedFor(nameof(SelectedSpecialization))]
		[NotifyPropertyChangedFor(nameof(ClassSubjects))]
		[NotifyPropertyChangedFor(nameof(IsClassSelected))]
		[NotifyCanExecuteChangedFor(nameof(AddSubjectCommand))]
		ClassVM selectedClass;

		partial void OnSelectedClassChanged(ClassVM? oldValue, ClassVM newValue)
		{
			if (newValue != null)
				ClassSubjects = new ObservableCollection<ClassSubject>(_context.ClassSubjects
				.Include(cs => cs.Class)
				.Include(cs => cs.Subject)
				.Where(cs => cs.ClassId == SelectedClass.Id));

			else
				ClassSubjects = new ObservableCollection<ClassSubject>();
		}

		public ProfessorVM SelectedProfessor
		{
			get
			{
				return Professors.Where(p => p.Id == SelectedClass?.Supervisor.Id).FirstOrDefault();
			}
		}

		public Specialization SelectedSpecialization
		{
			get => Specializations.Where(s => s.SpecId == SelectedClass?.Specialization.SpecId).FirstOrDefault();
		}

		[RelayCommand]
		public void AddClass()
		{
			_modalNavigationStore.CurrentViewModel = new ClassFormViewModel(this, _modalNavigationStore);
		}

		[RelayCommand(CanExecute = nameof(IsClassSelected))]
		public void AddSubject()
		{
			_modalNavigationStore.CurrentViewModel = new ClassSubjectFormViewModel(this, _modalNavigationStore);
		}

		[RelayCommand(CanExecute = nameof(IsSubjectSelected))]
		public void RemoveSubjectFromClass()
		{
			_context.Database.ExecuteSql($"RemoveSubjectFromClass {SelectedClassSubject.SubjectId}, {SelectedClass.Id}");
			MessageBox.Show("Subject removed from class");

			UpdateSubjectsFromDatabase();
		}

		public void UpdateClassesFromDatabase()
		{
			Classes = new ObservableCollection<ClassVM>(
				_context.Classes.Include(c => c.Prof)
				.Include(c => c.Spec)
				.Include(c => c.ClassSubjects)
				.Select(c => new ClassVM(c)).ToList());
		}

		public bool IsClassSelected => SelectedClass != null;
		public bool IsSubjectSelected => SelectedClassSubject != null;

		public void UpdateSubjectsFromDatabase()
		{
			ClassSubjects = new ObservableCollection<ClassSubject>(_context.ClassSubjects
					.Include(cs => cs.Class)
					.Include(cs => cs.Subject)
					.Where(cs => cs.ClassId == SelectedClass.Id));
		}
	}
}
