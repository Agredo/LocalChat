namespace AgredoApplication.MVVM.Services.Abstractions.Storage;

public interface IPreferences
{
    bool ContainsKey(string key, string sharedName);
    void Remove(string key, string sharedName);
    void Clear(string sharedName);
    string Get(string key, string defaultValue, string sharedName);
    bool Get(string key, bool defaultValue, string sharedName);
    int Get(string key, int defaultValue, string sharedName);
    double Get(string key, double defaultValue, string sharedName);
    float Get(string key, float defaultValue, string sharedName);
    long Get(string key, long defaultValue, string sharedName);
    void Set(string key, string value, string sharedName);
    void Set(string key, bool value, string sharedName);
    void Set(string key, int value, string sharedName);
    void Set(string key, double value, string sharedName);
    void Set(string key, float value, string sharedName);
    void Set(string key, long value, string sharedName);
}
