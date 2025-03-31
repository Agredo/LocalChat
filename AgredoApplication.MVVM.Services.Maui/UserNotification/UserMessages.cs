using AgredoApplication.MVVM.Services.Abstractions.UserNotification;

namespace AgredoApplication.MVVM.Services.Maui.UserNotification;

public class UserMessages : IUserMessages
{
    public async Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
    {
        return await Application.Current.MainPage.DisplayActionSheet(title, destruction, "Yes", "No");
    }

    public async Task<bool> ShowMessageDialogAsync(string titel, string content, string accept, string cancel)
    {
        return await Application.Current.MainPage.DisplayAlert(titel, content, accept, cancel);
    }

    public async Task<bool> ShowMessageDialogAsync(string titel, string content, string accept)
    {
        return await Application.Current.MainPage.DisplayAlert(titel, content, accept, "OK");
    }
}
