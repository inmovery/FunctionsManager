using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using FunctionsDesigner.Commands;
using FunctionsDesigner.Models;
using FunctionsDesigner.ViewModels.Base;

namespace FunctionsDesigner.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		public MainWindowViewModel()
		{
			Functions = new ObservableCollection<FunctionVm>();

			var notSelectedFunction = new Function("Not selected");
			notSelectedFunction.MarkAsUnused();

			var notSelectedFunctionVm = new FunctionVm(notSelectedFunction);
			Functions.Add(notSelectedFunctionVm);
			SelectedFunction = Functions.First();

			NeedShowInverseFunction = false;

			WindowClosingRequestedCommand = new RelayCommand<CancelEventArgs>(ExecuteWindowClosingRequestedCommand);
			SaveProjectCommand = new RelayCommand(ExecuteSaveProjectCommand);
			AddFunctionCommand = new RelayCommand(ExecuteAddFunctionCommand);

			Chart = new ChartVm();
			// Chart.Series.Add(firstFunctionVm.Series);
		}

		public ICommand WindowClosingRequestedCommand { get; }
		public ICommand SaveProjectCommand { get; }
		public ICommand AddFunctionCommand { get; }

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
			var functionName = GenerateFunctionName();
			var function = new Function(functionName);
			var functionVm = new FunctionVm(function);

			Functions.Add(functionVm);
			Chart.Series.Add(functionVm.Series);
			SelectedFunction = functionVm;
		}

		private void ExecuteSaveProjectCommand()
		{
			MessageBox.Show("Saving ...");
		}

		private void ExecuteWindowClosingRequestedCommand(CancelEventArgs eventArgs)
		{
			//eventArgs.Cancel = true;
			//RequestShutdown();

			string msg = "Close without saving?";
			MessageBoxResult result =
				MessageBox.Show(
					msg,
					"Data App",
					MessageBoxButton.YesNo,
					MessageBoxImage.Warning);

			if (result == MessageBoxResult.No)
			{
				eventArgs.Cancel = true;
			}
		}
	}
}
