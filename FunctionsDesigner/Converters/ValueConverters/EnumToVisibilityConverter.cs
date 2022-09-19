using System;
using System.Windows;
using System.Windows.Data;

namespace FunctionsDesigner.Converters.ValueConverters
{
	public class EnumToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return value?.Equals(parameter) == true ? Visibility.Visible : Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return value?.Equals(Visibility.Visible) == true ? parameter : Binding.DoNothing;
		}
    }
}
