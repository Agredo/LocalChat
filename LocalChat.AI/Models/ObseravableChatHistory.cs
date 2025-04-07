using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using System.Collections.ObjectModel;
using System.Text;

namespace LocalChat.AI.Models;

/// <summary>
/// Provides a history of chat messages from a chat conversation.
/// </summary>
public class ObservableChatHistory : ObservableCollection<ChatMessageContent>
{
    /// <summary>Initializes an empty history.</summary>
    public ObservableChatHistory()
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="ChatHistory"/> with a first message in the provided <see cref="AuthorRole"/>.
    /// If not role is provided then the first message will default to <see cref="AuthorRole.System"/> role.
    /// </summary>
    /// <param name="message">The text message to add to the first message in chat history.</param>
    /// <param name="role">The role to add as the first message.</param>
    public ObservableChatHistory(string message, AuthorRole role)
    {
        NotNullOrWhiteSpace(message);
        this.Add(new ChatMessageContent(role, message));
    }

    private void NotNullOrWhiteSpace(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            throw new ArgumentNullException(nameof(message));
        }
    }

    /// <summary>
    /// Creates a new instance of the <see cref="ChatHistory"/> class with a system message.
    /// </summary>
    /// <param name="systemMessage">The system message to add to the history.</param>
    public ObservableChatHistory(string systemMessage)
        : this(systemMessage, AuthorRole.System)
    {
    }

    /// <summary>Initializes the history will all of the specified messages.</summary>
    /// <param name="messages">The messages to copy into the history.</param>
    /// <exception cref="ArgumentNullException"><paramref name="messages"/> is null.</exception>
    public ObservableChatHistory(IEnumerable<ChatMessageContent> messages)
    {
        NotNull(messages);
        foreach (var message in messages)
        {
            this.Add(message);
        }
    }

    private void NotNull<T>(T messages)
    {
        if (messages == null)
        {
            throw new ArgumentNullException(nameof(messages));
        }
    }

    /// <summary>
    /// <param name="authorRole">Role of the message author</param>
    /// <param name="content">Message content</param>
    /// <param name="encoding">Encoding of the message content</param>
    /// <param name="metadata">Dictionary for any additional metadata</param>
    /// </summary>
    public void AddMessage(AuthorRole authorRole, string content, Encoding? encoding = null, IReadOnlyDictionary<string, object?>? metadata = null) =>
        this.Add(new ChatMessageContent(authorRole, content, null, null, encoding, metadata));

    /// <summary>
    /// <param name="authorRole">Role of the message author</param>
    /// <param name="contentItems">Instance of <see cref="ChatMessageContentItemCollection"/> with content items</param>
    /// <param name="encoding">Encoding of the message content</param>
    /// <param name="metadata">Dictionary for any additional metadata</param>
    /// </summary>
    public void AddMessage(AuthorRole authorRole, ChatMessageContentItemCollection contentItems, Encoding? encoding = null, IReadOnlyDictionary<string, object?>? metadata = null) =>
        this.Add(new ChatMessageContent(authorRole, contentItems, null, null, encoding, metadata));

    /// <summary>
    /// Add a user message to the chat history
    /// </summary>
    /// <param name="content">Message content</param>
    public void AddUserMessage(string content) =>
        this.AddMessage(AuthorRole.User, content);

    /// <summary>
    /// Add a user message to the chat history
    /// </summary>
    /// <param name="contentItems">Instance of <see cref="ChatMessageContentItemCollection"/> with content items</param>
    public void AddUserMessage(ChatMessageContentItemCollection contentItems) =>
        this.AddMessage(AuthorRole.User, contentItems);

    /// <summary>
    /// Add an assistant message to the chat history
    /// </summary>
    /// <param name="content">Message content</param>
    public void AddAssistantMessage(string content) =>
        this.AddMessage(AuthorRole.Assistant, content);

    /// <summary>
    /// Add a system message to the chat history
    /// </summary>
    /// <param name="content">Message content</param>
    public void AddSystemMessage(string content) =>
        this.AddMessage(AuthorRole.System, content);

    /// <summary>
    /// Add a developer message to the chat history
    /// </summary>
    /// <param name="content">Message content</param>
    public void AddDeveloperMessage(string content) =>
        this.AddMessage(AuthorRole.Developer, content);
}