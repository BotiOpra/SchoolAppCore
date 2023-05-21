using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppCore.Models.BussinessLogicLayer
{
	public class SubjectBLL : BLLBase
	{
		private readonly SchoolDbContext dbContext;

		private readonly List<Subject> subjects;
		
		public IEnumerable<Subject> Subjects => subjects;

		public SubjectBLL()
		{
			dbContext = new SchoolDbContext();

			subjects = new List<Subject>();
		}

		public void AddMethod(object obj)
		{
			Subject? subject = obj as Subject;

			if (subject != null)
			{
				Subject addedSubject = dbContext.Subjects.Add(subject).Entity;
				dbContext.SaveChanges();
				subjects.Add(addedSubject);
			}
		}

		public void RemoveMethod(object obj)
		{
			throw new NotImplementedException();
		}

		public void ModifyMethod(object obj)
		{
			throw new NotImplementedException();
		}

		public void GetAllMethod(object obj)
		{
			throw new NotImplementedException();
		}
	}
}
