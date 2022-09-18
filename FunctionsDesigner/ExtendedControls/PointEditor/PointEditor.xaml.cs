using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FunctionsDesigner.ExtendedControls.PointEditor
{
	public partial class PointEditor : UserControl
	{

		public static readonly DependencyProperty ParameterXProperty = DependencyProperty.Register(
			nameof(ParameterX),
			typeof(double),
			typeof(PointEditor),
			new FrameworkPropertyMetadata(0.0d, OnParameterXChanged));

		public static readonly DependencyProperty ParameterYProperty = DependencyProperty.Register(
			nameof(ParameterY),
			typeof(double),
			typeof(PointEditor),
			new FrameworkPropertyMetadata(0.0d, OnParameterYChanged));

		public static readonly DependencyProperty DisableDeletionProperty = DependencyProperty.Register(
			nameof(DisableDeletion),
			typeof(bool),
			typeof(PointEditor),
			new PropertyMetadata(false));

		public static readonly RoutedEvent ParameterXChangedEvent = EventManager.RegisterRoutedEvent(
			nameof(ParameterXChanged),
			RoutingStrategy.Bubble,
			typeof(RoutedEventHandler),
			typeof(PointEditor));

		public static readonly RoutedEvent ParameterYChangedEvent = EventManager.RegisterRoutedEvent(
			nameof(ParameterYChanged),
			RoutingStrategy.Bubble,
			typeof(RoutedEventHandler),
			typeof(PointEditor));

		public static readonly DependencyProperty RemovePointCommandProperty = DependencyProperty.Register(
			nameof(RemovePointCommand),
			typeof(ICommand),
			typeof(PointEditor),
			new UIPropertyMetadata(default));

		public PointEditor()
		{
			InitializeComponent();
		}

		public double ParameterX
		{
			get => (double)GetValue(ParameterXProperty);
			set => SetValue(ParameterXProperty, value);
		}

		public double ParameterY
		{
			get => (double)GetValue(ParameterYProperty);
			set => SetValue(ParameterXProperty, value);
		}

		public bool DisableDeletion
		{
			get => (bool)GetValue(DisableDeletionProperty);
			set => SetValue(DisableDeletionProperty, value);
		}

		public event RoutedEventHandler ParameterXChanged
		{
			add => AddHandler(ParameterXChangedEvent, value);
			remove => RemoveHandler(ParameterXChangedEvent, value);
		}

		public event RoutedEventHandler ParameterYChanged
		{
			add => AddHandler(ParameterYChangedEvent, value);
			remove => RemoveHandler(ParameterYChangedEvent, value);
		}

		public ICommand RemovePointCommand
		{
			get => (ICommand)GetValue(RemovePointCommandProperty);
			set => SetValue(RemovePointCommandProperty, value);
		}

		public static void OnParameterXChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			var pointEditor = (PointEditor)obj;
			// ToDo: change points position in chart
		}

		public static void OnParameterYChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			var pointEditor = (PointEditor)obj;
			// ToDo: change points position in chart
		}
	}
}
