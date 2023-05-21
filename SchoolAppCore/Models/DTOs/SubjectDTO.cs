using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppCore.Models.DTOs
{
	public class SubjectDTO
	{
		public string SubjectName {get; set;}
		public string ProfessorFirstName { get; set;}
		public string ProfessorLastName { get; set;}

		public SubjectDTO() { }

		public SubjectDTO(string subjectName, string professorFirstName, string professorLastName)
		{
			SubjectName = subjectName;
			ProfessorFirstName = professorFirstName;
			ProfessorLastName = professorLastName;
		}
	}
}
