using IMediaPicker = AgredoApplication.MVVM.Services.Abstractions.Media.IMediaPicker;
using MauiMediaPicker = Microsoft.Maui.Media.MediaPicker;

namespace AgredoApplication.MVVM.Services.Maui.Media;

public class MediaPicker : IMediaPicker
{
    public bool IsCaptureSupported { get; } = MauiMediaPicker.IsCaptureSupported;

    public async Task<byte[]> CapturePhotoAsync(string title = "")
    {
        return await CaptureMediaAsync(MauiMediaPicker.CapturePhotoAsync, title);
    }

    public async Task<byte[]> CaptureVideoAsync(string title = "")
    {
        return await CaptureMediaAsync(MauiMediaPicker.CaptureVideoAsync, title);
    }

    public async Task<byte[]> PickPhotoAsync(string title = "")
    {
        return await CaptureMediaAsync(MauiMediaPicker.PickPhotoAsync, title);
    }

    public async Task<byte[]> PickVideoAsync(string title = "")
    {
        return await CaptureMediaAsync(MauiMediaPicker.PickVideoAsync, title);
    }

    private async Task<byte[]> CaptureMediaAsync(Func<MediaPickerOptions, Task<FileResult>> captureMethod, string title)
    {
        if (IsCaptureSupported)
        {
            FileResult fileResult = await captureMethod(new MediaPickerOptions() { Title = title });
            if (fileResult != null)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await (await fileResult.OpenReadAsync()).CopyToAsync(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }
        return new byte[0];
    }
}
