using System;

namespace FunctionsDesigner.Extensions
{
	public static class ReflectionUtils
	{
		public static bool IsNullable(this Type type)
		{
			return type.IsGenericType && typeof(Nullable<>) == type.GetGenericTypeDefinition();
		}

		public static bool IsConcrete(this Type type)
		{
			return !type.IsInterface && !type.IsAbstract && !type.IsValueType;
		}

		public static void ThrowIfNull(this object @obj, string argumentName)
		{
			if (@obj == null)
				throw new ArgumentNullException(argumentName);
		}
	}
}
