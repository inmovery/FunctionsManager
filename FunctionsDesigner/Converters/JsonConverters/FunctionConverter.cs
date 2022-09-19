using System;
using FunctionsDesigner.Models;
using FunctionsDesigner.Models.Interfaces;
using Newtonsoft.Json;

namespace FunctionsDesigner.Converters.JsonConverters
{
	public class FunctionConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(IFunction);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return serializer.Deserialize<Function>(reader);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
