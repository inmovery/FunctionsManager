using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace FunctionsDesigner.Converters.ValueConverters
{
	public class EnumToDescriptionConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var fieldInfo = value?.GetType().GetField(value.ToString() ?? string.Empty);
			if (fieldInfo == null)
				return string.Empty;

			var customAttributes = fieldInfo.GetCustomAttributes(false);
			if (!customAttributes.Any())
				return value.ToString();

			var descriptionAttribute = customAttributes[0] as DescriptionAttribute;
			if (descriptionAttribute == null)
				return value.ToString();

			if (string.IsNullOrEmpty(descriptionAttribute.Description))
				return string.Empty;

			return descriptionAttribute.Description;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
