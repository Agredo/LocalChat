using IPreferences = AgredoApplication.MVVM.Services.Abstractions.Storage.IPreferences;
using MauiPreferences = Microsoft.Maui.Storage.Preferences;

namespace AgredoApplication.MVVM.Services.Maui.Storage;

public class Preferences : IPreferences
{
    public void Clear(string sharedName)
    {
        MauiPreferences.Clear(sharedName);
    }

    public bool ContainsKey(string key, string sharedName)
    {
        return MauiPreferences.ContainsKey(key, sharedName);
    }

    public string Get(string key, string defaultValue, string sharedName)
    {
        return MauiPreferences.Get(key, defaultValue, sharedName);
    }

    public bool Get(string key, bool defaultValue, string sharedName)
    {
        return MauiPreferences.Get(key, defaultValue, sharedName);
    }

    public int Get(string key, int defaultValue, string sharedName)
    {
        return MauiPreferences.Get(key, defaultValue, sharedName);
    }

    public double Get(string key, double defaultValue, string sharedName)
    {
        return MauiPreferences.Get(key, defaultValue, sharedName);
    }

    public float Get(string key, float defaultValue, string sharedName)
    {
        return MauiPreferences.Get(key, defaultValue, sharedName);
    }

    public long Get(string key, long defaultValue, string sharedName)
    {
        return MauiPreferences.Get(key, defaultValue, sharedName);
    }

    public void Remove(string key, string sharedName)
    {
        MauiPreferences.Remove(key, sharedName);
    }

    public void Set(string key, string value, string sharedName)
    {
        MauiPreferences.Set(key, value, sharedName);
    }

    public void Set(string key, bool value, string sharedName)
    {
        MauiPreferences.Set(key, value, sharedName);
    }

    public void Set(string key, int value, string sharedName)
    {
        MauiPreferences.Set(key, value, sharedName);
    }

    public void Set(string key, double value, string sharedName)
    {
        MauiPreferences.Set(key, value, sharedName);
    }

    public void Set(string key, float value, string sharedName)
    {
        MauiPreferences.Set(key, value, sharedName);
    }

    public void Set(string key, long value, string sharedName)
    {
        MauiPreferences.Set(key, value, sharedName);
    }
}
