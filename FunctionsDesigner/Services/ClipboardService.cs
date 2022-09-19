using System.Windows;
using FunctionsDesigner.Services.Interfaces;

namespace FunctionsDesigner.Services
{
	public class ClipboardService : IClipboardService
	{
		public string GetText() => Clipboard.GetText();

		public void SetText(string text) => Clipboard.SetText(text);
	}
}
