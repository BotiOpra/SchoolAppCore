using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAppCore.Models.BussinessLogicLayer
{
	public interface BLLBase 
	{
		public void AddMethod(object obj);
		public void RemoveMethod(object obj);
		public void ModifyMethod(object obj);

		public void GetAllMethod(object obj);

	}
}
