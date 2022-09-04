using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Animation;
using Microsoft.Xaml.Behaviors;

namespace FunctionsDesigner.Behaviors
{
	public class WindowClosingBehavior : Behavior<Window>
	{
		protected override void OnAttached()
		{
			AssociatedObject.Closing += AssociatedObject_Closing;
		}

		protected override void OnDetaching()
		{
			AssociatedObject.Closing -= AssociatedObject_Closing;
		}

		private void AssociatedObject_Closing(object sender, CancelEventArgs e)
		{
			if (sender is not Window window)
				return;

			window.Closing -= AssociatedObject_Closing;
			e.Cancel = true;

			var animation = new DoubleAnimation(0, TimeSpan.FromSeconds(0.5));
			animation.Completed += (s, _) => window.Close();

			window.BeginAnimation(UIElement.OpacityProperty, animation);
		}

	}
}
