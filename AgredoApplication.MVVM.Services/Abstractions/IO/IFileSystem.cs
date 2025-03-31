namespace AgredoApplication.MVVM.Services.Abstractions.IO;

public interface IFileSystem
{
    public string AppDataDirectory { get; }
    public Task<string> PickFilePath();
    public Task<string> PickDirectoryPath();

    public Task<string> CopyFolderToAppDirectory(string sourceFolderPath, string destinationFolderName);
    public Task<bool> StoreFile(byte[] data, string path, string fileName);

}
