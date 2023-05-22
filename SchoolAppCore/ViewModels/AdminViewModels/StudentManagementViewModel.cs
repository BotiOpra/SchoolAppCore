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
	public partial class StudentManagementViewModel : ObservableObject
	{
		private SchoolDbContext context;
		private readonly ModalNavigationStore _modalNavigationStore;


		public StudentManagementViewModel(ModalNavigationStore modalNavigationStore)
		{
			context = new SchoolDbContext();

			_modalNavigationStore = modalNavigationStore;
			Students = new ObservableCollection<Student>(context.Students.Include(s=>s.Class));
			Classes = new ObservableCollection<Class>(context.Classes.Include(p => p.Spec));
		}

		[ObservableProperty]
		ObservableCollection<Student> students;

		[ObservableProperty]
		ObservableCollection<Class> classes;

		[ObservableProperty]
		Class selectedClass;

		[ObservableProperty]
		[NotifyPropertyChangedFor(nameof(IsStudentSelected))]
		[NotifyCanExecuteChangedFor(nameof(DeleteCommand))]
		Student selectedStudent;

		[RelayCommand]
		public void Save()
		{
			var newStudents = Students.ToList().Except(context.Students);

			var newUsers = new List<User>(newStudents.Select(p => new User()
			{
				Email = p.Email,
				Password = "1234",
				Role = "student"
			}));

			context.Users.AddRange(newUsers);
			context.SaveChanges();

			context.UpdateRange(Students);
			context.UpdateRange(context.Users);
			context.SaveChanges();

			MessageBox.Show("Database updated");
		}

		[RelayCommand(CanExecute = nameof(IsStudentSelected))]
		public void Delete()
		{
				context.Students.Remove(SelectedStudent);
				context.SaveChanges();

				Students.Remove(SelectedStudent);
				MessageBox.Show("Removed from database");
		}

		public void UpdateFromDatabase()
		{
			Students = new ObservableCollection<Student>(context.Students.Include(s => s.Class));
		}

		public bool IsStudentSelected => SelectedStudent != null;
	}
}
