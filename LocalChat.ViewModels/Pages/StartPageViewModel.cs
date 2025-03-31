using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.SemanticKernel.ChatCompletion;

namespace LocalChat.ViewModels.Pages;

public partial class StartPageViewModel : ObservableObject
{
    [ObservableProperty]
    private ChatHistory chatHistory = new ChatHistory();

    [ObservableProperty]
    private string currentMessage = string.Empty;

    [ObservableProperty]
    private bool isProcessing;

    public StartPageViewModel()
    {
        FillChatHistoryWithTestData();
    }

    [RelayCommand]
    private async Task SendMessageAsync()
    {
        if (string.IsNullOrWhiteSpace(CurrentMessage))
            return;

        // Add user message to chat
        ChatHistory.AddUserMessage(CurrentMessage);

        // Store message and clear input field
        string userMessage = CurrentMessage;
        CurrentMessage = string.Empty;

        // Set processing state
        IsProcessing = true;

        try
        {
            // Simulate processing delay
            await Task.Delay(1000);

            // Add a response (in a real app, this would be from an LLM)
            switch (userMessage.ToLower())
            {
                case var msg when msg.Contains("code"):
                    AddCodeExampleResponse();
                    break;
                case var msg when msg.Contains("image"):
                    AddImageExampleResponse();
                    break;
                case var msg when msg.Contains("richtext"):
                    AddRichTextExampleResponse();
                    break;
                default:
                    ChatHistory.AddAssistantMessage($"You said: {userMessage}. This is a test response from the assistant.");
                    break;
            }
        }
        catch (Exception ex)
        {
            ChatHistory.AddAssistantMessage($"Error: {ex.Message}");
        }
        finally
        {
            IsProcessing = false;
        }
    }

    private void FillChatHistoryWithTestData()
    {
        // Add sample conversation
        ChatHistory.AddUserMessage("Hello, who are you?");
        ChatHistory.AddAssistantMessage("I'm a local chatbot powered by ONNX runtime. I can help answer questions and assist with various tasks.");

        ChatHistory.AddUserMessage("What can you do?");
        ChatHistory.AddAssistantMessage("I can help with information, creating code examples, answering questions, and more - all while running locally on your device using the NPU for acceleration.");

        ChatHistory.AddUserMessage("Show me some example code");
        AddCodeExampleResponse();
    }

    private void AddCodeExampleResponse()
    {
        string code = @"public class HelloWorld 
{
    public static void Main(string[] args) 
    {
        Console.WriteLine(""Hello, World!"");
        
        // A simple loop example
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($""Count: {i}"");
        }
    }
}";

        //ChatHistory.AddAssistantMessage(code, "csharp", "Here's a simple C# example:");
    }

    private void AddImageExampleResponse()
    {
        // In a real app, you would load an actual image
        // For this test implementation, we'll create a placeholder
        //var imageSource = new FileImageSource { File = "dotnet_bot.png" };
        //ChatHistory.AddAssistantImageMessage(imageSource, "This is a sample image caption");
    }

    private void AddRichTextExampleResponse()
    {
        string content = @"Here's an example of rich text with embedded code:

```csharp
public class Example 
{
    public void ShowMessage() 
    {
        Console.WriteLine(""This is part of a rich text message"");
    }
}
```

And then we can continue with more text explanation after the code block. 
This demonstrates how a single message can contain both text and code elements.";

        // Add this as RichTextChatMessage
        //ChatHistory.Add(new RichChatMessage(content, false));
    }
}
