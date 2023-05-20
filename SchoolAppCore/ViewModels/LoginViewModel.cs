using CommunityToolkit.Mvvm.ComponentModel;
using SchoolAppCore.Commands;
using SchoolAppCore.Services;
using SchoolAppCore.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchoolAppCore.ViewModels
{
	public class LoginViewModel : ObservableObject
	{
		public ICommand LoginCommand { get; set; }

		public LoginViewModel(NavigationStore navigationStore)
		{
			//LoginCommand = new LoginCommand(new NavigationService(navigationStore, () => new StudentPageViewModel(navigationStore)));
		}
	}
}
