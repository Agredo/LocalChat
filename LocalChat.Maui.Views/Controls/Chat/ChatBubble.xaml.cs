namespace LocalChat.Maui.Views.Chat;

public partial class ChatBubble : ContentView
{
	public ChatBubble()
	{
		InitializeComponent();
	}

    /// <summary>
    /// Erstellt eine neue Chat-Bubble mit dem angegebenen Inhalt
    /// </summary>
    /// <param name="content">Der anzuzeigende Inhalt</param>
    /// <param name="isUserMessage">Gibt an, ob die Nachricht vom Benutzer stammt</param>
    /// <param name="timestamp">Der Zeitstempel der Nachricht</param>
    public ChatBubble(View content, bool isUserMessage, DateTime timestamp)
    {
        InitializeComponent();

        // Inhalt setzen
        ContentContainer.Content = content;

        // Stil basierend auf Absender setzen
        BubbleFrame.Style = isUserMessage
            ? (Style)Resources["UserBubbleStyle"]
            : (Style)Resources["AssistantBubbleStyle"];

        // Zeitstempel setzen
        TimestampLabel.Text = timestamp.ToString("HH:mm");
    }
}