using Maui.BindableProperty.Generator.Core;

namespace LocalChat.Maui.Views.Chat;

public partial class TextChatMessage : ChatMessageBase
{
    /// <summary>
    /// Der Textinhalt der Nachricht
    /// </summary>
    [AutoBindable(DefaultValue = "")]
    private string messageText;

    /// <summary>
    /// Erstellt eine neue Instanz der TextChatMessage-Klasse
    /// </summary>
    public TextChatMessage()
    {
    }

    /// <summary>
    /// Erstellt eine neue Instanz der TextChatMessage-Klasse mit dem angegebenen Text
    /// </summary>
    /// <param name="text">Der Textinhalt der Nachricht</param>
    /// <param name="isUserMessage">Gibt an, ob die Nachricht vom Benutzer stammt</param>
    public TextChatMessage(string text, bool isUserMessage = false)
    {
        MessageText = text;
        IsUserMessage = isUserMessage;
    }
}
