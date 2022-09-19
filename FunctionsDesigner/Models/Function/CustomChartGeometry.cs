using LiveChartsCore.SkiaSharpView.Drawing;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using SkiaSharp;

namespace FunctionsDesigner.Models
{
	public class CustomChartGeometry : SVGPathGeometry
	{
		public static SKPath SvgPath = SKPath.ParseSvgPathData("M 256 48 C 141.1 48 48 141.1 48 256 48 370.9 141.1 464 256 464 370.9 464 464 370.9 464 256 464 141.1 370.9 48 256 48 Z m 0 398.7 C 150.9 446.7 65.3 361.2 65.3 256 65.3 150.9 150.8 65.3 256 65.3 c 105.1 0 190.7 85.5 190.7 190.7 0 105.1 -85.6 190.7 -190.7 190.7 z");

		public CustomChartGeometry()
			: base(SvgPath)
		{
			//var test = IconResourceUtils.GetPathTemplateByName("CircleGeometry").Figures.ToString();
		}

		public override void OnDraw(SkiaSharpDrawingContext context, SKPaint paint)
		{
			using (var backgroundPaint = new SKPaint())
			{
				backgroundPaint.Style = SKPaintStyle.Fill;
				backgroundPaint.Color = SKColors.White;

				var radius = Width / 2;
				context.Canvas.DrawCircle(X + radius, Y + radius, radius, backgroundPaint);
			}

			base.OnDraw(context, paint);
		}
    }
}
