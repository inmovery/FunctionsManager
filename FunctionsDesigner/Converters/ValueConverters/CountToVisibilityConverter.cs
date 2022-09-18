using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FunctionsDesigner.Converters.ValueConverters
{
	public class CountToVisibilityConverter : IValueConverter
	{
		public CompareOperandType Type { get; set; }

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is not int source || parameter == null)
				return Visibility.Collapsed;

			try
			{
				var count = int.Parse(parameter.ToString() ?? string.Empty);

				return Type switch
				{
					CompareOperandType.MoreThan => source > count ? Visibility.Visible : Visibility.Collapsed,
					CompareOperandType.LessThan => source < count ? Visibility.Visible : Visibility.Collapsed,
					_ => source == count ? Visibility.Visible : Visibility.Collapsed
				};
			}
			catch (Exception)
			{
				return Visibility.Collapsed;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
