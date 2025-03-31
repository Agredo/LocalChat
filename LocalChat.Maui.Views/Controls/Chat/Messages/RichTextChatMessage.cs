using System.Collections.ObjectModel;
using Maui.BindableProperty.Generator.Core;

namespace LocalChat.Maui.Views.Chat.Messages;

public partial class RichTextChatMessage : ChatMessageBase
{
    [AutoBindable]
    private ObservableCollection<ContentBlock> contentBlocks = [];

    public RichTextChatMessage()
    {
    }

    public RichTextChatMessage(string content, bool isUserMessage = false)
    {
        IsUserMessage = isUserMessage;
        ProcessContent(content);
    }

    private void ProcessContent(string content)
    {
        // Hier wird der Text auf Code-Blöcke analysiert und in ContentBlocks aufgeteilt
        var currentText = new System.Text.StringBuilder();

        int index = 0;
        while (index < content.Length)
        {
            int codeStart = content.IndexOf("```", index);

            if (codeStart == -1)
            {
                // Kein weiterer Code-Block gefunden
                currentText.Append(content.Substring(index));
                break;
            }

            // Text vor dem Code-Block hinzufügen
            if (codeStart > index)
            {
                currentText.Append(content.Substring(index, codeStart - index));
            }

            if (currentText.Length > 0)
            {
                ContentBlocks.Add(new TextBlock { Content = currentText.ToString().Trim() });
                currentText.Clear();
            }

            // Code-Block verarbeiten
            int languageEnd = content.IndexOf('\n', codeStart);
            int codeEnd = content.IndexOf("```", codeStart + 3);

            if (languageEnd != -1 && codeEnd != -1 && languageEnd < codeEnd)
            {
                string language = content.Substring(codeStart + 3, languageEnd - codeStart - 3).Trim();
                string code = content.Substring(languageEnd + 1, codeEnd - languageEnd - 1);

                ContentBlocks.Add(new CodeBlock
                {
                    Content = code,
                    Language = language
                });

                index = codeEnd + 3;
            }
            else
            {
                // Kein gültiger Code-Block gefunden
                currentText.Append(content.Substring(index, codeStart + 3 - index));
                index = codeStart + 3;
            }
        }

        // Verbleibender Text hinzufügen
        if (currentText.Length > 0)
        {
            ContentBlocks.Add(new TextBlock { Content = currentText.ToString().Trim() });
        }
    }
}

public abstract class ContentBlock
{
    public string Content { get; set; }
}

public class TextBlock : ContentBlock { }

public class CodeBlock : ContentBlock
{
    public string Language { get; set; } = "csharp";
}
