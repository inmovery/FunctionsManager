namespace FunctionsDesigner.Services.Interfaces
{
	public interface IMessageService
	{
		void ShowMessage(string message);

		bool ActionConfirmed(string message, string caption);
	}
}
