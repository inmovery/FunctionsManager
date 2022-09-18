using System.Collections.Generic;
using FunctionsDesigner.Models.Interfaces;

namespace FunctionsDesigner.Models.PointsComparison
{
	internal class PointModelEqualityComparer : IEqualityComparer<IPoint>
	{
		public bool Equals(IPoint firstPoint, IPoint secondPoint)
		{
			if (firstPoint == null && secondPoint == null)
				return true;
			if (firstPoint == null ^ secondPoint == null)
				return false;

			return firstPoint.X.Equals(secondPoint.X) && firstPoint.Y.Equals(secondPoint.Y);
		}

		public int GetHashCode(IPoint pointObject)
		{
			return (int)pointObject.X ^ (int)pointObject.Y;
		}
	}
}
