using CommunityToolkit.Mvvm.ComponentModel;
using SchoolAppCore.Models;
using SchoolAppCore.Models.BussinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace SchoolAppCore.ViewModels.EntityViewModels
{
	public partial class SubjectVM : ObservableObject
	{
		private Subject subject;
		private readonly SubjectBLL subjectLogic;

		public SubjectVM(Subject subject)
		{
			this.subject = subject;
		}

		public string SubjectName
		{
			get => subject.SubjectName;
			set
			{
				SetProperty(subject.SubjectName, value, subject, (s, n) => s.SubjectName = n);
			}
		}

		public Professor professor
		{
			get => subject.Prof;
			set
			{
				SetProperty(subject.Prof, value, subject, (s, p) => s.Prof = p);
			}
		}

		public string ProfessorName
		{
			get => professor.FirstName + " " + professor.LastName;
		}

		public int SubjectId => subject.SubjectId;
	}
}
