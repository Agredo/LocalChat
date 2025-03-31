namespace AgredoApplication.MVVM.Services.Abstractions.Navigation;

public interface IShellRoutingService
{
    public Dictionary<string, string> Routes { get; set; }

    public void AddRoute(string id, string route);

    public void RemoveRoute(string id);

    public string GetRoute(IRoutes id);
}
