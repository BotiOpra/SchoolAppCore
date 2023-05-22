using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolAppCore.Models;
using SchoolAppCore.Stores;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace SchoolAppCore.ViewModels.AdminViewModels
{
	public partial class SpecializationManagementViewModel : ObservableObject
	{
		private ModalNavigationStore modalNavigationStore;
		private SchoolDbContext context;

		public SpecializationManagementViewModel(ModalNavigationStore modalNavigationStore)
		{
			this.modalNavigationStore = modalNavigationStore;

			context = new SchoolDbContext();

			Specializations = new ObservableCollection<Specialization>(context.Specializations.Include(s=>s.Classes));
		}

		[ObservableProperty]
		ObservableCollection<Specialization> specializations;

		[ObservableProperty]
		[NotifyPropertyChangedFor(nameof(IsSpecializationSelected))]
		[NotifyCanExecuteChangedFor(nameof(DeleteCommand))]
		Specialization selectedSpecialization;

		[RelayCommand]
		public void Save()
		{
			var newSpecializations = Specializations.ToList().Except(context.Specializations);

			context.AddRange(newSpecializations);
			context.SaveChanges();

			MessageBox.Show("Database updated");

		}

		[RelayCommand(CanExecute =nameof(IsSpecializationSelected))]
		public void Delete()
		{
			if (SelectedSpecialization.Classes.IsNullOrEmpty())
			{

				context.Specializations.Remove(SelectedSpecialization);
				context.SaveChanges();

				Specializations.Remove(SelectedSpecialization);
				MessageBox.Show("Removed from database");
			}
			else
				MessageBox.Show("Specialization added for class. First remove class");
		}

		public void UpdateFromDatabase()
		{
			Specializations = new ObservableCollection<Specialization>(context.Specializations.Include(s => s.Classes));
		}

		public bool IsSpecializationSelected => SelectedSpecialization != null;
	}
}