using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LocalChat.AI.Models;
using Microsoft.SemanticKernel.ChatCompletion;
using System.Diagnostics;

namespace LocalChat.ViewModels.Pages;

public partial class StartPageViewModel : ObservableObject
{
    [ObservableProperty]
    private ChatHistory chatHistory = new ChatHistory();

    [ObservableProperty]
    private string currentMessage = "Write me a C# Program to print the fibonacci sequence!";

    [ObservableProperty]
    private bool isProcessing;

    public LocalOnnxChatModel LocalOnnxChatModel { get; }

    public StartPageViewModel()
    {
        IsProcessing = false;
        //FillChatHistoryWithTestData();

        LocalOnnxChatModel = new LocalOnnxChatModel("C:\\Users\\chris\\.aitk\\models\\DeepSeek\\DeepSeek-R1-Distilled-NPU-Optimized", ModelType.DeepSeek, ChatHistory);

        LocalOnnxChatModel.initialize();

        LocalOnnxChatModel.OnTokenGenerated += (token) =>
        {
            // Handle token generation events
            if (token != null)
            {
                // Display the generated token
                Debug.Write(token);
            }
        };

        LocalOnnxChatModel.OnGenerationMetrics += (metrics) =>
        {
            // Handle generation metrics events
            if (metrics != null)
            {
                // Display the generation metrics
                Debug.WriteLine(string.Empty);
                Debug.WriteLine($"Generated {metrics.TotalTokens} tokens in {metrics.TokensPerSecond} Tokens/ms");
            }
        };
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

        ChatHistory = new ChatHistory(ChatHistory);

        // Set processing state
        IsProcessing = true;
        // Use streaming generation
        var response = await LocalOnnxChatModel.SendMessageStreamingAsync(
            userMessage
        );

        // Add response to chat
        ChatHistory.AddAssistantMessage(response.Content);

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
                    ChatHistory = new ChatHistory(ChatHistory);
                    break;
            }
        }
        catch (Exception ex)
        {
            ChatHistory.AddAssistantMessage($"Error: {ex.Message}");
            ChatHistory = new ChatHistory(ChatHistory);
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
