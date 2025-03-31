using System.Collections.ObjectModel;
using System.Text.Json;

namespace LocalChat.Maui.Views.Chat.History;

/// <summary>
/// Verwaltet die Sammlung von Chat-Nachrichten und bietet Funktionen für Speicherung und Wiederherstellung
/// </summary>
public class ChatHistory : ObservableCollection<ChatMessageBase>
{
    /// <summary>
    /// Erstellt eine neue Chat-Historie
    /// </summary>
    public ChatHistory()
    {
    }

    /// <summary>
    /// Fügt eine neue Textnachricht vom Benutzer hinzu
    /// </summary>
    /// <param name="text">Der Nachrichtentext</param>
    public void AddUserTextMessage(string text)
    {
        Add(new TextChatMessage(text, true));
    }

    /// <summary>
    /// Fügt eine neue Textnachricht vom Assistenten hinzu
    /// </summary>
    /// <param name="text">Der Nachrichtentext</param>
    public void AddAssistantTextMessage(string text)
    {
        Add(new TextChatMessage(text, false));
    }

    /// <summary>
    /// Fügt eine neue Codenachricht vom Assistenten hinzu
    /// </summary>
    /// <param name="code">Der Code-Inhalt</param>
    /// <param name="language">Die Programmiersprache</param>
    /// <param name="description">Optionale Beschreibung</param>
    public void AddAssistantCodeMessage(string code, string language = "csharp", string description = "")
    {
        Add(new CodeChatMessage(code, language, description, false));
    }

    /// <summary>
    /// Fügt eine neue Bildnachricht vom Assistenten hinzu
    /// </summary>
    /// <param name="imageSource">Die Bildquelle</param>
    /// <param name="caption">Optionale Beschreibung</param>
    public void AddAssistantImageMessage(ImageSource imageSource, string caption = "")
    {
        Add(new ImageChatMessage(imageSource, caption, false));
    }

    /// <summary>
    /// Erstellt eine formatierte Textdarstellung des Chatverlaufs für die Übergabe an das LLM
    /// </summary>
    /// <param name="maxMessages">Maximale Anzahl der zurückzugebenden Nachrichten</param>
    /// <returns>Chatverlauf als formatierter Text</returns>
    public string FormatChatHistoryForLLM(int maxMessages = 10)
    {
        var formattedHistory = new System.Text.StringBuilder();

        // Wir betrachten nur TextChatMessage-Objekte für die LLM-Eingabe
        foreach (var message in this.TakeLast(maxMessages))
        {
            if (message is TextChatMessage textMessage)
            {
                string role = textMessage.IsUserMessage ? "User" : "Assistant";
                formattedHistory.AppendLine($"{role}: {textMessage.MessageText}");
            }
            else if (message is CodeChatMessage codeMessage)
            {
                formattedHistory.AppendLine($"Assistant: ```{codeMessage.Language}");
                formattedHistory.AppendLine(codeMessage.CodeContent);
                formattedHistory.AppendLine("```");

                if (!string.IsNullOrEmpty(codeMessage.Description))
                {
                    formattedHistory.AppendLine(codeMessage.Description);
                }
            }
        }

        return formattedHistory.ToString();
    }

    /// <summary>
    /// Speichert den Chatverlauf in einer JSON-Datei
    /// </summary>
    /// <param name="filename">Der Dateiname</param>
    /// <returns>Ein Task, der die asynchrone Operation darstellt</returns>
    public async Task SaveToFileAsync(string filename)
    {
        // Hier müsste eine Serialisierung implementiert werden
        // Für eine vollständige Implementierung wäre ein eigenes DTO-Modell notwendig,
        // da die Chatnachrichten UI-Elemente enthalten, die nicht direkt serialisiert werden können

        // Beispielhafte Implementation:
        string path = Path.Combine(FileSystem.AppDataDirectory, filename);

        // Serialisierbare Darstellung erstellen
        var messages = new List<object>();
        foreach (var message in this)
        {
            if (message is TextChatMessage textMessage)
            {
                messages.Add(new
                {
                    Type = "Text",
                    IsUserMessage = textMessage.IsUserMessage,
                    Timestamp = textMessage.Timestamp,
                    Text = textMessage.MessageText
                });
            }
            else if (message is CodeChatMessage codeMessage)
            {
                messages.Add(new
                {
                    Type = "Code",
                    IsUserMessage = codeMessage.IsUserMessage,
                    Timestamp = codeMessage.Timestamp,
                    Code = codeMessage.CodeContent,
                    Language = codeMessage.Language,
                    Description = codeMessage.Description
                });
            }
            // ImageChatMessage würde zusätzliche Verarbeitung benötigen, um den ImageSource zu serialisieren
        }

        string json = JsonSerializer.Serialize(messages, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(path, json);
    }

    /// <summary>
    /// Lädt einen Chatverlauf aus einer JSON-Datei
    /// </summary>
    /// <param name="filename">Der Dateiname</param>
    /// <returns>Ein Task, der die asynchrone Operation mit der geladenen ChatHistory darstellt</returns>
    public static async Task<ChatHistory> LoadFromFileAsync(string filename)
    {
        // Beispielhafte Implementation
        string path = Path.Combine(FileSystem.AppDataDirectory, filename);

        if (!File.Exists(path))
            return new ChatHistory();

        string json = await File.ReadAllTextAsync(path);
        var chatHistory = new ChatHistory();

        try
        {
            var messages = JsonSerializer.Deserialize<List<JsonElement>>(json);

            foreach (var msg in messages)
            {
                string type = msg.GetProperty("Type").GetString();
                bool isUserMessage = msg.GetProperty("IsUserMessage").GetBoolean();
                DateTime timestamp = msg.GetProperty("Timestamp").GetDateTime();

                switch (type)
                {
                    case "Text":
                        string text = msg.GetProperty("Text").GetString();
                        var textMessage = new TextChatMessage(text, isUserMessage)
                        {
                            Timestamp = timestamp
                        };
                        chatHistory.Add(textMessage);
                        break;

                    case "Code":
                        string code = msg.GetProperty("Code").GetString();
                        string language = msg.GetProperty("Language").GetString();
                        string description = msg.TryGetProperty("Description", out var descProp)
                            ? descProp.GetString() : string.Empty;

                        var codeMessage = new CodeChatMessage(code, language, description, isUserMessage)
                        {
                            Timestamp = timestamp
                        };
                        chatHistory.Add(codeMessage);
                        break;

                        // ImageChatMessage würde zusätzliche Verarbeitung benötigen
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Laden des Chatverlaufs: {ex.Message}");
        }

        return chatHistory;
    }
}
