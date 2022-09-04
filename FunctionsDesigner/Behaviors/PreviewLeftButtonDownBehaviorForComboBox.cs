using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using FunctionsDesigner.Extensions;
using Microsoft.Xaml.Behaviors;

namespace FunctionsDesigner.Behaviors
{
	/// <summary>
	/// Поведение для ComboBox. По возможности не дает ему выпадать за пределы окна. 
	/// Проверяет высоту выпадающего списка и выход за пределы окна сверху и снизу.
	/// Меняет направление выпадающего списка на верхнее, если список не влазит вниз, но влазит вверх.
	/// Если список не влазит в обоих направлениях, выпадает вниз. 
	/// </summary>
	public class PreviewLeftButtonDownBehaviorForComboBox : Behavior<FrameworkElement>
	{
		private const int ItemHeight = 27;
		private const int MaxItemsCountInDropdown = 13;
		private const int MaxDropdownHeight = 350;

		protected override void OnAttached()
		{
			base.OnAttached();
			AssociatedObject.PreviewMouseLeftButtonDown += OnMouseLeftButtonDown;
		}

		private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			try
			{
				var grid = sender as Grid;
				var comboBox = grid?.GetVisualParent<ComboBox>();
				if (comboBox == null)
					return;

				var comboBoxPosition = comboBox.PointToScreen(new Point(0, 0));
				var dropDownContainer = comboBox.FindChild<Popup>();
				if (dropDownContainer == null)
					return;

				var window = AncestorUtils.FindActiveWindow();

				dropDownContainer.Placement = PlacementMode.Bottom;

				double additionalHeight;

				if (comboBox.Items.Count > MaxItemsCountInDropdown)
					additionalHeight = MaxDropdownHeight;
				else
					additionalHeight = ItemHeight * comboBox.Items.Count;

				var dropDownBottom = comboBoxPosition.Y + comboBox.ActualHeight + additionalHeight;
				var dropDownTop = comboBoxPosition.Y - additionalHeight;

				// Выход за границы окна сверху
				var extraTop = window.Top - dropDownTop;

				// Выпадающий список не влазит вниз, но влазит вверх
				var canDropdownTop = (dropDownBottom > window.ActualHeight + window.Top ||
									 dropDownBottom > SystemParameters.PrimaryScreenHeight)
									 && extraTop < 0;

				if (canDropdownTop)
					dropDownContainer.Placement = PlacementMode.Top;
			}
			catch
			{
				e.Handled = false;
			}
		}
	}
}
