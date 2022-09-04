using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using FunctionsDesigner.Commands;
using FunctionsDesigner.Extensions;
using FunctionsDesigner.Models;
using FunctionsDesigner.ViewModels.Base;

namespace FunctionsDesigner.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		public MainWindowViewModel()
		{
			Functions = new ObservableCollection<FunctionVm>();

			IsFunctionSelected = false;

			var notSelectedFunction = new FunctionVm("Not selected");
			notSelectedFunction.Points.Add(new PointVm());
			Functions.Add(notSelectedFunction);

			var firstFunction = new FunctionVm("Function # 1");
			Functions.Add(firstFunction);

			var secondFunctions = new FunctionVm("Function # 2");
			Functions.Add(secondFunctions);

			SelectedFunction = Functions.First();

			SelectedFunction = null;
			NeedShowInverseFunction = false;

			WindowClosingRequestedCommand = new RelayCommand<CancelEventArgs>(ExecuteWindowClosingRequestedCommand);
			SaveProjectCommand = new RelayCommand(ExecuteSaveProjectCommand);
			AddPointCommand = new RelayCommand(ExecuteAddPointCommand);
		}

		public ICommand WindowClosingRequestedCommand { get; }
		public ICommand SaveProjectCommand { get; }
		public ICommand AddPointCommand { get; }

		public bool IsFunctionSelected
		{
			get { return NotifyPropertyGet(() => IsFunctionSelected); }
			set { NotifyPropertySet(() => IsFunctionSelected, value); }
		}

		public ObservableCollection<FunctionVm> Functions
		{
			get { return NotifyPropertyGet(() => Functions); }
			set { NotifyPropertySet(() => Functions, value); }
		}

		public FunctionVm SelectedFunction
		{
			get { return NotifyPropertyGet(() => SelectedFunction); }
			set { NotifyPropertySet(() => SelectedFunction, value); }
		}

		public bool NeedShowInverseFunction
		{
			get { return NotifyPropertyGet(() => NeedShowInverseFunction); }
			set { NotifyPropertySet(() => NeedShowInverseFunction, value); }
		}

		private void ExecuteAddPointCommand()
		{
			SelectedFunction.Points.Add(new PointVm());
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
