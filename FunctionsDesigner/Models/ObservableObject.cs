using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FunctionsDesigner.Models
{
	/// <summary>
	/// Defines a point for the Cartesian coordinate system that implements <see cref="INotifyPropertyChanged"/>.
	/// </summary>
	/// <seealso cref="INotifyPropertyChanged" />
	public class ObservableObject : INotifyPropertyChanged
	{
		private double? _x;
		private double? _y;

		/// <summary>
		/// Initializes a new instance of the <see cref="ObservableObject"/> class.
		/// </summary>
		public ObservableObject()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ObservableObject"/> class.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public ObservableObject(double? x, double? y)
		{
			_x = x;
			_y = y;
		}

		/// <summary>
		/// Gets or sets the x coordinate.
		/// </summary>
		/// <value>
		/// The x.
		/// </value>
		public double? X
		{
			get => _x;
			set
			{
				_x = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the y coordinate.
		/// </summary>
		/// <value>
		/// The y.
		/// </value>
		public double? Y
		{
			get => _y;
			set
			{
				_y = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		/// <returns></returns>
		public event PropertyChangedEventHandler? PropertyChanged;

		/// <summary>
		/// Called when a property changed.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(propertyName, new PropertyChangedEventArgs(propertyName));
		}
	}
}