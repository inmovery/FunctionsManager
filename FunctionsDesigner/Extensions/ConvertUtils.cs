using System;
using System.ComponentModel;
using System.Globalization;

namespace FunctionsDesigner.Extensions
{
	public static class ConvertUtils
	{
		public static bool CanConvertType(this Type initialType, Type targetType)
		{
			if (targetType.IsNullable())
				targetType = Nullable.GetUnderlyingType(targetType);

			if (targetType == initialType)
				return true;

			var isAssignableFrom = typeof(IConvertible).IsAssignableFrom(initialType) && typeof(IConvertible).IsAssignableFrom(targetType);
			if (isAssignableFrom)
				return true;

			var isDateTimeType = initialType == typeof(DateTime) && targetType == typeof(DateTimeOffset);
			if (isDateTimeType)
				return true;

			var isGuidType = initialType == typeof(Guid) && (targetType == typeof(Guid) || targetType == typeof(string));
			if (isGuidType)
				return true;

			var isGeneralType = initialType == typeof(Type) && targetType == typeof(string);
			if (isGeneralType)
				return true;

			var typeConverter = GetConverter(initialType);
			var canConvertType = targetType != null && typeConverter != null && !IsComponentConverter(typeConverter) && typeConverter.CanConvertTo(targetType);
			var isNotConvertSelf = typeConverter?.GetType() != typeof(TypeConverter);
			if (canConvertType && isNotConvertSelf)
				return true;

			var fromConverter = GetConverter(targetType);
			if (fromConverter != null && !IsComponentConverter(fromConverter) && fromConverter.CanConvertFrom(initialType))
				return true;

			return initialType == typeof(DBNull) && targetType.IsNullable();
		}

		public static T Convert<T>(this object initialValue)
		{
			return initialValue.Convert<T>(CultureInfo.CurrentCulture);
		}

		public static T Convert<T>(this object initialValue, CultureInfo culture)
		{
			return (T)initialValue.Convert(culture, typeof(T));
		}

		public static object Convert(this object initialValue, CultureInfo culture, Type targetType)
		{
			initialValue.ThrowIfNull("initialValue");
			if (targetType.IsNullable())
				targetType = Nullable.GetUnderlyingType(targetType);

			var initialType = initialValue.GetType();

			if (targetType == initialType)
				return initialValue;

			if (initialValue is string targetValue && typeof(Type).IsAssignableFrom(targetType))
				return Type.GetType(targetValue, true);

			if (!targetType.IsConcrete())
				throw new ArgumentException(
					string.Format(culture, "Target type {0} is not a value type or a non-abstract class.", targetType), "TargetType");

			if (initialValue is IConvertible && typeof(IConvertible).IsAssignableFrom(targetType))
			{
				if (!targetType.IsEnum)
					return System.Convert.ChangeType(initialValue, targetType, culture);

				if (initialValue is string)
					return Enum.Parse(targetType, initialValue.ToString() ?? string.Empty, true);

				if (initialValue.IsInteger())
					return Enum.ToObject(targetType, initialValue);

				return System.Convert.ChangeType(initialValue, targetType, culture);
			}

			if (initialValue is DateTime dateTime && targetType == typeof(DateTimeOffset))
				return new DateTimeOffset(dateTime);

			if (initialValue is not string value)
				return null;

			if (targetType == typeof(Guid))
				return new Guid(value);

			if (targetType == typeof(Uri))
				return new Uri(value);

			if (targetType == typeof(TimeSpan))
				return TimeSpan.Parse(value, CultureInfo.InvariantCulture);

			return value;

		}

		internal static TypeConverter GetConverter(Type t)
		{
			return TypeDescriptor.GetConverter(t);
		}

		private static bool IsComponentConverter(TypeConverter converter)
		{
			return (converter is ComponentConverter);
		}

		public static bool IsInteger(this object value)
		{
			switch (System.Convert.GetTypeCode(value))
			{
				case TypeCode.Byte:
				case TypeCode.SByte:
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.UInt16:
				case TypeCode.Int64:
				case TypeCode.UInt32:
				case TypeCode.UInt64:
					return true;

				default:
					return false;
			}
		}
	}
}
