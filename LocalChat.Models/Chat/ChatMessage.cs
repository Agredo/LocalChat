namespace LocalChat.Models.Chat;

/// <summary>
/// Repräsentiert eine Nachricht im Chat-Verlauf
/// </summary>
public record ChatMessage
{
    /// <summary>
    /// Die eindeutige ID der Nachricht
    /// </summary>
    public string Id { get; init; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Der Inhalt der Nachricht
    /// </summary>
    public string Content { get; init; } = string.Empty;

    /// <summary>
    /// Der Typ der Nachricht (Text, Code, Bild)
    /// </summary>
    public MessageType Type { get; init; } = MessageType.Text;

    /// <summary>
    /// Gibt an, ob es sich um eine Nachricht des Benutzers handelt (true) oder des KI-Assistenten (false)
    /// </summary>
    public bool IsUserMessage { get; init; }

    /// <summary>
    /// Zeitstempel der Nachricht
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.Now;

    /// <summary>
    /// Zusätzliche Metadaten für die Nachricht (Sprache für Code, Bildpfad für Bilder, etc.)
    /// </summary>
    public Dictionary<string, string> Metadata { get; init; } = [];

    /// <summary>
    /// Konstruktor, um eine neue Instanz mit allen obligatorischen Informationen zu erstellen
    /// </summary>
    public ChatMessage(string content, MessageType type, bool isUserMessage, Dictionary<string, string> metadata)
    {
        Content = content;
        Type = type;
        IsUserMessage = isUserMessage;
        Metadata = metadata;
    }

    public ChatMessage() { }

    /// <summary>
    /// Erstellt eine neue Textnachricht vom Benutzer
    /// </summary>
    public static ChatMessage CreateUserMessage(string content)
    {
        return new ChatMessage
        {
            Content = content,
            Type = MessageType.Text,
            IsUserMessage = true
        };
    }

    /// <summary>
    /// Erstellt eine neue Textnachricht vom Assistenten
    /// </summary>
    public static ChatMessage CreateAssistantMessage(string content)
    {
        return new ChatMessage
        {
            Content = content,
            Type = MessageType.Text,
            IsUserMessage = false
        };
    }

    /// <summary>
    /// Erstellt eine neue Codenachricht vom Assistenten
    /// </summary>
    public static ChatMessage CreateCodeMessage(string code, string language = "csharp", string description = "")
    {
        return new ChatMessage
        {
            Content = code,
            Type = MessageType.Code,
            IsUserMessage = false,
            Metadata = new Dictionary<string, string>
            {
                ["Language"] = language,
                ["Description"] = description
            }
        };
    }

    /// <summary>
    /// Erstellt eine neue Bildnachricht
    /// </summary>
    public static ChatMessage CreateImageMessage(string imagePath, string caption = "", bool isUserMessage = false)
    {
        return new ChatMessage
        {
            Content = imagePath,
            Type = MessageType.Image,
            IsUserMessage = isUserMessage,
            Metadata = new Dictionary<string, string>
            {
                ["Caption"] = caption
            }
        };
    }



    ///// <summary>
    ///// Konvertiert diese ChatMessage zu einer AuthorRole für SemanticKernel
    ///// </summary>
    //public AuthorRole ToAuthorRole()
    //{
    //    return IsUserMessage ? AuthorRole.User : AuthorRole.Assistant;
    //}
}
