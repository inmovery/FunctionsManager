using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FunctionsDesigner.ExtendedControls
{
	public class WheelSpeedScrollViewer : ScrollViewer
	{
		public static readonly DependencyProperty SpeedFactorProperty = DependencyProperty.Register(
			nameof(SpeedFactor),
			typeof(double),
			typeof(WheelSpeedScrollViewer),
			new PropertyMetadata(0.4d));

		public double SpeedFactor
		{
			get => (double)GetValue(SpeedFactorProperty);
			set => SetValue(SpeedFactorProperty, value);
		}

		protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
		{
			var isScrollBarVisible = ComputedVerticalScrollBarVisibility == Visibility.Visible;
			if (e.Handled || ScrollInfo is not ScrollContentPresenter scrollContentPresenter || !isScrollBarVisible)
				return;

			scrollContentPresenter.SetVerticalOffset(VerticalOffset - e.Delta * SpeedFactor);
			e.Handled = true;
		}
	}
}
