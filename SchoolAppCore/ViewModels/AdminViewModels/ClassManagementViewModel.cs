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
			Professors = new ObservableCollection<ProfessorVM>(
						Classes.Select(c=>c.Supervisor)
					);

			
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
		ClassVM selectedClass;

		[RelayCommand]
		public void AddClass()
		{
			_modalNavigationStore.CurrentViewModel = new ClassFormViewModel(this, _modalNavigationStore);
		}

		public void UpdateFromDatabase()
		{
			Classes = new ObservableCollection<ClassVM>(
				_context.Classes.Include(c => c.Prof)
				.Include(c => c.Spec)
				.Include(c => c.ClassSubjects)
				.Select(c => new ClassVM(c)).ToList());
		}
	}
}
