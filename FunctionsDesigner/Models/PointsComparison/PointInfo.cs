using FunctionsDesigner.Models.Interfaces;

namespace FunctionsDesigner.Models.PointsComparison
{
	public class PointInfo
	{
		public PointInfo(IPoint point, double? oldXValue = null)
		{
			Point = point;
			OldXValue = oldXValue;
		}

		public IPoint Point { get; }

		public double? OldXValue { get; }

		public bool IsAdded => !OldXValue.HasValue;
	}
}
