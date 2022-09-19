using FunctionsDesigner.Converters.JsonConverters;
using FunctionsDesigner.Models.Interfaces;
using Newtonsoft.Json;

namespace FunctionsDesigner.Models.PointsComparison
{
	public class PointInfo
	{
		public PointInfo(IPoint point, double? oldXValue = null)
		{
			Point = point;
			OldXValue = oldXValue;
		}

		[JsonProperty(TypeNameHandling = TypeNameHandling.Objects, ItemConverterType = typeof(PointConverter))]
		public IPoint Point { get; }

		public double? OldXValue { get; }

		public bool IsAdded => !OldXValue.HasValue;
	}
}
