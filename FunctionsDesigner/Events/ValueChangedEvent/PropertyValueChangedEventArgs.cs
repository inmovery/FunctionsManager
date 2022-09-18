using System.ComponentModel;

namespace FunctionsDesigner.Events.ValueChangedEvent
{
	public class PropertyValueChangedEventArgs : PropertyChangedEventArgs
	{
		public PropertyValueChangedEventArgs(string propertyName, object oldValue, object newValue)
			: base(propertyName)
		{
			OldValue = oldValue;
			NewValue = newValue;
		}

		public virtual object OldValue { get; }

		public virtual object NewValue { get; }
	}
}
