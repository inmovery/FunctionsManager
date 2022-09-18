using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using FunctionsDesigner.Events.ValueChangedEvent;
using FunctionsDesigner.Models.Interfaces;
using FunctionsDesigner.Models.PointsComparison;
using FunctionsDesigner.ViewModels.Base;

namespace FunctionsDesigner.Models
{
	public class Function : BaseViewModel, IFunction
	{
		public Function()
		{
			Name = string.Empty;
			Points = new ObservableCollection<IPoint>();
			FunctionType = FunctionType.Used;
		}

		public Function(string functionName)
		{
			Name = functionName;
			Points = new ObservableCollection<IPoint>();
			FunctionType = FunctionType.Used;
		}

		public Function(string functionName, IEnumerable<IPoint> points)
		{
			Name = functionName;
			Points = new ObservableCollection<IPoint>(points);
			FunctionType = FunctionType.Used;

			// ToDo: подумать над сортировкой
			SortPoints();
		}

		public Function(IEnumerable<IPoint> points)
		{
			Name = string.Empty;
			Points = new ObservableCollection<IPoint>(points);
			FunctionType = FunctionType.Used;

			// ToDo: подумать над сортировкой
			SortPoints();
		}

		public event NotifyCollectionChangedEventHandler CollectionChanged;
		public event EventHandler FunctionChanged;

		public int Count => Points.Count;

		public IPoint this[int index]
		{
			get => Points[index];
			set
			{
				// ToDo: починить возможный экспешн
				if (Points[index] == value)
					return;

				Points[index] = value;
			}
		}

		public FunctionType FunctionType
		{
			get { return NotifyPropertyGet(() => FunctionType); }
			set { NotifyPropertySet(() => FunctionType, value); }
		}

		public string Name
		{
			get { return NotifyPropertyGet(() => Name); }
			set { NotifyPropertySet(() => Name, value); }
		}

		public ObservableCollection<IPoint> Points
		{
			// ToDO: переделать на IEnumerable с приватным свойством и наследованием от INotifyPropertyChanged
			get { return NotifyPropertyGet(() => Points); }
			set
			{
				if (Points != null)
					Points.CollectionChanged -= OnPointsCollectionChanged;

				NotifyPropertySet(() => Points, value);
				if (Points == null)
					return;

				Points.CollectionChanged += OnPointsCollectionChanged;
			}
		}

		public void MarkAsUnused()
		{
			FunctionType = FunctionType.Unused;
		}

		public void Add(double x, double y)
		{
			var point = new PointVm(x, y);
			point.PropertyValueChanged += OnPointPropertyValueChanged;

			Points.Add(point);
		}

		public void Remove(IPoint point)
		{
			if (!Points.Contains(point))
				return;

			((PointVm)point).PropertyValueChanged -= OnPointPropertyValueChanged;
			Points.Remove(point);
		}

		private void OnPointPropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
		{
			if (sender is not IPoint point)
				throw new ArgumentException(nameof(sender));

			FunctionChanged?.Invoke(this, EventArgs.Empty);

			//ToDo: подумать над сортировкой
			if (!IsXPropertyName(e.PropertyName))
			{
				SortPoints();
				return;
			}

			var oldValue = (double?)e.OldValue;

			var pointInfo = new PointInfo(point, oldValue);
			SortPoints(new PointModelComparer(Points, new[] { pointInfo }));
		}

		private void OnPointsCollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
		{
			CollectionChanged?.Invoke(sender, eventArgs);
			FunctionChanged?.Invoke(this, EventArgs.Empty);

			//ToDo: подумать над сортировкой
			IComparer<IPoint> comparer;
			List<PointInfo> pointsInfo;

			switch (eventArgs.Action)
			{
				case NotifyCollectionChangedAction.Add:
					pointsInfo = eventArgs.NewItems.Cast<IPoint>().Select(p => new PointInfo(p)).ToList();
					comparer = new PointModelComparer(Points, pointsInfo);
					break;

				case NotifyCollectionChangedAction.Replace:
					var oldPoints = eventArgs.OldItems.Cast<IPoint>().ToArray();
					var newPoints = eventArgs.NewItems.Cast<IPoint>().ToArray();

					pointsInfo = oldPoints.Select(point => new PointInfo(newPoints[0], oldPoints[0].X)).ToList();
					comparer = new PointModelComparer(Points, pointsInfo);
					break;

				default:
					return;
			}

			SortPoints(comparer);
		}

		private bool IsXPropertyName(string propertyName)
		{
			//return propertyName.Equals(nameof(IPoint.X));
			IPoint point = null;
			return propertyName.Equals(nameof(point.X));
		}

		private void SortPoints(IComparer<IPoint> comparer = null)
		{
			var points = Points.ToList();
			var sortedPoints = comparer == null
				? points.OrderBy(p => p.X).Distinct(new PointModelEqualityComparer())
				: points.OrderBy(p => p, comparer).Distinct(new PointModelEqualityComparer());

			Points = new ObservableCollection<IPoint>(sortedPoints);
		}
	}
}
