using Maui.BindableProperty.Generator.Core;
using Microsoft.SemanticKernel.ChatCompletion;
using System.Data;

namespace LocalChat.Maui.Views.Chat; 

public partial class ChatBubble : ContentView
{

    [AutoBindable(OnChanged = nameof(IsRoleUpdated))]
    private readonly AuthorRole role;

    [AutoBindable(OnChanged = nameof(TextContentUpdated))]
    private string textContent;

    private void TextContentUpdated(string oldValue, string newValue)
    {
        if (newValue != textContent && newValue is not null)
        {
            TextContentLabel.Text = newValue;
        }
    }

    private void IsRoleUpdated(AuthorRole newValue)
    {
        Role = newValue;

        if (Role == AuthorRole.User)
        { 
            BubbleFrame.Style = (Style)Resources["UserBubbleStyle"];
        }
        else
        {
            BubbleFrame.Style = (Style)Resources["AssistantBubbleStyle"];
        }
    }


    [AutoBindable(OnChanged = nameof(TimestampUpdated))]
    private readonly DateTime timestamp;

    private void TimestampUpdated(DateTime newValue)
    {
        if (newValue != timestamp)
        {
            TimestampLabel.Text = newValue.ToString("HH:mm");
        }
    }



    public ChatBubble()
    {
        InitializeComponent();
    }
}
