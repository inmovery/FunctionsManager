using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;
using System.Windows.Media;

namespace FunctionsDesigner.Extensions
{
	public static class AncestorUtils
	{
		public static T FindAncestor<T>(this DependencyObject current)
			where T : DependencyObject
		{
			if (current == null)
				return null;
			do
			{
				if (current is T ancestor)
					return ancestor;

				current = VisualTreeHelper.GetParent(current);
			}
			while (current != null);

			return null;
		}

		public static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
		{
			var parentObject = VisualTreeHelper.GetParent(child);
			if (parentObject == null)
				return null;

			if (parentObject is T parent)
				return parent;

			return FindVisualParent<T>(parentObject);
		}

		public static T GetChildOfType<T>(this DependencyObject dObj) where T : DependencyObject
		{
			if (dObj == null)
				return null;

			for (var i = 0; i < VisualTreeHelper.GetChildrenCount(dObj); i++)
			{
				var child = VisualTreeHelper.GetChild(dObj, i);

				var result = (child as T) ?? GetChildOfType<T>(child);
				if (result != null)
					return result;
			}

			return null;
		}

		public static IEnumerable<T> FindChildrenOfType<T>(this FrameworkElement parent)
			where T : class
		{
			var result = new List<T>();
			if (parent == null)
				return result;

			if (parent.GetType() == typeof(T))
			{
				if (parent is T self)
					result.Add(self);
			}

			if (parent is Popup popup)
			{
				var child = popup.Child as FrameworkElement;
				result.AddRange(FindChildrenOfType<T>(child));
				return result;
			}

			var childCount = VisualTreeHelper.GetChildrenCount(parent);

			for (var i = 0; i < childCount; i++)
			{
				var child = VisualTreeHelper.GetChild(parent, i) as FrameworkElement;
				result.AddRange(FindChildrenOfType<T>(child));
			}

			return result;
		}

		public static T FindChild<T>(this FrameworkElement parent) where T : class
		{
			if (parent == null)
				return null;

			if (parent.GetType() == typeof(T))
				return parent as T;

			var childCount = VisualTreeHelper.GetChildrenCount(parent);

			for (var i = 0; i < childCount; i++)
			{
				var child = VisualTreeHelper.GetChild(parent, i) as FrameworkElement;
				var result = FindChild<T>(child);

				if (result != null)
					return result;
			}

			return null;
		}

		public static T GetVisualParent<T>(this Visual child) where T : Visual
		{
			if (VisualTreeHelper.GetParent(child) is not Visual visual)
				return null;

			var parent = visual as T ?? GetVisualParent<T>(visual);

			return parent;
		}

		public static Window FindActiveWindow()
		{
			if (!InvokingUtils.IsMainThread)
				throw new InvalidOperationException("Attempt to access window not from main thread.");

			return Application.Current?.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
		}

		public static Window GetMainWindow()
		{
			if (!InvokingUtils.IsMainThread)
				throw new InvalidOperationException("Attempt to access window not from main thread.");

			var mainWindow = Application.Current?.MainWindow;

			return mainWindow;
		}

		public static bool BringWindowToForeground(Window window)
		{
			try
			{
				if (window == null)
					return false;

				var wih = new WindowInteropHelper(window);
				var mainHwnd = wih.Handle;

				return SetForegroundWindow(mainHwnd) != 0;
			}
			catch (Exception ex)
			{
				throw new Exception("Error while bring windows to foreground for message box.", ex);
			}
		}

		public static void AddRange<T>(this ObservableCollection<T> sourceCollection, IEnumerable<T> additionalCollection)
		{
			if (additionalCollection == null)
				return;

			foreach (var item in additionalCollection)
				sourceCollection.Add(item);
		}

		[DllImport("user32.dll")]
		private static extern int SetForegroundWindow(IntPtr hWnd);
	}
}
