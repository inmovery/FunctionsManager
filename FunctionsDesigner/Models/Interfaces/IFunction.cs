using System.Collections.ObjectModel;

namespace FunctionsDesigner.Models.Interfaces
{
	public interface IFunction
	{
		int Count { get; }
		IPoint this[int index] { get; set; }
		string Name { get; }
		ObservableCollection<IPoint> Points { get; }
		void Add(double x, double y);
		void Remove(IPoint point);
		void MarkAsUnused();
	}
}
