using Maui.BindableProperty.Generator.Core;

namespace LocalChat.Maui.Views.Chat
{
    public partial class CodeChatMessage : ChatMessageBase
    {
        /// <summary>
        /// Der Code-Inhalt
        /// </summary>
        [AutoBindable(DefaultValue = "")]
        private string codeContent;

        /// <summary>
        /// Die Programmiersprache des Codes
        /// </summary>
        [AutoBindable(DefaultValue = "\"csharp\"")]
        private string language;

        /// <summary>
        /// Optionale Beschreibung zum Code
        /// </summary>
        [AutoBindable(DefaultValue = "")]
        private string description;

        /// <summary>
        /// Erstellt eine neue Instanz der CodeChatMessage-Klasse
        /// </summary>
        public CodeChatMessage()
        {
        }

        /// <summary>
        /// Erstellt eine neue Instanz der CodeChatMessage-Klasse mit dem angegebenen Code
        /// </summary>
        /// <param name="code">Der Code-Inhalt</param>
        /// <param name="language">Die Programmiersprache</param>
        /// <param name="description">Optionale Beschreibung</param>
        /// <param name="isUserMessage">Gibt an, ob die Nachricht vom Benutzer stammt</param>
        public CodeChatMessage(string code, string language = "csharp", string description = "", bool isUserMessage = false)
        {
            CodeContent = code;
            Language = language;
            Description = description;
            IsUserMessage = isUserMessage;
        }
    }
}
