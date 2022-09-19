using System;
using System.IO;
using System.Threading.Tasks;
using FunctionsDesigner.Common;
using FunctionsDesigner.Exceptions;
using FunctionsDesigner.Models;
using FunctionsDesigner.Services.Interfaces;
using Newtonsoft.Json;

namespace FunctionsDesigner.Services
{
	public class ProjectService : IProjectService
	{
		public Project ProjectInstance { get; private set; }

		public void InitializeProject()
		{
			ProjectInstance = new Project();
		}

		public void SetupProjectInstance(Project project)
		{
			ProjectInstance = project;
		}

		public async Task LoadProjectAsync(string filePath)
		{
			if (string.IsNullOrWhiteSpace(filePath))
				throw new ArgumentException(nameof(filePath));

			if (!Constants.ProjectFileExtension.Equals(Path.GetExtension(filePath), StringComparison.OrdinalIgnoreCase))
				throw new InvalidFileTypeException($"Only '{Constants.ProjectFileExtension}' files are supported.");

			if (!File.Exists(filePath))
				throw new InvalidOperationException("The path is invalid.");

			if (!File.Exists(filePath))
				throw new InvalidOperationException("The active project file path is invalid.");

			try
			{
				var serializerSettings = new JsonSerializerSettings()
				{
					ReferenceLoopHandling = ReferenceLoopHandling.Serialize
				};

				var projectContent = await File.ReadAllTextAsync(filePath);
				ProjectInstance = JsonConvert.DeserializeObject<Project>(projectContent);
			}
			catch (JsonReaderException exception)
			{
				throw new InvalidFileTypeException(exception.Message);
			}
		}

		public Task SaveActiveProjectAsync(string filePath)
		{
			if (ProjectInstance == null)
				throw new InvalidOperationException("The active project is not specified.");

			if (string.IsNullOrWhiteSpace(filePath))
				throw new ArgumentException(nameof(filePath));

			if (!Constants.ProjectFileExtension.Equals(Path.GetExtension(filePath), StringComparison.OrdinalIgnoreCase))
				throw new InvalidFileTypeException($"Only '{Constants.ProjectFileExtension}' files are supported");

			var serializerSettings = new JsonSerializerSettings()
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Serialize
			};

			var projectContent = JsonConvert.SerializeObject(ProjectInstance, serializerSettings);
			return File.WriteAllTextAsync(filePath, projectContent);
		}
	}
}
