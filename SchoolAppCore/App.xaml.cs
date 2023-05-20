using Microsoft.Extensions.DependencyInjection;
using SchoolAppCore.Services;
using SchoolAppCore.Stores;
using SchoolAppCore.ViewModels;
using SchoolAppCore.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SchoolAppCore
{
	public partial class App : Application
	{
		private readonly IServiceProvider _serviceProvider;

		public App()
		{
			IServiceCollection services = new ServiceCollection();

			services.AddSingleton<NavigationStore>();
			services.AddSingleton<MainViewModel>();
			services.AddSingleton<AdminNavigationStore>();
			services.AddSingleton<NavigationService>(s => CreateInitialNavigationService(s));

			services.AddTransient<LoginViewModel>(CreateLoginViewModel);
			services.AddTransient<StudentPageViewModel>(CreateStudentViewModel);
			services.AddTransient<TeacherPageViewModel>(CreateTeacherViewModel);
			services.AddTransient<AdminPageViewModel>(CreateAdminPageViewModel);

			services.AddSingleton<MainWindow>(s => new MainWindow() 
			{
				DataContext = s.GetRequiredService<MainViewModel>()
			});

			_serviceProvider = services.BuildServiceProvider();
		}

		private AdminPageViewModel CreateAdminPageViewModel(IServiceProvider serviceProvider)
		{
			return new AdminPageViewModel(
				serviceProvider.GetRequiredService<NavigationStore>(),
				serviceProvider.GetRequiredService<AdminNavigationStore>());
		}

		private TeacherPageViewModel CreateTeacherViewModel(IServiceProvider serviceProvider)
		{
			return new TeacherPageViewModel(serviceProvider.GetRequiredService<AdminNavigationStore>());
		}

		private StudentPageViewModel CreateStudentViewModel(IServiceProvider serviceProvider)
		{
			return new StudentPageViewModel(serviceProvider.GetRequiredService<AdminNavigationStore>());
		}

		private LoginViewModel CreateLoginViewModel(IServiceProvider serviceProvider)
		{
			return new LoginViewModel(serviceProvider.GetRequiredService<NavigationStore>());
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
			NavigationService initialNavigationService = _serviceProvider.GetRequiredService<NavigationService>();
			initialNavigationService.Navigate();

			MainWindow.Show();

			base.OnStartup(e);
		}

		private NavigationService CreateInitialNavigationService(IServiceProvider serviceProvider)
		{
			return new NavigationService(
				serviceProvider.GetRequiredService<NavigationStore>(), 
				() => serviceProvider.GetRequiredService<AdminPageViewModel>());
		}
	}
}
