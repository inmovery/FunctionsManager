using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using FunctionsDesigner.ViewModels.Base;

namespace FunctionsDesigner.Models
{
	public class FunctionVm : BaseViewModel
	{
		public FunctionVm()
		{
			Name = string.Empty;
			Points = new ObservableCollection<PointVm>();
		}

		public FunctionVm(string functionName)
		{
			Name = functionName;
			Points = new ObservableCollection<PointVm>();
		}

		public FunctionVm(string functionName, IEnumerable<PointVm> points)
		{
			Name = functionName;
			Points = new ObservableCollection<PointVm>(points);
		}

		public FunctionVm(IEnumerable<PointVm> points)
		{
			Name = GenerateFunctionName();
			Points = new ObservableCollection<PointVm>(points);
		}

		//public event NotifyCollectionChangedEventHandler CollectionChanged;
		public event EventHandler FunctionChanged;

		public virtual int Count => Points.Count;

		public string Name
		{
			get { return NotifyPropertyGet(() => Name); }
			set { NotifyPropertySet(() => Name, value); }
		}

		public ObservableCollection<PointVm> Points
		{
			get { return NotifyPropertyGet(() => Points); }
			set { NotifyPropertySet(() => Points, value); }
		}

		public void Add(double x, double y)
		{
			var point = new PointVm(x, y);
		}

		private string GenerateFunctionName()
		{
			return string.Empty;
		}
	}
}
