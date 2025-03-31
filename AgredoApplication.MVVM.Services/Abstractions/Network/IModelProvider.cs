namespace AgredoApplication.MVVM.Services.Abstractions.Network
{
    public interface IModelProvider
    {
        Task<Dictionary<string, byte[]>> DownloadPhi35Async(IProgress<double> progress = null);
    }
}
