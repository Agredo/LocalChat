namespace AgredoApplication.MVVM.Services.Abstractions.Network;

public interface IFileDownloader
{
    Task<byte[]> DownloadFileAsync(string url, IProgress<double> progress = null);
}
