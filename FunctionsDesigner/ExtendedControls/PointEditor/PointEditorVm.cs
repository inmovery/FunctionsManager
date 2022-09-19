using System.Windows.Input;
using FunctionsDesigner.Commands;
using FunctionsDesigner.ViewModels.Base;

namespace FunctionsDesigner.ExtendedControls.PointEditor
{
	public class PointEditorVm : BaseViewModel
	{
		public PointEditorVm()
		{
			ApplyChangesCommand = new RelayCommand(ExecuteApplyChangesCommand);
		}

		public ICommand ApplyChangesCommand { get; }

		private void ExecuteApplyChangesCommand()
		{

		}
	}
}
