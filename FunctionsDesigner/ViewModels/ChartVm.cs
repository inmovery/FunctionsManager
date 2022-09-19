using System.Collections.ObjectModel;
using FunctionsDesigner.ViewModels.Base;
using LiveChartsCore;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;

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
    }
}
