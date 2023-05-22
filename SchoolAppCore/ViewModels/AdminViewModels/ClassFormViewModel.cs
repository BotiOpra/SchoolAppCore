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
using System.Windows.Media.Animation;

namespace SchoolAppCore.ViewModels.AdminViewModels
{
	public partial class ClassFormViewModel : ObservableObject
	{
		private readonly ClassManagementViewModel viewModel;
		private readonly ModalNavigationStore navigation;

		public ClassFormViewModel(ClassManagementViewModel viewModel, ModalNavigationStore navigation)
		{
			this.viewModel = viewModel;
			this.navigation = navigation;

			using (var context = new SchoolDbContext())
			{
				Specializations = new ObservableCollection<Specialization>(context.Specializations);
				Professors = new ObservableCollection<ProfessorVM>(
						context.Professors
						.Include(p => p.Class)
						.Where(p => p.Class == null).Select(p => new ProfessorVM(p))
					);
			}
		}

		public ObservableCollection<Specialization> Specializations { get; private set; }
		public ObservableCollection<ProfessorVM> Professors { get; private set; }

		[ObservableProperty]
		string year;

		[ObservableProperty]
		Specialization selectedSpecialization;

		[ObservableProperty]
		ProfessorVM selectedSupervisor;

		[RelayCommand]
		public void AddClass()
		{
			try
			{
				using (var context = new SchoolDbContext())
				{
					context.Database.ExecuteSql($"InsertClass {year}, {selectedSpecialization.SpecId}, {selectedSupervisor.Id}");

				}

			}
			catch (Exception e)
			{
				MessageBox.Show("Error: " + e);
			}
			finally
			{
				viewModel.UpdateClassesFromDatabase();

				navigation.CloseModal();
			}
		}

		[RelayCommand]
		public void Cancel()
		{
			navigation.CloseModal();
		}

	}
}
