namespace LocalChat.AI.Models
{
    public record Token
    {
        public static string System { get; internal set; }
        public static string SystemEnd { get; internal set; }
        public static string User { get; internal set; }
        public static string UserEnd { get; internal set; }
        public static string Assistant { get; internal set; }
        public static bool SystemPropmtSupport { get; internal set; } = true;
    }
}