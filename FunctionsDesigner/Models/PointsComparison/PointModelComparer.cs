using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FunctionsDesigner.Models.Interfaces;

namespace FunctionsDesigner.Models.PointsComparison
{
	public class PointModelComparer : IComparer<IPoint>
	{
		private readonly IList<IPoint> _points;
		private readonly IList<PointInfo> _editedPoints;

		public PointModelComparer(IList<IPoint> points, IList<PointInfo> editedPoints)
		{
			_points = points;
			_editedPoints = editedPoints;
		}

		public int Compare(IPoint firstPoint, IPoint secondPoint)
		{
			if (firstPoint == null && secondPoint == null)
				return 0;
			if (firstPoint == null || secondPoint == null)
				return firstPoint != null ? -1 : 1;

			if (firstPoint.X == secondPoint.X && firstPoint.Y == secondPoint.Y)
				return 0;

			if (firstPoint.X != secondPoint.X)
				return firstPoint.X.CompareTo(secondPoint.X);


			var firstPointInfo = GetEditInfo(firstPoint);
			var secondPointInfo = GetEditInfo(secondPoint);

			var editStatusPair = GetEditStatusPair(firstPointInfo, secondPointInfo);

			switch (editStatusPair)
			{
				case PointArgumentsInfo.NoEdits:
				case PointArgumentsInfo.BothEdited:
					return KeepOrder(firstPoint, secondPoint);

				case PointArgumentsInfo.BothAdded:
					return CompareYValues(firstPoint, secondPoint);

				case PointArgumentsInfo.OneAddedOneEdited:
				case PointArgumentsInfo.OneAdded:
					return SetAddedRighter(firstPointInfo, secondPointInfo);

				case PointArgumentsInfo.OneEdited:
					return CompareOldValues(firstPoint, firstPointInfo, secondPoint, secondPointInfo);
			}

			return 0;
		}

		protected PointInfo GetEditInfo(IPoint point)
		{
			return _editedPoints.FirstOrDefault(e => e.Point == point);
		}

		private PointArgumentsInfo GetEditStatusPair(PointInfo firstPointInfo, PointInfo secondPointInfo)
		{
			if (firstPointInfo == null && secondPointInfo == null)
				return PointArgumentsInfo.NoEdits;

			switch (firstPointInfo?.IsAdded)
			{
				case true when secondPointInfo?.IsAdded == true:
					return PointArgumentsInfo.BothAdded;
				case false when secondPointInfo?.IsAdded == false:
					return PointArgumentsInfo.BothEdited;
			}

			if (firstPointInfo != null && secondPointInfo != null)
				return PointArgumentsInfo.OneAddedOneEdited;

			if (firstPointInfo?.IsAdded == true ^ secondPointInfo?.IsAdded == true)
				return PointArgumentsInfo.OneAdded;

			return PointArgumentsInfo.OneEdited;
		}

		private int KeepOrder(IPoint firstPoint, IPoint secondPoint)
		{
			return _points.IndexOf(firstPoint).CompareTo(_points.IndexOf(secondPoint));
		}

		private int CompareYValues(IPoint firstPoint, IPoint secondPoint)
		{
			return firstPoint.Y.CompareTo(secondPoint.Y);
		}

		private int SetAddedRighter(PointInfo firstPointInfo, PointInfo secondPointInfo)
		{
			return firstPointInfo?.IsAdded == true ? 1 : -1;
		}

		private int CompareOldValues(IPoint firstPoint, PointInfo firstPointInfo, IPoint secondPoint, PointInfo secondPointInfo)
		{
			var firstValue = firstPointInfo?.OldXValue.HasValue == true ? firstPointInfo.OldXValue.Value : firstPoint.X;
			var secondValue = secondPointInfo?.OldXValue.HasValue == true ? secondPointInfo.OldXValue.Value : secondPoint.X;

			var comparisonResult = firstValue.CompareTo(secondValue);

			return comparisonResult != 0 ? comparisonResult : KeepOrder(firstPoint, secondPoint);
		}
	}
}
