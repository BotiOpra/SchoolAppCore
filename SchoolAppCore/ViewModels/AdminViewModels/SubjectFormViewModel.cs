using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using SchoolAppCore.Models;
using SchoolAppCore.Stores;
using SchoolAppCore.ViewModels.EntityViewModels;
using System.Collections.ObjectModel;
using System.Linq;

namespace SchoolAppCore.ViewModels.AdminViewModels
{
	public partial class SubjectFormViewModel : ObservableObject
	{
		private SchoolDbContext context;
		private ModalNavigationStore modalNavigationStore;
		private SubjectManagementViewModel subjectManagementViewModel;

		public SubjectFormViewModel(SubjectManagementViewModel subjectManagementViewModel, SchoolDbContext context, ModalNavigationStore modalNavigationStore)
		{
			this.context = context;
			this.modalNavigationStore = modalNavigationStore;
			this.subjectManagementViewModel = subjectManagementViewModel;

			Professors = new ObservableCollection<ProfessorVM>(context.Professors.Select(p => new ProfessorVM(p)).ToList());
		}

		[ObservableProperty]
		string subjectName;

		[ObservableProperty]
		ObservableCollection<ProfessorVM> professors;

		[ObservableProperty]
		ProfessorVM selectedProfessor;

		[RelayCommand]
		void AddSubject()
		{
			context.Database.ExecuteSql($"InsertSubject {subjectName}, {selectedProfessor.Id}");
			subjectManagementViewModel.UpdateFromDatabase();

			modalNavigationStore.CloseModal();
		}

		[RelayCommand]
		void Cancel()
		{
			modalNavigationStore.CloseModal();
		}
	}
}