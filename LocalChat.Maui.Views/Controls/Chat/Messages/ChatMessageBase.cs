using Maui.BindableProperty.Generator.Core;

namespace LocalChat.Maui.Views.Chat;

/// <summary>
/// Basis-Klasse für alle Chat-Nachrichtentypen
/// </summary>
public abstract partial class ChatMessageBase : ContentView
{
    /// <summary>
    /// Gibt an, ob es sich um eine Nachricht des Benutzers handelt (true) oder des KI-Assistenten (false)
    /// </summary>
    [AutoBindable]
    private bool isUserMessage;

    /// <summary>
    /// Zeitstempel der Nachricht
    /// </summary>
    [AutoBindable]
    private DateTime timestamp = DateTime.Now;

    /// <summary>
    /// Eine eindeutige ID für die Nachricht
    /// </summary>
    [AutoBindable]
    private string messageId = Guid.NewGuid().ToString();

    /// <summary>
    /// Erstellt eine neue Instanz der ChatMessageBase-Klasse
    /// </summary>
    public ChatMessageBase()
    {
        HorizontalOptions = LayoutOptions.Fill;
    }
}
