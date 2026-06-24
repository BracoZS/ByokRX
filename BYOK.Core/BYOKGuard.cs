using BYOK.Core.Exceptions;

namespace BYOK.Core;

public static class BYOKGuard
{
    public static string IfModelIdMissing(string? modelId)
    {
        if (string.IsNullOrWhiteSpace(modelId))
        {
            throw new BYOKArgumentException(
                "ModelId is required. Pass it in ChatOptions.ModelId",
                nameof(modelId));
        }
        return modelId;
    }

    public static string IfApiKeyMissing(string? apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new BYOKConfigurationException("The API Key is not configured.");
        }
        return apiKey;
    }
}
