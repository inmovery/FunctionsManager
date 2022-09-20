using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FunctionsDesigner.Converters.JsonConverters;
using FunctionsDesigner.Models.Interfaces;
using FunctionsDesigner.Models.PointsComparison;
using FunctionsDesigner.ViewModels.Base;
using Newtonsoft.Json;

namespace FunctionsDesigner.Models
{
	public class Function : BaseViewModel, IFunction
	{
		public Function()
		{
			Name = string.Empty;
			FunctionType = FunctionType.Used;
			Points = new ObservableCollection<IPoint>();
		}

		public Function(string functionName)
		{
			Name = functionName;
			FunctionType = FunctionType.Used;
			Points = new ObservableCollection<IPoint>();
		}

		public Function(string functionName, IEnumerable<IPoint> points)
		{
			Name = functionName;
			FunctionType = FunctionType.Used;
			Points = new ObservableCollection<IPoint>(points);

			SortPoints();
		}

		public Function(IEnumerable<IPoint> points)
		{
			Name = string.Empty;
			Points = new ObservableCollection<IPoint>(points);
			FunctionType = FunctionType.Used;
			SortPoints();
		}

		public int Count => Points.Count;

		public IPoint this[int index]
		{
			get => Points[index];
			set
			{
				// ToDo: решить эксепшн
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

		[JsonProperty(ItemConverterType = typeof(PointConverter))]
		public ObservableCollection<IPoint> Points
		{
			get { return NotifyPropertyGet(() => Points); }
			set { NotifyPropertySet(() => Points, value); }
		}

		public void MarkAsUnused() => FunctionType = FunctionType.Unused;

		public void Add(IPoint point) => Points.Add(point);

		public void Remove(IPoint point)
		{
			var isPointExists = Points.Contains(point);
			if (!isPointExists)
				return;

			Points.Remove(point);
		}

		private void SortPoints(IComparer<IPoint> comparer = null)
		{
			var pointList = Points.ToList();
			var sortedPoints = comparer == null
				? pointList.OrderBy(p => p.X).Distinct(new PointModelEqualityComparer())
				: pointList.OrderBy(p => p, comparer).Distinct(new PointModelEqualityComparer());

			Points = new ObservableCollection<IPoint>(sortedPoints);
		}
	}
}
