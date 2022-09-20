using System.Collections.ObjectModel;
using System.Linq;
using FunctionsDesigner.Models.Interfaces;
using FunctionsDesigner.ViewModels.Base;
using LiveChartsCore;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace FunctionsDesigner.Models
{
	public class ChartVm : BaseViewModel
	{
		public ChartVm()
		{
			Series = new ObservableCollection<ISeries>();
		}

		public ObservableCollection<ISeries> Series
		{
			get { return NotifyPropertyGet(() => Series); }
			set { NotifyPropertySet(() => Series, value); }
		}

		public ICartesianAxis[] XAxes { get; set; } =
		{
			new Axis()
			{
				Name = "X"
			}
		};

		public ICartesianAxis[] YAxes { get; set; } =
		{
			new Axis()
			{
				Name = "Y"
			}
		};

		public void Reinitialize()
		{
			var series = Series.ToList();
			Series.Clear();
			series.ForEach(seriesModel =>
			{
				((LineSeries<IPoint>)seriesModel).GeometryFill = new SolidColorPaint(SKColors.Black);
				Series.Add(seriesModel);
			});
		}
	}
}
