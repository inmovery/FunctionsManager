using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace FunctionsDesigner.ViewModels.Base
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		private readonly Dictionary<string, object> _notifyPropertyValues;

		public BaseViewModel()
		{
			_notifyPropertyValues = new Dictionary<string, object>();
		}

		public event PropertyChangedEventHandler? PropertyChanged = (_, _) => { };

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected virtual void RaisePropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}

		protected void NotifyPropertySet<T>(Expression<Func<T>> notifyProperty, T newValue)
		{
			if (newValue == null)
				return;

			var propertyName = GetPropertyName(notifyProperty);

			if (!_notifyPropertyValues.ContainsKey(propertyName))
				_notifyPropertyValues.Add(propertyName, default(T)!);

			var lastValue = (T)_notifyPropertyValues[propertyName];
			if (IsEquals(lastValue, newValue))
				return;

			_notifyPropertyValues.Remove(propertyName);
			_notifyPropertyValues.Add(propertyName, newValue);
			OnPropertyChanged(propertyName);
		}

		protected T NotifyPropertyGet<T>(Expression<Func<T>> notifyProperty)
		{
			var propertyName = GetPropertyName(notifyProperty);

			if (!_notifyPropertyValues.ContainsKey(propertyName))
				_notifyPropertyValues.Add(propertyName, default(T)!);

			return (T)_notifyPropertyValues[propertyName];
		}

		public string GetPropertyName<T>(Expression<Func<T>> expression)
		{
			var member = (MemberExpression)expression.Body;
			return member.Member.Name;
		}

		private static bool IsEquals<T>(T obj1, T obj2)
		{
			if (obj1 == null)
				return obj2 == null;

			return obj1.Equals(obj2);
		}
	}
}
