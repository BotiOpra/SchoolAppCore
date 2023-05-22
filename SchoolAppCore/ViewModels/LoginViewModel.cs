using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolAppCore.Commands;
using SchoolAppCore.Models;
using SchoolAppCore.Services;
using SchoolAppCore.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SchoolAppCore.ViewModels
{
	public partial class LoginViewModel : ObservableObject
	{
		private readonly NavigationStore _navigationStore;
		private readonly SchoolDbContext _context;

		public LoginViewModel(NavigationStore navigationStore)
		{
			_navigationStore = navigationStore;

			_context = new SchoolDbContext();
		}

		[ObservableProperty]
		[NotifyPropertyChangedFor(nameof(CanLogin))]
		[NotifyCanExecuteChangedFor(nameof(LoginCommand))]
		string username;

		[ObservableProperty]
		[NotifyPropertyChangedFor(nameof(CanLogin))]
		[NotifyCanExecuteChangedFor(nameof(LoginCommand))]
		string password;

		[RelayCommand(CanExecute = nameof(CanLogin))]
		public void Login()
		{
			// authenticate
			User? user = null;

				user = _context.Users.Where(
					u => u.Email == Username && u.Password == Password).FirstOrDefault();

				if (user == null)
				{
					MessageBox.Show("Invalid credentials");
					return;
				}


				MessageBox.Show($"Welcome, {user.Role}!");
				if (user.Role == "professor")
				{
					Professor professor = _context.Professors.Include(p => p.EmailNavigation).Where(p => p.Email == Username).First();

					_navigationStore.CurrentViewModel = new TeacherPageViewModel(professor, _navigationStore);
				}
				else if (user.Role == "student")
				{
					Student student = _context.Students
					.Include(s => s.EmailNavigation)
					.Include(s => s.Class)
					.Include(s => s.Grades)
					.Include(s => s.Absences)
					.Where(s => s.Email == Username).First();

					_navigationStore.CurrentViewModel = new StudentPageViewModel(student, _navigationStore);
				}
		}

		public bool CanLogin => !Username.IsNullOrEmpty() && !Password.IsNullOrEmpty();
	}
}
