using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntimeGenAI;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using System.Text;

namespace LocalChat.AI.Models;

/// <summary>
/// Handles chat interactions with local ONNX language models.
/// </summary>
public class LocalOnnxChatModel
{
    private readonly int MaxLength = 4096;
    private readonly int MinLength = 32;

    public string ModelPath { get; set; }
    public ModelType ModelType { get; set; } = ModelType.DeepSeek;
    public ChatHistory ChatHistory { get; }
    public Config Config { get; set; }
    public Model Model { get; set; }
    public Tokenizer Tokenizer { get; set; }

    //Event for every generated Token
    public event Action<string> OnTokenGenerated;

    //Event for Generation Metrics
    public event Action<GenerationMetrics> OnGenerationMetrics;


    public LocalOnnxChatModel(string modelPath, ModelType type, ChatHistory chatHistory)
    {
        ModelPath = modelPath;
        ModelType = type;
        ChatHistory = chatHistory;
    }

    public bool initialize()
    {
        try
        {
            Config = new Config(ModelPath);
            // Initialize the model and tokenizer
            Model = new Model(Config);
            Tokenizer = new Tokenizer(Model);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }
    }

    public void SetConfig(Config config)
    {
        Config = config;
    }

    public async Task<string> GenerateResponse(string prompt)
    {

        if (string.IsNullOrEmpty(prompt))
        {
            throw new ArgumentException("Prompt cannot be null or empty.", nameof(prompt));
        }
        if (MinLength < 0 || MaxLength <= 0 || MinLength > MaxLength)
        {
            throw new ArgumentOutOfRangeException("Invalid length parameters.");
        }
        ChatHistory.AddUserMessage(prompt);
        return GenerateResponse(ChatHistory, MinLength, MaxLength);
    }


    private string GenerateResponse(ChatHistory history, int minLength, int maxLength)
    {
        var sequences = Tokenizer.Encode(buildPropmt(ChatHistory));

        using GeneratorParams generatorParams = new GeneratorParams(Model);
        generatorParams.SetSearchOption("min_length", minLength);
        generatorParams.SetSearchOption("max_length", maxLength);

        using var tokenizerStream = Tokenizer.CreateStream();
        using var generator = new Generator(Model, generatorParams);
        generator.AppendTokenSequences(sequences);

        var watch = System.Diagnostics.Stopwatch.StartNew();
        StringBuilder responseBuilder = new();

        while (!generator.IsDone())
        {
            generator.GenerateNextToken();
            var tokens = generator.GetSequence(0).ToArray().Select(x => tokenizerStream.Decode(x)).ToList();
            string token = tokenizerStream.Decode(generator.GetSequence(0)[^1]);

            // Fire the token generated event
            OnTokenGenerated?.Invoke(token);

            responseBuilder.Append(token);
        }

        watch.Stop();

        var runTimeInSeconds = watch.Elapsed.TotalSeconds;
        var outputSequence = generator.GetSequence(0);
        var totalTokens = outputSequence.Length;

        //Fire the generation metrics event
        OnGenerationMetrics?.Invoke(new GenerationMetrics
        {
            TotalTokens = totalTokens,
            RunTimeInSeconds = runTimeInSeconds,
            TokensPerSecond = totalTokens / runTimeInSeconds
        });

        return responseBuilder.ToString();
    }

    public async Task<ChatMessageContent> SendMessageStreamingAsync(string currentMessage)
    {
        string response = await GenerateResponse(currentMessage);

        ChatHistory.AddAssistantMessage(response);

        return ChatHistory.Last();
    }

    private string buildPropmt(ChatHistory ChatHistory)
    {
        Token token = new Token();

        switch(ModelType)
        {
            case ModelType.DeepSeek:
                Token.SystemPropmtSupport = false;
                Token.System = string.Empty;
                Token.SystemEnd =string.Empty;
                Token.User = "<|user|>";
                Token.UserEnd = "<|end|>";
                Token.Assistant = "<|assistant|>";
                break;
            case ModelType.Phi35:
                Token.SystemPropmtSupport = true;
                Token.System  = "<|system|>";
                Token.SystemEnd = "<|end|>";
                Token.User = "<|user|>";
                Token.UserEnd = "<|end|>";
                Token.Assistant = "<|assistant|>";
                break;
            case ModelType.Phi4:
                Token.SystemPropmtSupport = true;
                Token.System = "<|system|>";
                Token.SystemEnd = "<|end|>";
                Token.User = "<|user|>";
                Token.UserEnd = "<|end|>";
                Token.Assistant = "<|assistant|>";
                break;

            default:
                Token.SystemPropmtSupport = true;
                Token.System = "<|system|>";
                Token.SystemEnd = "<|end|>";
                Token.User = "<|user|>";
                Token.UserEnd = "<|end|>";
                Token.Assistant = "<|assistant|>";
                break;
        }

        StringBuilder promptBuilder = new StringBuilder();

        //build the whole prompt with the whole chat
        foreach (var message in ChatHistory.ToList())
        {
            if (message.Role == AuthorRole.User)
            {
                promptBuilder.Append($"{Token.User}{message.Content}{Token.UserEnd}");
            }
            else if (message.Role == AuthorRole.Assistant)
            {
                promptBuilder.Append($"{Token.Assistant}{message.Content}{Token.Assistant}");
            }
            else if(Token.SystemPropmtSupport && message.Role == AuthorRole.System)
            {
                promptBuilder.Append($"{Token.System}{message.Content}{Token.SystemEnd}");
            }
        }

        return promptBuilder.ToString();
    }
}
