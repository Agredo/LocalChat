namespace LocalChat.AI.Models
{
    public record GenerationMetrics
    {
        public int TotalTokens { get; internal set; }
        public double RunTimeInSeconds { get; internal set; }
        public double TokensPerSecond { get; internal set; }

        public override string ToString()
        {
            return $"Streaming Tokens: {TotalTokens} Time: {RunTimeInSeconds:0.00} Tokens per second: {TotalTokens / RunTimeInSeconds:0.00}";
        }
    }
}