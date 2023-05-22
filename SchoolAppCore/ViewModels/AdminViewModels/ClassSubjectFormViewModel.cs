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
	public partial class ClassSubjectFormViewModel : ObservableObject
	{
		private ClassManagementViewModel classManagementViewModel;
		private ModalNavigationStore modalNavigationStore;

		private SchoolDbContext context;

		private ClassVM SelectedClass => classManagementViewModel.SelectedClass;

		public ClassSubjectFormViewModel(ClassManagementViewModel classManagementViewModel, ModalNavigationStore modalNavigationStore)
		{
			this.classManagementViewModel = classManagementViewModel;
			this.modalNavigationStore = modalNavigationStore;
			context = new SchoolDbContext();

			var subjectIds = context.ClassSubjects
				.Include(cs => cs.Class)
				.Include(cs => cs.Subject)
				.Where(cs => cs.ClassId == SelectedClass.Id)
				.Select(cs => cs.Subject);

			var allSubjects = context.Subjects;

			PossibleSubjects = new ObservableCollection<SubjectVM>(allSubjects.Except(subjectIds).Select(s => new SubjectVM(s)));
		}

		[ObservableProperty]
		ObservableCollection<SubjectVM> possibleSubjects;

		[ObservableProperty]
		SubjectVM selectedSubject;

		[ObservableProperty]
		bool hasFinalTest;

		[RelayCommand]
		public void AddSubject()
		{
			if (selectedSubject != null)
			{
				SelectedClass.AddSubject(selectedSubject, HasFinalTest);
				MessageBox.Show("Subject added");
			}

			classManagementViewModel.UpdateSubjectsFromDatabase();
			modalNavigationStore.CloseModal();
		}

		[RelayCommand]
		public void Cancel()
		{
			modalNavigationStore.CloseModal();
		}
	}
}
