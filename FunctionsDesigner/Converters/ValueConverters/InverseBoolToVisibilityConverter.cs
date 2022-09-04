using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FunctionsDesigner.Converters.ValueConverters
{
	public class InverseBoolToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
				return DependencyProperty.UnsetValue;

			var sourceValue = (bool)value;
			return sourceValue ? Visibility.Collapsed : Visibility.Visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return DependencyProperty.UnsetValue;
		}
	}
}
