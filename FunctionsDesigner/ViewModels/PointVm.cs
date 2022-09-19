using FunctionsDesigner.Models.Interfaces;
using FunctionsDesigner.ViewModels.Base;

namespace FunctionsDesigner.Models
{
	public class PointVm : ValueChangedNotifier, IPoint
	{
		private double _temporaryX;
		private double _temporaryY;

		public PointVm()
		{
			X = 0.0d;
			Y = 0.0d;

			InitializeTemporaryParameters();
		}

		public PointVm(double x, double y)
		{
			X = x;
			Y = y;

			InitializeTemporaryParameters();
		}

		public double X
		{
			get { return NotifyPropertyGet(() => X); }
			set
			{
				var oldValue = _temporaryX;
				_temporaryX = value;
				NotifyPropertySet(() => X, value);
				OnPropertyValueChanged(oldValue, value);
			}
		}

		public double Y
		{
			get { return NotifyPropertyGet(() => Y); }
			set
			{
				var oldValue = _temporaryY;
				_temporaryY = value;
				NotifyPropertySet(() => Y, value);
				OnPropertyValueChanged(oldValue, value);
			}
		}

		private void InitializeTemporaryParameters()
		{
			_temporaryX = X;
			_temporaryY = Y;
		}
	}
}
