using System;
using FunctionsDesigner.Models;
using FunctionsDesigner.Models.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FunctionsDesigner.Converters.JsonConverters
{
	public class PointConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(IPoint);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return serializer.Deserialize<PointVm>(reader);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			JToken t = JToken.FromObject(value, serializer);
			if (t.Type == JTokenType.Object)
			{
				JObject o = (JObject)t;
				o.AddFirst(new JProperty("Type", value.GetType().Name));
				o.WriteTo(writer);
			}
			else
			{
				t.WriteTo(writer);
			}
		}
	}
}
