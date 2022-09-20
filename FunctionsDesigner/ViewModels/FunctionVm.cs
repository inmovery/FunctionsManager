using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using FunctionsDesigner.Commands;
using FunctionsDesigner.Converters.JsonConverters;
using FunctionsDesigner.Events.ValueChangedEvent;
using FunctionsDesigner.Models;
using FunctionsDesigner.Models.Interfaces;
using FunctionsDesigner.Models.PointsComparison;
using FunctionsDesigner.Services;
using FunctionsDesigner.ViewModels.Base;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Newtonsoft.Json;
using SkiaSharp;

namespace FunctionsDesigner.ViewModels
{
	public class FunctionVm : BaseViewModel, IDisposable
	{
		private readonly IPointSortingService _pointSortingService;

		public FunctionVm()
		{
			_pointSortingService = new PointSortingService();

			Function = new Function();
			Properties = new FunctionPropertiesSelector();

			Series = BuildLineSeries();
			Series.Values = Function.Points;
			NewXParameter = 0.0d;
			NewYParameter = 0.0d;

			Function.Points.CollectionChanged += OnPointsCollectionChanged;
		}

		public FunctionVm(IFunction function, FunctionPropertiesSelector functionPropertiesSelector)
		{
			_pointSortingService = new PointSortingService();

			Function = (Function)function;
			Properties = functionPropertiesSelector;

			Function.Points.CollectionChanged += OnPointsCollectionChanged;

			Series = BuildLineSeries();
			Series.Values = Function.Points;

			AddPointCommand = new RelayCommand(ExecuteAddPointCommand);
			RemovePointCommand = new RelayCommand<PointVm>(ExecuteRemovePointCommand);
		}

		public ICommand AddPointCommand { get; }
		public ICommand RemovePointCommand { get; }

		public event EventHandler FunctionChanged;

		public Function Function
		{
			get { return NotifyPropertyGet(() => Function); }
			set { NotifyPropertySet(() => Function, value); }
		}

		[JsonProperty(ItemConverterType = typeof(PointConverter))]
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

		public void Dispose()
		{
			FunctionChanged = null;
			Function.Points.CollectionChanged -= OnPointsCollectionChanged;
			foreach (var point in Function.Points)
				((PointVm)point).PropertyValueChanged -= OnPointPropertyValueChanged;
		}

		private void OnPointPropertyValueChanged(object sender, PropertyValueChangedEventArgs eventArgs)
		{
			if (sender is not IPoint point)
				throw new ArgumentException(nameof(sender));

			var isXProperty = eventArgs.PropertyName.Equals(nameof(IPoint.X));
			if (!isXProperty)
			{
				SortPoints();
				return;
			}

			var oldValue = (double?)eventArgs.OldValue;

			var pointInfo = new PointInfo(point, oldValue);
			var comparer = new PointModelComparer(Function.Points, new[] { pointInfo });

			SortPoints(comparer);
			UpdateAndNotifyChanges();
		}

		private void OnPointsCollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
		{
			IComparer<IPoint> comparer;
			List<PointInfo> pointsInfo;

			switch (eventArgs.Action)
			{
				case NotifyCollectionChangedAction.Add:
					pointsInfo = eventArgs.NewItems.Cast<IPoint>().Select(point => new PointInfo(point)).ToList();
					comparer = new PointModelComparer(Function.Points, pointsInfo);
					break;

				case NotifyCollectionChangedAction.Replace:
					var oldPoints = eventArgs.OldItems.Cast<IPoint>().ToArray();
					var newPoints = eventArgs.NewItems.Cast<IPoint>().ToArray();

					pointsInfo = oldPoints.Select(point => new PointInfo(newPoints[0], oldPoints[0].X)).ToList();
					comparer = new PointModelComparer(Function.Points, pointsInfo);
					break;

				default:
					return;
			}

			SortPoints(comparer);
			UpdateAndNotifyChanges();
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

		private void UpdateAndNotifyChanges()
		{
			Series.Values = Function.Points;
			FunctionChanged?.Invoke(this, EventArgs.Empty);
		}

		private void ExecuteAddPointCommand()
		{
			var point = new PointVm(NewXParameter, NewYParameter);
			point.PropertyValueChanged += OnPointPropertyValueChanged;

			Function.Add(point);
		}

		private void ExecuteRemovePointCommand(PointVm point)
		{
			Function.Remove(point);
		}

		private void SortPoints(IComparer<IPoint> comparer = null)
		{
			var sortedPoints = _pointSortingService.SortPoints(Function.Points, comparer);

			Function.Points.CollectionChanged -= OnPointsCollectionChanged;
			Function.Points = new ObservableCollection<IPoint>(sortedPoints);
			Function.Points.CollectionChanged += OnPointsCollectionChanged;
		}
	}
}
