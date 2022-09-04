using System;
using System.Globalization;
using System.Windows.Data;

namespace FunctionsDesigner.Converters.ValueConverters
{
	public class NotNullToBoolConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value != null)
				return true;

			return false;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
