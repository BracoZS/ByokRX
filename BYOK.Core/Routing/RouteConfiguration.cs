namespace BYOK.Core.Routing;

internal class RouteConfiguration
{
    // Key: "openai:gpt-4o" -> Value: ["anthropic/claude-3-opus", "groq/llama-3-70b"]
    public Dictionary<string, List<string>> Fallbacks { get; } = new(StringComparer.OrdinalIgnoreCase);

    // Key: "cheap-fast-model" -> Value: "groq/llama-3-8b"
    public Dictionary<string, string> Aliases { get; } = new(StringComparer.OrdinalIgnoreCase);
}