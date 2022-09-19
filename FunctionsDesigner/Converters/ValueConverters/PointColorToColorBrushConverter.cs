using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using LiveChartsCore.SkiaSharpView.Painting;

namespace FunctionsDesigner.Converters.ValueConverters
{
	public class PointColorToColorBrushConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var solidColorPaint = (SolidColorPaint)value;
			if (solidColorPaint == null)
				return Brushes.Transparent;

			var color = Color.FromArgb(solidColorPaint.Color.Alpha, solidColorPaint.Color.Red,
				solidColorPaint.Color.Green, solidColorPaint.Color.Blue);

			return new SolidColorBrush(color);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
