using LocalChat.Maui.Views.Chat.Messages;

namespace LocalChat.Maui.Views.Chat.TemplateSelector;

/// <summary>
/// DataTemplateSelector für die verschiedenen Nachrichtentypen im Chat
/// </summary>
public class ChatMessageTemplateSelector : DataTemplateSelector
{
    /// <summary>
    /// Template für Text-Nachrichten
    /// </summary>
    public DataTemplate TextMessageTemplate { get; set; }

    /// <summary>
    /// Template für Bild-Nachrichten
    /// </summary>
    public DataTemplate ImageMessageTemplate { get; set; }

    /// <summary>
    /// Template für Code-Nachrichten
    /// </summary>
    public DataTemplate CodeMessageTemplate { get; set; }

    public DataTemplate RichTextMessageTemplate { get; set; }

    /// <summary>
    /// Wählt das passende Template basierend auf dem Nachrichtentyp aus
    /// </summary>
    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item is TextChatMessage)
            return TextMessageTemplate;

        if (item is ImageChatMessage)
            return ImageMessageTemplate;

        if (item is CodeChatMessage)
            return CodeMessageTemplate;

        if (item is RichTextChatMessage)
            return RichTextMessageTemplate;

        // Fallback auf Text-Template für unbekannte Typen
        return TextMessageTemplate;
    }
}