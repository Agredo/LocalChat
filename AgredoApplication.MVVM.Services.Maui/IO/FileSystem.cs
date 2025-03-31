using CommunityToolkit.Maui.Storage;
using IFileSystem = AgredoApplication.MVVM.Services.Abstractions.IO.IFileSystem;
using MauiFileSystem = Microsoft.Maui.Storage.FileSystem;

namespace AgredoApplication.MVVM.Services.Maui.IO;

public class FileSystem : IFileSystem
{
    public string AppDataDirectory => MauiFileSystem.AppDataDirectory;

    public async Task<string> CopyFolderToAppDirectory(string sourceFolderPath, string destinationFolderName = "Model")
    {
        string appDataPath = MauiFileSystem.AppDataDirectory;
        string destinationPath = Path.Combine(appDataPath, destinationFolderName);

        if (!Directory.Exists(destinationPath))
        {
            Directory.CreateDirectory(destinationPath);
        }

        foreach (string filePath in Directory.GetFiles(sourceFolderPath, "*.*", SearchOption.AllDirectories))
        {
            string relativePath = Path.GetRelativePath(sourceFolderPath, filePath);
            string targetPath = Path.Combine(destinationPath, relativePath);

            string targetDirectory = Path.GetDirectoryName(targetPath);
            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            using Stream sourceStream = await MauiFileSystem.OpenAppPackageFileAsync(filePath);
            using FileStream targetStream = File.Create(targetPath);
            await sourceStream.CopyToAsync(targetStream);
        }

        return destinationPath;
    }

    public async Task<string> PickDirectoryPath()
    {
        FolderPickerResult value = await FolderPicker.PickAsync(CancellationToken.None);

        if (value != null && value.Folder != null)
        {
            return value.Folder.Path;
        }

        return string.Empty;
    }

    public async Task<string> PickFilePath()
    {
        FileResult value = await FilePicker.PickAsync(PickOptions.Default);

        if (value != null)
        {
            return value.FullPath;
        }

        return string.Empty;
    }

    public async Task<bool> StoreFile(byte[] data, string path, string fileName)
    {
        try
        {
            string filePath = Path.Combine(AppDataDirectory, path, fileName);
            await File.WriteAllBytesAsync(filePath, data);
            return await Task.FromResult(true);
        }
        catch (Exception) 
        {
            return await Task.FromResult(false);
        }
    }
}
