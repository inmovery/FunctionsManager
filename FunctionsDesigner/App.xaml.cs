using System.Windows;
using FunctionsDesigner.Models.Interfaces;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace FunctionsDesigner
{
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			DoLiveChartMappings();
		}

		private void DoLiveChartMappings()
		{
			LiveCharts.Configure(config =>
			{
				_ = config.AddSkiaSharp()
					.AddDefaultMappers()
					.AddLightTheme()
					.HasMap<IPoint>((pointModel, chartPoint) =>
					{
						chartPoint.PrimaryValue = pointModel.Y;
						chartPoint.SecondaryValue = pointModel.X;
					});
			});
		}
	}


}
