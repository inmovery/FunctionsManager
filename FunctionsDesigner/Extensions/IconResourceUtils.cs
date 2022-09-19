using System;
using System.Windows;
using System.Windows.Media;

namespace FunctionsDesigner.Extensions
{
	public class IconResourceUtils
	{
		private static readonly ResourceDictionary Instance = new ResourceDictionary()
		{
			Source = new Uri("pack://application:,,,/FunctionsDesigner;component/Styles/Icons.xaml")
		};

		public static PathGeometry GetPathTemplateByName(string name)
		{
			try
			{
				return (PathGeometry)Instance[name];
			}
			catch (Exception)
			{
				return new PathGeometry();
			}
		}

		public static GeometryDrawing GetDrawingTemplateByName(string name)
		{
			try
			{
				return (GeometryDrawing)Instance[name];
			}
			catch (Exception)
			{
				return new GeometryDrawing();
			}
		}
	}
}
