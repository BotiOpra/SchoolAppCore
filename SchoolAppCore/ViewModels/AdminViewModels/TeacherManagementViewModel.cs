using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
	public partial class TeacherManagementViewModel : ObservableObject
	{
		private ModalNavigationStore modalNavigationStore;
		private SchoolDbContext context;

		public TeacherManagementViewModel(ModalNavigationStore modalNavigationStore)
		{
			this.modalNavigationStore = modalNavigationStore;

			context = new SchoolDbContext();

			Professors = new ObservableCollection<Professor>(context.Professors.Include(p => p.Class).Include(p => p.Subjects));
		}

		[ObservableProperty]
		ObservableCollection<Professor> professors;

		[ObservableProperty]
		[NotifyPropertyChangedFor(nameof(IsProfessorSelected))]
		[NotifyCanExecuteChangedFor(nameof(DeleteCommand))]
		Professor selectedProfessor;

		[RelayCommand]
		public void Save()
		{
			var newProfessors = Professors.ToList().Except(context.Professors);

			var newUsers = new List<User>(newProfessors.Select(p => new User()
			{
				Email = p.Email,
				Password = "1234",
				Role = "professor"
			}));

			context.Users.AddRange(newUsers);
			context.SaveChanges();

			context.UpdateRange(Professors);
			context.UpdateRange(context.Users);
			context.SaveChanges();

			MessageBox.Show("Database updated");

		}

		[RelayCommand(CanExecute = nameof(IsProfessorSelected))]
		public void Delete()
		{
			if (SelectedProfessor.Class != null)
			{
				MessageBox.Show("Professor is Supervisor. First remove class");
				return;
			}
			if (!SelectedProfessor.Subjects.IsNullOrEmpty())
			{
				MessageBox.Show("Professor has subject. First remove subject");
				return;
			}

			context.Professors.Remove(SelectedProfessor);
			context.SaveChanges();

			Professors.Remove(SelectedProfessor);
			MessageBox.Show("Removed from database");

		}

		public void UpdateFromDatabase()
		{
			Professors = new ObservableCollection<Professor>(context.Professors.Include(p => p.Class).Include(p => p.Subjects));
		}

		public bool IsProfessorSelected => SelectedProfessor != null;
	}
}
