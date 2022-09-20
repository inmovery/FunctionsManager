using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Serialization;

namespace FunctionsDesigner.Converters.JsonConverters
{
	public class JsonObservableCollectionConverter : DefaultContractResolver
	{
		public override JsonContract ResolveContract(Type type)
		{
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ICollection<>))
				return ResolveContract(typeof(ObservableCollection<>).MakeGenericType(type.GetGenericArguments()));

			return base.ResolveContract(type);
		}
	}
}
