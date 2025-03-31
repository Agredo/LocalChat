using LocalChat.ViewModels.Pages;

namespace LocalChat.Maui.Views.Pages;

public partial class StartPage : ContentPage
{
	public StartPage(StartPageViewModel startPageViewModel)
	{
		InitializeComponent();

        BindingContext = startPageViewModel;
    }
}