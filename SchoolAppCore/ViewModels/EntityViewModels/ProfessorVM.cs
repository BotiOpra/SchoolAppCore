using CommunityToolkit.Mvvm.ComponentModel;
using SchoolAppCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SchoolAppCore.ViewModels.EntityViewModels
{
	public partial class ProfessorVM : ObservableObject
	{
		private readonly Professor _professor;

		public ProfessorVM(Professor professor)
		{
			_professor = professor;
		}

		public int Id => _professor.ProfId;

		public string FirstName
		{
			get => _professor.FirstName;
			set
			{
				SetProperty(_professor.FirstName, value, _professor, (p, n) => p.FirstName = n);
			}
		}

		public string LastName
		{
			get => _professor.LastName;
			set
			{
				SetProperty(_professor.LastName, value, _professor, (p, n) => p.LastName = n);
			}
		}

		public string FullName => FirstName + ' ' + LastName;
	}
}
