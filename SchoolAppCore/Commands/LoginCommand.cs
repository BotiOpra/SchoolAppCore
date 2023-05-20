using SchoolAppCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SchoolAppCore.Commands
{
	public class LoginCommand : CommandBase
	{
		private NavigationService _navigationService;

		public LoginCommand(NavigationService navigationService)
		{
			_navigationService = navigationService;
		}

		public override void Execute(object parameter)
		{
			MessageBox.Show("Logged in... Navigating");

			_navigationService.Navigate();
		}
	}
}
