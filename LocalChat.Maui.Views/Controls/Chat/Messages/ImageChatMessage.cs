using System;
using Maui.BindableProperty.Generator.Core;

namespace LocalChat.Maui.Views.Chat;

public partial class ImageChatMessage : ChatMessageBase
{
    /// <summary>
    /// Die Bildquelle
    /// </summary>
    [AutoBindable]
    private ImageSource imageSource;

    /// <summary>
    /// Optionale Beschreibung zum Bild
    /// </summary>
    [AutoBindable(DefaultValue = "")]
    private string caption;

    /// <summary>
    /// Erstellt eine neue Instanz der ImageChatMessage-Klasse
    /// </summary>
    public ImageChatMessage()
    {
    }

    /// <summary>
    /// Erstellt eine neue Instanz der ImageChatMessage-Klasse mit der angegebenen Bildquelle
    /// </summary>
    /// <param name="imageSource">Die Bildquelle</param>
    /// <param name="caption">Optionale Beschreibung</param>
    /// <param name="isUserMessage">Gibt an, ob die Nachricht vom Benutzer stammt</param>
    public ImageChatMessage(ImageSource imageSource, string caption = "", bool isUserMessage = false)
    {
        ImageSource = imageSource;
        Caption = caption;
        IsUserMessage = isUserMessage;
    }
}