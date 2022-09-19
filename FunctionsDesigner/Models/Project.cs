using System.Collections.Generic;
using FunctionsDesigner.ViewModels;

namespace FunctionsDesigner.Models
{
	public class Project
	{
		public Project()
		{
			Functions = new List<Function>();
		}

		public List<Function> Functions { get; set; }

		public void AddRangeFunctions(IList<Function> functions) => Functions.AddRange(functions);

		public void AddFunction(Function function) => Functions.Add(function);
	}
}
