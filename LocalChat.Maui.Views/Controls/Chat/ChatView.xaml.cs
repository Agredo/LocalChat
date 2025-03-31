using Maui.BindableProperty.Generator.Core;
using Microsoft.SemanticKernel.ChatCompletion;
using System.ComponentModel;
using System.Windows.Input;

namespace LocalChat.Maui.Views.Chat;

public partial class ChatView : ContentView
{
    #region Bindable Properties

    /// <summary>
    /// Die aktuelle Nachricht in der Eingabe
    /// </summary>
    [AutoBindable(OnChanged = nameof(OnCurrentMessageChanged))]
    private string _currentMessage = string.Empty;

    /// <summary>
    /// Der Chatverlauf
    /// </summary>
    [AutoBindable(OnChanged = nameof(OnChatHistoryChanged))]
    private ChatHistory _chatHistory = new ChatHistory();

    /// <summary>
    /// Der Befehl, der beim Senden einer Nachricht ausgeführt wird
    /// </summary>
    [AutoBindable]
    private ICommand _sendMessageCommand;

    /// <summary>
    /// Gibt an, ob gerade eine Nachricht verarbeitet wird
    /// </summary>
    [AutoBindable]
    private bool _isProcessing;

    /// <summary>
    /// Gibt an, ob eine Nachricht gesendet werden kann
    /// </summary>
    [AutoBindable]
    private bool _canSendMessage;

    #endregion

    #region Konstruktoren

    /// <summary>
    /// Erstellt eine neue Instanz des ChatView-Controls
    /// </summary>
    public ChatView()
    {
        InitializeComponent();

        // Event-Handler für die Enter-Taste im Editor
        MessageInput.Completed += OnMessageInputCompleted;

        // Aktualisiere den Status für das Senden von Nachrichten
        UpdateCanSendMessage();
    }

    #endregion

    #region Event Handler

    private void OnCurrentMessageChanged()
    {
        UpdateCanSendMessage();
    }

    private void OnChatHistoryChanged()
    {
        // Registriere die PropertyChanged-Events für die ObservableCollection
        if (ChatHistory is INotifyPropertyChanged newNotify)
        {
            newNotify.PropertyChanged += OnChatHistoryPropertyChanged;
        }

        // Aktualisiere die Anzeige
        MessagesCollection.ItemsSource = ChatHistory;
    }

    private void OnChatHistoryPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        // Wenn sich die ChatHistory ändert, scrolle zum Ende
        if (ChatHistory.Count > 0)
        {
            MessagesCollection.ScrollTo(ChatHistory.Count - 1, animate: true);
        }
    }

    private void OnMessageInputCompleted(object sender, EventArgs e)
    {
        if (CanSendMessage && SendMessageCommand?.CanExecute(null) == true)
        {
            SendMessageCommand.Execute(null);
        }
    }

    #endregion

    #region Helper Methods

    /// <summary>
    /// Aktualisiert den Status, ob eine Nachricht gesendet werden kann
    /// </summary>
    private void UpdateCanSendMessage()
    {
        CanSendMessage = !IsProcessing && !string.IsNullOrWhiteSpace(CurrentMessage);
    }

    #endregion
}
