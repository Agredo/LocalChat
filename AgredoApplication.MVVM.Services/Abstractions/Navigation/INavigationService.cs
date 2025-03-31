namespace AgredoApplication.MVVM.Services.Abstractions.Navigation;

public interface INavigationService
{
    public Dictionary<string, object> Parameters { get; set; }

    Task NavigateBack();
    public Task ShellNavigationTo(string route);
    public Task ShellNavigationTo(string route, bool animate);
    public Task ShellNavigationTo(string route, Dictionary<string, object> parameters);
    public Task ShellNavigationTo(string route, bool animate, IDictionary<string, object> parameters);
}
