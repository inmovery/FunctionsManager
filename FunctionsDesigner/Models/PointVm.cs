using System;
using FunctionsDesigner.ViewModels.Base;

namespace FunctionsDesigner.Models
{
	public class PointVm : ValueChangedNotifier
	{
		public PointVm()
		{
			X = 0;
			Y = 0;
		}

		public PointVm(double x, double y)
		{
			X = x;
			Y = y;
		}

		public double X
		{
			get { return NotifyPropertyGet(() => X); }
			set
			{
				//var oldValue = string.Empty;
				NotifyPropertySet(() => X, value);
				//OnPropertyValueChanged(oldValue, value);
			}
		}

		public double Y
		{
			get { return NotifyPropertyGet(() => Y); }
			set
			{
				//var oldValue = string.Empty;
				NotifyPropertySet(() => Y, value);
				//OnPropertyValueChanged(oldValue, value);
			}
		}
	}
}
