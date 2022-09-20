using System.Collections.Generic;
using FunctionsDesigner.Models.Interfaces;

namespace FunctionsDesigner.Services
{
	public interface IPointSortingService
	{
		IEnumerable<IPoint> SortPoints(IEnumerable<IPoint> points, IComparer<IPoint> comparer = null);
	}
}
