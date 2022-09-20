using System.Collections.Generic;
using System.Linq;
using FunctionsDesigner.Models.Interfaces;
using FunctionsDesigner.Models.PointsComparison;

namespace FunctionsDesigner.Services
{
	public class PointSortingService : IPointSortingService
	{
		public IEnumerable<IPoint> SortPoints(IEnumerable<IPoint> points, IComparer<IPoint> comparer = null)
		{
			var sortedPoints = comparer == null
				? points.OrderBy(p => p.X).Distinct(new PointModelEqualityComparer())
				: points.OrderBy(p => p, comparer).Distinct(new PointModelEqualityComparer());

			return sortedPoints;
		}
	}
}
