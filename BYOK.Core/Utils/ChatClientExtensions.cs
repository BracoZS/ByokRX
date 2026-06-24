using Microsoft.Extensions.AI;

namespace BYOK.Core.Utils;

public static class ChatClientExtensions
{
    public static IChatClient WithMetadata(this IChatClient inner, string providerName, string defaultModelId, Uri? providerUri)
        => new MetadataChatClient(inner, providerName, defaultModelId, providerUri);
}

/// <summary>
/// Chat client wrapper that adds metadata information.
/// </summary>
sealed class MetadataChatClient : DelegatingChatClient
{
    public ChatClientMetadata Metadata { get; }

    public MetadataChatClient(IChatClient inner, string providerName, string defaultModelId, Uri? providerUri)
        : base(inner)
    {
        Metadata = new ChatClientMetadata(providerName, providerUri, defaultModelId);
    }
}