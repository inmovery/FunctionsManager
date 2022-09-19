using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using FunctionsDesigner.Commands;
using FunctionsDesigner.Converters.JsonConverters;
using FunctionsDesigner.Models;
using FunctionsDesigner.Models.Interfaces;
using FunctionsDesigner.ViewModels.Base;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Newtonsoft.Json;
using SkiaSharp;

namespace FunctionsDesigner.ViewModels
{
	public class FunctionVm : BaseViewModel
	{
		public FunctionVm()
		{
			Function = new Function();
			Properties = new FunctionPropertiesSelector();

			Series = BuildLineSeries();
			Series.Values = Function.Points;
			NewXParameter = 0.0d;
			NewYParameter = 0.0d;
		}

		public FunctionVm(IFunction function, FunctionPropertiesSelector functionPropertiesSelector)
		{
			Function = (Function)function;
			Properties = functionPropertiesSelector;

			Series = BuildLineSeries();
			Series.Values = Function.Points;

			Function.PropertyChanged += OnFunctionPropertyChanged;
			Function.CollectionChanged += OnPointsCollectionChanged;

			AddPointCommand = new RelayCommand(ExecuteAddPointCommand);
			RemovePointCommand = new RelayCommand<PointVm>(ExecuteRemovePointCommand);
		}

		public ICommand AddPointCommand { get; }
		public ICommand RemovePointCommand { get; }

		public Function Function
		{
			get { return NotifyPropertyGet(() => Function); }
			set { NotifyPropertySet(() => Function, value); }
		}

		[JsonProperty(TypeNameHandling = TypeNameHandling.Objects, ItemConverterType = typeof(PointConverter))]
		public LineSeries<IPoint> Series
		{
			get { return NotifyPropertyGet(() => Series); }
			set { NotifyPropertySet(() => Series, value); }
		}

		public FunctionPropertiesSelector Properties
		{
			get { return NotifyPropertyGet(() => Properties); }
			set { NotifyPropertySet(() => Properties, value); }
		}

		public double NewXParameter
		{
			get { return NotifyPropertyGet(() => NewXParameter); }
			set { NotifyPropertySet(() => NewXParameter, value); }
		}

		public double NewYParameter
		{
			get { return NotifyPropertyGet(() => NewYParameter); }
			set { NotifyPropertySet(() => NewYParameter, value); }
		}

		private void OnFunctionPropertyChanged(object sender, PropertyChangedEventArgs eventArgs)
		{
			Series.Values = Function.Points;
		}

		private void OnPointsCollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
		{
		}

		private LineSeries<IPoint, CustomChartGeometry> BuildCustomLineSeries()
		{
			var series = new LineSeries<IPoint, CustomChartGeometry>()
			{
				LineSmoothness = 0,
				Stroke = new SolidColorPaint(SKColors.Black, 3),
				Fill = null,
				GeometryStroke = null,
				GeometryFill = new SolidColorPaint(SKColors.Black),
				GeometrySize = 15
			};

			return series;
		}

		private LineSeries<IPoint> BuildLineSeries()
		{
			var series = new LineSeries<IPoint>()
			{
				LineSmoothness = Properties.LineSmoothness,
				Stroke = Properties.Stroke,
				Fill = Properties.Fill,
				GeometryStroke = Properties.GeometryStroke,
				GeometryFill = Properties.GeometryFill,
				GeometrySize = Properties.GeometrySize
			};

			return series;
		}

		private void ExecuteAddPointCommand()
		{
			Function.Add(NewXParameter, NewYParameter);
		}

		private void ExecuteRemovePointCommand(PointVm point)
		{
			Function.Points.Remove(point);
		}
	}
}
