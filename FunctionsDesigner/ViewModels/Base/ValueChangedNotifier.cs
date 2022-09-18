using System.Runtime.CompilerServices;
using FunctionsDesigner.Events.ValueChangedEvent;

namespace FunctionsDesigner.ViewModels.Base
{
	public class ValueChangedNotifier : BaseViewModel
	{
		public event PropertyValueChangedEventHandler PropertyValueChanged;

		protected void OnPropertyValueChanged(object oldValue, object newValue, [CallerMemberName] string propertyName = null)
		{
			var eventArgs = new PropertyValueChangedEventArgs(propertyName, oldValue, newValue);
			PropertyValueChanged?.Invoke(this, eventArgs);
		}
	}
}
