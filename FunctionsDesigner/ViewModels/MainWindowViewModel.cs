using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FunctionsDesigner.Commands;
using FunctionsDesigner.Common;
using FunctionsDesigner.Exceptions;
using FunctionsDesigner.Models;
using FunctionsDesigner.Models.Interfaces;
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
		private readonly IClipboardService _clipboardService;

		private string _filePath = string.Empty;

		public MainWindowViewModel()
		{
			_fileSystemService = new FileSystemService();
			_messageService = new MessageService();
			_projectService = new ProjectService();
			_clipboardService = new ClipboardService();

			_functionPropertiesSelector = new FunctionPropertiesSelector();

			_projectService.InitializeProject();

			Project = new Project();

			FunctionPropertiesList = new List<FunctionPropertiesSelector>();
			Functions = new ObservableCollection<FunctionVm>();
			Functions.CollectionChanged += OnFunctionsCollectionChanged;

			var notSelectedFunction = new Function("Not selected");
			notSelectedFunction.MarkAsUnused();

			var notSelectedFunctionVm = new FunctionVm(notSelectedFunction, _functionPropertiesSelector);

			Functions.Add(notSelectedFunctionVm);
			SelectedFunction = Functions.First();

			NeedShowInverseFunction = false;
			AreAnyUnsavedChanges = false;

			WindowClosingRequestedCommand = new RelayCommand<CancelEventArgs>(ExecuteWindowClosingRequestedCommand);
			OpenProjectCommand = new RelayCommand(ExecuteOpenProjectCommand);
			SaveProjectCommand = new RelayCommand(ExecuteSaveProjectCommand);
			AddFunctionCommand = new RelayCommand(ExecuteAddFunctionCommand);
			RemoveFunctionCommand = new RelayCommand(ExecuteRemoveFunctionCommand);
			InsertFromClipboardCommand = new RelayCommand(ExecuteInsertFromClipboardCommand);
			CopyToClipboardCommand = new RelayCommand(ExecuteCopyToClipboardCommand);

			Chart = new ChartVm();
		}

		public ICommand WindowClosingRequestedCommand { get; }
		public ICommand AddFunctionCommand { get; }
		public ICommand RemoveFunctionCommand { get; }
		public ICommand OpenProjectCommand { get; }
		public ICommand SaveProjectCommand { get; }
		public ICommand InsertFromClipboardCommand { get; }
		public ICommand CopyToClipboardCommand { get; }

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
			if (Functions.Count < 2)
				return $"Function # {Functions.Count}";

			var maxFunctionNumber = Functions.Skip(1).Select(function =>
			{
				var functionName = function.Function.Name;
				var functionNumber = functionName.Split(new[] { ' ' }).Last();

				return int.Parse(functionNumber);
			}).Max();

			return $"Function # {++maxFunctionNumber}";
		}

		private void ExecuteAddFunctionCommand()
		{
			_functionPropertiesSelector.GenerateStroke();

			var functionName = GenerateFunctionName();
			var function = new Function(functionName);
			var functionVm = new FunctionVm(function, _functionPropertiesSelector);
			functionVm.FunctionChanged += OnFunctionChanged;

			Functions.Add(functionVm);

			Chart.Series.Add(functionVm.Series);
			SelectedFunction = functionVm;
		}

		private void OnFunctionChanged(object sender, EventArgs eventArgs) => AreAnyUnsavedChanges = true;

		private void OnFunctionsCollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs) => AreAnyUnsavedChanges = true;

		private void ExecuteRemoveFunctionCommand()
		{
			SelectedFunction.Dispose();
			Chart.Series.Remove(SelectedFunction.Series);
			Functions.Remove(SelectedFunction);

			var isFunctionsExists = Functions.Skip(1).Any();
			SelectedFunction = isFunctionsExists ? Functions.Skip(1).First() : Functions.First();

			Chart.Reinitialize();
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

			AreAnyUnsavedChanges = false;
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
			var functions = loadedFunctions.Select(function =>
			{
				_functionPropertiesSelector.GenerateStroke();
				var functionVm = new FunctionVm(function, _functionPropertiesSelector);
				functionVm.FunctionChanged += OnFunctionChanged;

				return functionVm;
			});
			Functions = new ObservableCollection<FunctionVm>(functions);
			Functions.CollectionChanged += OnFunctionsCollectionChanged;

			SelectedFunction = Functions.ElementAt(1);

			foreach (var function in Functions)
				Chart.Series.Add(function.Series);

			AreAnyUnsavedChanges = false;
		}

		private void ExecuteInsertFromClipboardCommand()
		{
			// ToDo: add another file type handling (except with Excel table structure)
			var textFromClipboard = _clipboardService.GetText();
			var pointTextModels = textFromClipboard.Split(new[] { "\r\n" }, StringSplitOptions.None);
			try
			{
				// ToDo: add an exceptions check
				foreach (var pointTextModel in pointTextModels)
				{
					if (string.IsNullOrWhiteSpace(pointTextModel))
						continue;

					var dividedValues = pointTextModel.Split(new[] { "\t" }, StringSplitOptions.None);

					var xValue = int.Parse(dividedValues[0]);
					var yValue = int.Parse(dividedValues[1]);
					var point = new PointVm(xValue, yValue);

					SelectedFunction.Function.Add(point);
				}
			}
			catch (Exception)
			{
				// ToDo: add custom exception handling
				_messageService.ShowMessage("Invalid clipboard source format.");
			}
		}

		private void ExecuteCopyToClipboardCommand()
		{
			var functionTextValue = new StringBuilder();
			foreach (var point in SelectedFunction.Function.Points)
			{
				var textPoint = $"{point.X}\t{point.Y}\r\n";
				functionTextValue.Append(textPoint);
			}

			_clipboardService.SetText(functionTextValue.ToString());
		}

		private async void ExecuteWindowClosingRequestedCommand(CancelEventArgs eventArgs)
		{
			if (!AreAnyUnsavedChanges)
				return;

			var message = "Do you wanna save the changes?";
			var caption = "Piecewise linear functions designer";

			if (_messageService.ActionConfirmed(message, caption))
				await SaveProjectAsync(false);

			eventArgs.Cancel = false;
		}
	}
}
