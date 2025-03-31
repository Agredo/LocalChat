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
    private string currentMessage = string.Empty;

    /// <summary>
    /// Der Chatverlauf
    /// </summary>
    [AutoBindable(OnChanged = nameof(OnChatHistoryChanged))]
    private ChatHistory chatHistory = [];

    /// <summary>
    /// Der Befehl, der beim Senden einer Nachricht ausgef�hrt wird
    /// </summary>
    [AutoBindable]
    private ICommand sendMessageCommand;

    /// <summary>
    /// Gibt an, ob gerade eine Nachricht verarbeitet wird
    /// </summary>
    [AutoBindable]
    private bool isProcessing;

    /// <summary>
    /// Gibt an, ob eine Nachricht gesendet werden kann
    /// </summary>
    [AutoBindable]
    private bool canSendMessage;

    #endregion

    #region Konstruktoren

    /// <summary>
    /// Erstellt eine neue Instanz des ChatView-Controls
    /// </summary>
    public ChatView()
    {
        InitializeComponent();

        // Event-Handler f�r die Enter-Taste im Editor
        MessageInput.Completed += OnMessageInputCompleted;

        // Aktualisiere den Status f�r das Senden von Nachrichten
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
        // Registriere die PropertyChanged-Events f�r die ObservableCollection
        if (ChatHistory is INotifyPropertyChanged newNotify)
        {
            newNotify.PropertyChanged += OnChatHistoryPropertyChanged;
        }
        // Aktualisiere die Anzeige
        MessagesCollection.ItemsSource = ChatHistory;
    }

    private void OnChatHistoryPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        // Wenn sich die ChatHistory �ndert, scrolle zum Ende
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
