using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FunctionsDesigner.Events
{
	/// <summary>
	/// Defines an object that notifies when the value property changes.
	/// </summary>
	/// <seealso cref="INotifyPropertyValueChanged" />
	public class ObservableValue : INotifyPropertyChanged
	{
		private double? _value;

		/// <summary>
		/// Initializes a new instance of the <see cref="ObservableValue"/> class.
		/// </summary>
		public ObservableValue()
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="ObservableValue"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		public ObservableValue(double value)
		{
			_value = value;
		}

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		public double? Value { get => _value; set { _value = value; OnPropertyChanged(); } }

		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		/// <returns></returns>
		public event PropertyChangedEventHandler? PropertyChanged;

		/// <summary>
		/// Called when am property changed.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(propertyName, new PropertyChangedEventArgs(propertyName));
		}
	}
}
