using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using AgredoApplication.MVVM.Services.Abstractions.Navigation;

namespace AgredoApplication.MVVM.Services.Maui.Navigation;

public class PopupNavigationService : IPopupNavigationService
{
    public IDictionary<string, object> Parameters
    {
        get
        {
            var parameters = Parameters;
            Parameters.Clear();
            return parameters;
        }
        set
        {
            Parameters = value;
        }
    }

    public string EditDocumentLabelPopup { get; set; } = "Nexile.Maui.Views.Popups.EditDocumentLabelPopup, Nexile.Maui.Views";
    public IPopupService PopupService { get; }

    public PopupNavigationService(IPopupService popupService)
    {
        PopupService = popupService;
    }

    public async Task<TResult> ShowPopupByViewModel<TViewModel, TResult>(string titel, string description) where TViewModel : BasePopupViewModel where TResult : new()
    {
        return (TResult)await PopupService.ShowPopupAsync<TViewModel>(onPresenting: viewModel => viewModel.SetContent(titel, description));
    }

    public void ShowPopup(string popupTypeName)
    {
        var popup = GetPopup(popupTypeName);
        Application.Current.MainPage.ShowPopup(popup);
    }

    public void ShowPopup(string popupTypeName, Dictionary<string, object> parameters)
    {
        Parameters = parameters;

        ShowPopup(popupTypeName);
    }


    public async Task<T> ShowPopup<T>(string popupTypeName)
    {
        var popup = GetPopup(popupTypeName);
        return (T)await Application.Current.MainPage.ShowPopupAsync(popup);

    }

    public async Task<T> ShowPopup<T>(string popupTypeName, Dictionary<string, object> parameters)
    {
        Parameters = parameters;

        return await ShowPopup<T>(popupTypeName);
    }

    private Popup GetPopup(string popupTypeName)
    {
        Type popupType = Type.GetType(popupTypeName);

        var popup = Activator.CreateInstance(popupType);

        if (popup is Popup)
        {
            return popup as Popup;
        }
        else
        {
            throw new ArgumentException("Popup is not of type Popup");
        }
    }
}
