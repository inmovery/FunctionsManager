using System.Threading.Tasks;
using FunctionsDesigner.Models;

namespace FunctionsDesigner.Services.Interfaces
{
	public interface IProjectService
	{
		Project ProjectInstance { get; }

		void InitializeProject();

		void SetupProjectInstance(Project project);

		Task LoadProjectAsync(string filePath);

		Task SaveProjectAsync(string filePath);
	}
}
