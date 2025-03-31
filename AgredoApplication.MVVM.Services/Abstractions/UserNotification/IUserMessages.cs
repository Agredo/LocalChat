namespace AgredoApplication.MVVM.Services.Abstractions.UserNotification;

public interface IUserMessages
{
    Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons);
    public Task<bool> ShowMessageDialogAsync(string titel, string content, string accept, string cancel);
    public Task<bool> ShowMessageDialogAsync(string titel, string content, string accept);
}
