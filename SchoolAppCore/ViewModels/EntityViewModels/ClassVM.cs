using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.VisualBasic;
using SchoolAppCore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppCore.ViewModels.EntityViewModels
{
	public partial class ClassVM : ObservableObject
	{
		private readonly Class _class;
		public ClassVM(Class @class)
		{
			_class = @class;

			_supervisor = new ProfessorVM(_class.Prof);
			ClassSubjects = new ObservableCollection<ClassSubject>(_class.ClassSubjects);
		}

		public int Id => _class.ClassId;

		public string Year => _class.ClassYear.ToString();

		private ProfessorVM _supervisor;
		public ProfessorVM Supervisor
		{
			get => _supervisor;
			set
			{
				_supervisor = value;
				OnPropertyChanged(nameof(Supervisor));
			}
		}

		public Specialization Specialization => _class.Spec;

		public ObservableCollection<ClassSubject> ClassSubjects;

		public string ClassName => Year + '/' + Specialization.SpecName;
	}
}
