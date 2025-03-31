namespace AgredoApplication.MVVM.Services.Abstractions.Navigation;

public interface IPopupNavigationService
{
    public IDictionary<string, object> Parameters { get; set; }
    public string EditDocumentLabelPopup { get; set; }

    void ShowPopup(string popupTypeName);
    void ShowPopup(string popupTypeName, Dictionary<string, object> parameters);

    Task<TResult> ShowPopupByViewModel<TViewModel, TResult>(string titel, string description) where TViewModel : BasePopupViewModel where TResult : new();

    Task<T> ShowPopup<T>(string popupTypeName);
    Task<T> ShowPopup<T>(string popupTypeName, Dictionary<string, object> parameters);
}
