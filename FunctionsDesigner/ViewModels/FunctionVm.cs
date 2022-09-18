using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using FunctionsDesigner.Commands;
using FunctionsDesigner.Models;
using FunctionsDesigner.Models.Interfaces;
using FunctionsDesigner.ViewModels.Base;
using LiveChartsCore.SkiaSharpView;

namespace FunctionsDesigner.ViewModels
{
	public class FunctionVm : BaseViewModel
	{
		public FunctionVm()
		{
			Function = new Function();
			Series = BuildSeries();
			Series.Values = Function.Points;
			NewXParameter = 0.0d;
			NewYParameter = 0.0d;
		}

		public FunctionVm(IFunction function)
		{
			Function = (Function)function;
			Series = BuildSeries();
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

		public LineSeries<IPoint> Series
		{
			get { return NotifyPropertyGet(() => Series); }
			set { NotifyPropertySet(() => Series, value); }
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

		private LineSeries<IPoint> BuildSeries()
		{
			var series = new LineSeries<IPoint>()
			{
				LineSmoothness = 0,
			};

			return series;
		}

		private void ExecuteAddPointCommand()
		{
			Function.Add(NewXParameter, NewYParameter);
			// Series Add(new PointVm(NewXParameter, NewYParameter));
		}

		private void ExecuteRemovePointCommand(PointVm point)
		{
			Function.Points.Remove(point);
		}
	}
}
