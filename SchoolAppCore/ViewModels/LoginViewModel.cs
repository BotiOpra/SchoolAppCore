using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolAppCore.Commands;
using SchoolAppCore.Models;
using SchoolAppCore.Services;
using SchoolAppCore.Stores;
using SchoolAppCore.ViewModels.EntityViewModels;
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
		private readonly ModalNavigationStore _modalNavigationStore;
		private readonly AdminNavigationStore _adminNavigation;
		private readonly SchoolDbContext _context;

		public LoginViewModel(NavigationStore navigationStore, ModalNavigationStore modal, AdminNavigationStore adminNavigation)
		{
			_navigationStore = navigationStore;
			_modalNavigationStore = modal;
			_adminNavigation = adminNavigation;

			_context = new SchoolDbContext();
			_adminNavigation = adminNavigation;
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
			if (Username == "admin")
			{
				_navigationStore.CurrentViewModel = new AdminPageViewModel(_adminNavigation, _modalNavigationStore);
				return;
			}
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
				Professor professor = _context.Professors.Include(p => p.EmailNavigation).Include(p => p.Subjects).Where(p => p.Email == Username).First();

				_navigationStore.CurrentViewModel = new TeacherPageViewModel(professor, _modalNavigationStore);
			}
			else if(user.Role == "headteacher")
			{
				HeadTeacher headteacher = _context.HeadTeachers
					.Include(t => t.EmailNavigation)
					.Include(t => t.Class)
					.ThenInclude(c=>c.Students)
					.ThenInclude(s=>s.Absences)
					.Include(t2=>t2.Class)
					.ThenInclude(c2=>c2.Students)
					.ThenInclude(s2=>s2.Grades)
					.ThenInclude(g=>g.Subject)
					.Where(t => t.Email == Username).First();

				_navigationStore.CurrentViewModel = new HeadTeacherViewModel(headteacher, _modalNavigationStore);
			}
			else if (user.Role == "student")
			{
				Student student = _context.Students
				.Include(s => s.EmailNavigation)
				.Include(s => s.Class)
				.Include(s => s.Grades)
				.Include(s => s.Absences)
				.Where(s => s.Email == Username).First();

				_navigationStore.CurrentViewModel = new StudentPageViewModel(student, _modalNavigationStore);
			}
		}

		public bool CanLogin => !Username.IsNullOrEmpty() && !Password.IsNullOrEmpty();
	}
}
