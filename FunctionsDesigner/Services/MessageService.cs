using System.Windows;
using FunctionsDesigner.Services.Interfaces;

namespace FunctionsDesigner.Services
{
	public class MessageService : IMessageService
	{
		public void ShowMessage(string message)
		{
			MessageBox.Show(message);
		}

		public bool ActionConfirmed(string message, string caption)
		{
			return MessageBox.Show(
				message,
				caption,
				MessageBoxButton.YesNo) == MessageBoxResult.Yes;
		}
	}
}
