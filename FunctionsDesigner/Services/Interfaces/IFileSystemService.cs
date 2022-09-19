namespace FunctionsDesigner.Services.Interfaces
{
	public interface IFileSystemService
	{
		bool OpenFile(string extensionDescription, string extension, out string selectedFile);

		bool SaveFile(string extensionDescription, string extension, out string filePath);
	}
}
