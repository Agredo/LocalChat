namespace AgredoApplication.MVVM.Services.Abstractions.Media;

/// <summary>
/// Represents a media picker service that allows capturing and picking photos and videos.
/// </summary>
public interface IMediaPicker
{
    /// <summary>
    /// Gets or sets a value indicating whether capturing is supported.
    /// </summary>
    bool IsCaptureSupported { get; }

    /// <summary>
    /// Asynchronously captures a photo.
    /// </summary>
    /// <param name="title">The title of the capture.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the captured photo as a byte array.</returns>
    Task<byte[]> CapturePhotoAsync(string title = "");

    /// <summary>
    /// Asynchronously captures a video.
    /// </summary>
    /// <param name="title">The title of the capture.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the captured video as a byte array.</returns>
    Task<byte[]> CaptureVideoAsync(string title = "");

    /// <summary>
    /// Asynchronously picks a photo from the media library.
    /// </summary>
    /// <param name="title">The title of the pick.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the picked photo as a byte array.</returns>
    Task<byte[]> PickPhotoAsync(string title = "");

    /// <summary>
    /// Asynchronously picks a video from the media library.
    /// </summary>
    /// <param name="title">The title of the pick.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the picked video as a byte array.</returns>
    Task<byte[]> PickVideoAsync(string title = "");
}
