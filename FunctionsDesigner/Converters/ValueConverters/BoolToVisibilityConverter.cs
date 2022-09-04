using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FunctionsDesigner.Converters.ValueConverters
{
	public class BoolToVisibilityConverter : IValueConverter
	{
		public Visibility WhenTrue { get; set; } = Visibility.Visible;

		public Visibility WhenFalse { get; set; } = Visibility.Collapsed;

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool boolValue)
				return boolValue ? WhenTrue : WhenFalse;

			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}
	}
}
