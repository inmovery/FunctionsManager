using System.Windows.Input;
using FunctionsDesigner.Commands;
using FunctionsDesigner.ViewModels.Base;

namespace FunctionsDesigner.ExtendedControls.PointEditor
{
	public class PointEditorVm : BaseViewModel
	{
		public PointEditorVm()
		{
		}

		public ICommand RemovePointCommand { get; }
	}
}
