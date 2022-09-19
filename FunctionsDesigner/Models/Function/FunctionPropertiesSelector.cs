using FunctionsDesigner.Models.Interfaces;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Themes;
using SkiaSharp;

namespace FunctionsDesigner.Models
{
	public class FunctionPropertiesSelector
	{
		private readonly LvcColor[] _colors = ColorPalletes.FluentDesign;
		private int _currentColorIndex;

		public FunctionPropertiesSelector()
		{
			Fill = null;
			LineSmoothness = 0.0d;
			Stroke = new SolidColorPaint(SKColors.Black, 3);
			GeometryStroke = null;
			GeometryFill = new SolidColorPaint(SKColors.Black);
			GeometrySize = 15;
		}

		public SolidColorPaint Stroke { get; private set; }

		public SolidColorPaint Fill { get; private set; }

		public SolidColorPaint GeometryFill { get; private set; }

		public SolidColorPaint GeometryStroke { get; private set; }

		public double LineSmoothness { get; }

		public double GeometrySize { get; }

		public void GenerateStroke()
		{
			var nextColorIndex = _currentColorIndex++ % _colors.Length;
			var color = _colors[nextColorIndex];

			Stroke = new SolidColorPaint(new SKColor(color.R, color.G, color.B)) { StrokeThickness = 3 };
		}

		public void GenerateFill(LineSeries<IPoint> series)
		{
			var nextColorIndex = _currentColorIndex++ % _colors.Length;
			var color = _colors[nextColorIndex];

			Fill = new SolidColorPaint(new SKColor(color.R, color.G, color.B, 90));
		}

		public void GenerateGeometryFill(LineSeries<IPoint> series)
		{
			var nextColorIndex = _currentColorIndex++ % _colors.Length;
			var color = _colors[nextColorIndex];

			GeometryFill = new SolidColorPaint(new SKColor(color.R, color.G, color.B));
		}

		public void GenerateGeometryStroke(LineSeries<IPoint> series)
		{
			var nextColorIndex = _currentColorIndex++ % _colors.Length;
			var color = _colors[nextColorIndex];

			GeometryStroke = new SolidColorPaint(new SKColor(color.R, color.G, color.B)) { StrokeThickness = 3 };
		}
	}
}
