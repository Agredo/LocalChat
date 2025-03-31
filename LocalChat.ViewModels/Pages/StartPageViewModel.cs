using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace LocalChat.ViewModels.Pages;

public partial class StartPageViewModel : ObservableObject
{
    [ObservableProperty]
    private ChatHistory chatHistory = [];

    public StartPageViewModel()
    {
        fillChatHitoryWithTestData();
    }

    private void fillChatHitoryWithTestData()
    {
        var history = new ChatHistory();
        //add some test data to the chat history
        history.Add(new Microsoft.SemanticKernel.ChatMessageContent(AuthorRole.User, "Hello, who are you?"));
        history.Add(new Microsoft.SemanticKernel.ChatMessageContent(AuthorRole.Assistant, "I'm a chatbot, nice to meet you!"));
        history.Add(new Microsoft.SemanticKernel.ChatMessageContent(AuthorRole.User, "What can you do?"));
        history.Add(new Microsoft.SemanticKernel.ChatMessageContent(AuthorRole.Assistant, "I can help you with various tasks, like answering questions, providing information, or even playing games!"));
        history.Add(new Microsoft.SemanticKernel.ChatMessageContent(AuthorRole.User, "That's cool!"));

        ChatHistory = history;
    }
}
