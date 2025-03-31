namespace LocalChat.Maui.Views.Chat.Messages;

public class ContentBlockTemplateSelector : DataTemplateSelector
{
    public DataTemplate TextBlockTemplate { get; set; }
    public DataTemplate CodeBlockTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item is TextBlock)
            return TextBlockTemplate;

        if (item is CodeBlock)
            return CodeBlockTemplate;

        return TextBlockTemplate;
    }
}
