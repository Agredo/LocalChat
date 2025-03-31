using AgredoApplication.MVVM.Services.Abstractions.Navigation;

namespace AgredoApplication.MVVM.Services.Maui.Navigation;

public class ShellRoutingService : IShellRoutingService
{
    public Dictionary<string, string> Routes { get; set; }

    public void AddRoute(string id, string route)
    {
        throw new NotImplementedException();
    }

    public string GetRoute(IRoutes id)
    {
        throw new NotImplementedException();
    }

    public void RemoveRoute(string id)
    {
        throw new NotImplementedException();
    }
}
