using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FunctionsDesigner.Commands;
using FunctionsDesigner.Common;
using FunctionsDesigner.Exceptions;
using FunctionsDesigner.Models;
using FunctionsDesigner.Services;
using FunctionsDesigner.Services.Interfaces;
using FunctionsDesigner.ViewModels.Base;

namespace FunctionsDesigner.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		private readonly FunctionPropertiesSelector _functionPropertiesSelector;

		private readonly IFileSystemService _fileSystemService;
		private readonly IMessageService _messageService;
		private readonly IProjectService _projectService;

		private string _filePath = string.Empty;

		public MainWindowViewModel()
		{
			_functionPropertiesSelector = new FunctionPropertiesSelector();
			_fileSystemService = new FileSystemService();
			_messageService = new MessageService();
			_projectService = new ProjectService();

			_projectService.InitializeProject();

			Project = new Project();

			AreAnyUnsavedChanges = false;

			Functions = new ObservableCollection<FunctionVm>();
			FunctionPropertiesList = new List<FunctionPropertiesSelector>();

			var notSelectedFunction = new Function("Not selected");
			notSelectedFunction.MarkAsUnused();

			var notSelectedFunctionVm = new FunctionVm(notSelectedFunction, _functionPropertiesSelector);

			Functions.Add(notSelectedFunctionVm);
			SelectedFunction = Functions.First();

			NeedShowInverseFunction = false;

			WindowClosingRequestedCommand = new RelayCommand<CancelEventArgs>(ExecuteWindowClosingRequestedCommand);
			OpenProjectCommand = new RelayCommand(ExecuteOpenProjectCommand);
			SaveProjectCommand = new RelayCommand(ExecuteSaveProjectCommand);
			AddFunctionCommand = new RelayCommand(ExecuteAddFunctionCommand);

			Chart = new ChartVm();
		}

		public ICommand WindowClosingRequestedCommand { get; }
		public ICommand AddFunctionCommand { get; }
		public ICommand OpenProjectCommand { get; }
		public ICommand SaveProjectCommand { get; }

		public bool IsUnusedFunctionSelected
		{
			get { return NotifyPropertyGet(() => IsUnusedFunctionSelected); }
			set { NotifyPropertySet(() => IsUnusedFunctionSelected, value); }
		}

		public ObservableCollection<FunctionVm> Functions
		{
			get { return NotifyPropertyGet(() => Functions); }
			set { NotifyPropertySet(() => Functions, value); }
		}

		public List<FunctionPropertiesSelector> FunctionPropertiesList { get; set; }

		public FunctionVm SelectedFunction
		{
			get { return NotifyPropertyGet(() => SelectedFunction); }
			set
			{
				NotifyPropertySet(() => SelectedFunction, value);
				CheckUnusedFunction();
			}
		}

		public bool NeedShowInverseFunction
		{
			get { return NotifyPropertyGet(() => NeedShowInverseFunction); }
			set { NotifyPropertySet(() => NeedShowInverseFunction, value); }
		}

		public ChartVm Chart
		{
			get { return NotifyPropertyGet(() => Chart); }
			set { NotifyPropertySet(() => Chart, value); }
		}

		public Project Project { get; set; }

		public bool AreAnyUnsavedChanges
		{
			get { return NotifyPropertyGet(() => AreAnyUnsavedChanges); }
			set { NotifyPropertySet(() => AreAnyUnsavedChanges, value); }
		}

		private void CheckUnusedFunction()
		{
			IsUnusedFunctionSelected = SelectedFunction.Function.FunctionType == FunctionType.Unused;
		}

		private string GenerateFunctionName()
		{
			return $"Function # {Functions.Count}";
		}

		private void ExecuteAddFunctionCommand()
		{
			_functionPropertiesSelector.GenerateStroke();

			var functionName = GenerateFunctionName();
			var function = new Function(functionName);
			var functionVm = new FunctionVm(function, _functionPropertiesSelector);

			Functions.Add(functionVm);

			Chart.Series.Add(functionVm.Series);
			SelectedFunction = functionVm;
		}

		private async void ExecuteSaveProjectCommand()
		{
			await SaveProjectAsync();
		}

		private async Task SaveProjectAsync(bool showConfirmation = true)
		{
			if (string.IsNullOrWhiteSpace(_filePath))
			{
				if (!_fileSystemService.SaveFile(GetProjectExtensionDescription(), Constants.ProjectFileExtension, out _filePath))
					return;
			}

			try
			{
				var functions = Functions.Select(function => function.Function).ToList();
				Project.AddRangeFunctions(functions);
				_projectService.SetupProjectInstance(Project);

				await _projectService.SaveActiveProjectAsync(_filePath);

				if (showConfirmation)
					_messageService.ShowMessage("Project successfully saved.");

				// ToDo: add a change check
			}
			catch (InvalidFileTypeException)
			{
				_messageService.ShowMessage("Invalid File Type.");
				_filePath = null;
			}
		}

		private static string GetProjectExtensionDescription()
		{
			var extensionDescription = $"Piecewise linear function project files (*{Constants.ProjectFileExtension})|*{Constants.ProjectFileExtension}";
			return extensionDescription;
		}

		private async void ExecuteOpenProjectCommand()
		{
			// ToDo: add a change check

			if (_fileSystemService.OpenFile(GetProjectExtensionDescription(), Constants.ProjectFileExtension, out _filePath))
			{
				try
				{
					await _projectService.LoadProjectAsync(_filePath);
				}
				catch (InvalidFileTypeException)
				{
					_messageService.ShowMessage("Invalid file type.");
				}
			}

			var loadedFunctions = _projectService.ProjectInstance.Functions;
			var functions = loadedFunctions.Select(function => new FunctionVm(function, _functionPropertiesSelector));
			Functions = new ObservableCollection<FunctionVm>(functions);

			SelectedFunction = Functions.ElementAt(1);
			foreach (var function in Functions)
				Chart.Series.Add(function.Series);
		}

		private async void ExecuteWindowClosingRequestedCommand(CancelEventArgs eventArgs)
		{
			if (!AreAnyUnsavedChanges)
				return;

			var message = "Close without saving?";
			var caption = "Piecewise linear functions designer";

			if (_messageService.ActionConfirmed(message, caption))
				await SaveProjectAsync(false);
			else
				eventArgs.Cancel = true;
		}
	}
}
