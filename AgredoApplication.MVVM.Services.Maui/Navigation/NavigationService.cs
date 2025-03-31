using AgredoApplication.MVVM.Services.Abstractions.Navigation;

namespace AgredoApplication.MVVM.Services.Maui.Navigation;

public class NavigationService : INavigationService
{
    public Dictionary<string, object> Parameters { get; set; }

    public async Task ShellNavigationTo(string route)
    {
        await Shell.Current.GoToAsync(route);
    }

    public async Task ShellNavigationTo(string route, bool animate)
    {
        await Shell.Current.GoToAsync(route, animate);
    }

    public async Task ShellNavigationTo(string route, Dictionary<string, object> parameters)
    {
        Parameters = parameters;
        await Shell.Current.GoToAsync(route, parameters);
    }

    public Task ShellNavigationTo(string route, bool animate, IDictionary<string, object> parameters)
    {
        Parameters.Add(route, parameters);
        return Shell.Current.GoToAsync(route, animate, parameters);
    }
    public async Task NavigateBack()
    {
        await Shell.Current.Navigation.PopAsync();
    }
}
