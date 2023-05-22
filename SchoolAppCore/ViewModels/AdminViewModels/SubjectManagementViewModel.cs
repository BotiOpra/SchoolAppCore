using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using SchoolAppCore.Models;
using SchoolAppCore.Models.DTOs;
using SchoolAppCore.Stores;
using SchoolAppCore.ViewModels.EntityViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SchoolAppCore.ViewModels.AdminViewModels
{
	public partial class SubjectManagementViewModel : ObservableObject
	{
		private readonly SchoolDbContext _context;
		private readonly ModalNavigationStore _modalNavigationStore;

		[ObservableProperty]
		[NotifyCanExecuteChangedFor(nameof(DeleteSubjectCommand))]
		[NotifyCanExecuteChangedFor(nameof(ModifySubjectCommand))]
		object selectedSubject;

		[ObservableProperty]
		ObservableCollection<SubjectVM> subjectList;

		public SubjectManagementViewModel(ModalNavigationStore modalNavigationStore)
		{
			_context = new SchoolDbContext();
			_modalNavigationStore = modalNavigationStore;

			_modalNavigationStore.CurrentViewModelChanged += OnCurrentModalViewModelChanged;

			SubjectList = new ObservableCollection<SubjectVM>
				(
					_context.Subjects.Include(s => s.Prof)
					.Select(s => new SubjectVM(s))
				);
		}

		public ObservableObject CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;

		private void OnCurrentModalViewModelChanged()
		{
			OnPropertyChanged(nameof(CurrentModalViewModel));
			OnPropertyChanged(nameof(IsModalOpen));
		}

		[RelayCommand]
		private void AddSubject()
		{
			_modalNavigationStore.CurrentViewModel = new SubjectFormViewModel(this, _context, _modalNavigationStore);
		}

		public void UpdateFromDatabase()
		{
			SubjectList = new ObservableCollection<SubjectVM>
				(
					_context.Subjects.Include(s => s.Prof)
					.Select(s => new SubjectVM(s))
				);
		}

		[RelayCommand(CanExecute = nameof(IsSubjectSelected))]
		private void DeleteSubject()
		{
			Debug.Assert(SelectedSubject != null);

			var subjectVM = SelectedSubject as SubjectVM;

			_context.Database.ExecuteSql($"RemoveSubject {subjectVM.SubjectId}");
			_context.SaveChanges();

			UpdateFromDatabase();
		}

		[RelayCommand(CanExecute = nameof(IsSubjectSelected))]
		private void ModifySubject()
		{
			var subject = SelectedSubject as SubjectVM;

			subject.SubjectName = "Kecskepasztor";
		}

		public bool IsModalOpen => _modalNavigationStore.IsOpen;

		private bool IsSubjectSelected()
		{
			return SelectedSubject != null;
		}
	}
}
