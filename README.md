# BYOK RX — Bring Your Own Key
[![MIT License](https://img.shields.io/github/license/tutim-io/tutim)](https://github.com/tutim-io/tutim/blob/main/LICENSE) 

<p align="left">
  <!-- OpenAI -->
  <img src="https://img.shields.io/badge/OpenAI-1a1a1a?style=for-the-badge&logo=data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHZpZXdCb3g9IjAgMCAyNCAyNCI+PHBhdGggZmlsbD0id2hpdGUiIGQ9Ik0yMi4yODIgOS44MjFhNS45ODUgNS45ODUgMCAwIDAtLjUxNi00LjkxIDYuMDQ2IDYuMDQ2IDAgMCAwLTYuNTEtMi45QTYuMDY1IDYuMDY1IDAgMCAwIDQuOTgxIDQuMThhNS45ODUgNS45ODUgMCAwIDAtMy45OTggMi45IDYuMDQ2IDYuMDQ2IDAgMCAwIC43NDMgNy4wOTcgNS45OCA1Ljk4IDAgMCAwIC41MSA0LjkxMSA2LjA1MSA2LjA1MSAwIDAgMCA2LjUxNSAyLjlBNS45ODUgNS45ODUgMCAwIDAgMTMuMjYgMjRhNi4wNTYgNi4wNTYgMCAwIDAgNS43NzItNC4yMDYgNS45OSA1Ljk5IDAgMCAwIDMuOTk3LTIuOSA2LjA1NiA2LjA1NiAwIDAgMC0uNzQ3LTcuMDczek0xMy4yNiAyMi40M2E0LjQ3NiA0LjQ3NiAwIDAgMS0yLjg3Ni0xLjA0bC4xNDEtLjA4MSA0Ljc3OS0yLjc1OGEuNzk1Ljc5NSAwIDAgMCAuMzkyLS42ODF2LTYuNzM3bDIuMDIgMS4xNjhhLjA3MS4wNzEgMCAwIDEgLjAzOC4wNTJ2NS41ODNhNC41MDQgNC41MDQgMCAwIDEtNC40OTQgNC40OTR6TTMuNiAxOC4zMDRhNC40NyA0LjQ3IDAgMCAxLS41MzUtMy4wMTRsLjE0Mi4wODUgNC43ODMgMi43NTlhLjc3MS43NzEgMCAwIDAgLjc4IDBsNS44NDMtMy4zNjl2Mi4zMzJhLjA4LjA4IDAgMCAxLS4wMzMuMDYyTDkuNzQgMTkuOTVhNC41IDQuNSAwIDAgMS02LjE0LTEuNjQ2ek0yLjM0IDcuODk2YTQuNDg1IDQuNDg1IDAgMCAxIDIuMzY2LTEuOTczVjExLjZhLjc2Ni43NjYgMCAwIDAgLjM4OC42NzZsNS44MTUgMy4zNTUtMi4wMiAxLjE2OGEuMDc2LjA3NiAwIDAgMS0uMDcxIDBMNC4wMSAxNC41NDVBNC41MDQgNC41MDQgMCAwIDEgMi4zNCA3Ljg5NnptMTYuNTk3IDMuODU1LTUuODMzLTMuMzg3TDE1LjExOSA3LjJhLjA3Ni4wNzYgMCAwIDEgLjA3MSAwbDQuODA4IDIuNzczYTQuNSA0LjUgMCAwIDEtLjY3NiA4LjEyMnYtNS42NzZhLjc4Ni43ODYgMCAwIDAtLjM4NS0uNjY4em0yLjAxLTMuMDIzLS4xNDEtLjA4NS00Ljc3NC0yLjc4MmEuNzc2Ljc3NiAwIDAgMC0uNzg1IDBMOS40MDkgOS4yM1Y2Ljg5N2EuMDY2LjA2NiAwIDAgMSAuMDI4LS4wNjFsNC44MDctMi43NzJhNC41IDQuNSAwIDAgMSA2LjY4IDQuNjZ6bS0xMi42NCA0LjEzNS0yLjAyLTEuMTY0YS4wOC4wOCAwIDAgMS0uMDM4LS4wNTdWNi4wNzVhNC41IDQuNSAwIDAgMSA3LjM3NS0zLjQ1M2wtLjE0Mi4wOC00Ljc3OCAyLjc1OGEuNzk1Ljc5NSAwIDAgMC0uMzkzLjY4MXptMS4wOTctMi4zNjUgMi42MDItMS41IDIuNjA3IDEuNXYyLjk5OWwtMi41OTcgMS41LTIuNjA3LTEuNVoiLz48L3N2Zz4=" alt="OpenAI">
  <!-- Anthropic -->
<img src="https://img.shields.io/badge/Anthropic-D4A27F?style=for-the-badge&logo=anthropic&logoColor=white" alt="Anthropic">
  <!-- Groq -->
<img src="https://img.shields.io/badge/Groq-F43E01?style=for-the-badge&logo=data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iMTUycHgiIGhlaWdodD0iNTUuNXB4IiBlbmFibGUtYmFja2dyb3VuZD0ibmV3IDAgMzIuMjUgMTUyIDU1LjUiIHZlcnNpb249IjEuMSIgdmlld0JveD0iMCAzMi4yNSAxNTIgNTUuNSIgeG1sOnNwYWNlPSJwcmVzZXJ2ZSIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj4KIDxwYXRoIGZpbGw9IndoaXRlIiBkPSJtODQuODQ4IDM0LjEzN2MtOS43OTggMC0xNy43NjkgNy45NzEtMTcuNzY5IDE3Ljc3czcuOTcxIDE3Ljc2OSAxNy43NjkgMTcuNzY5IDE3Ljc3LTcuOTcxIDE3Ljc3LTE3Ljc2OS03Ljk3My0xNy43Ny0xNy43Ny0xNy43N3ptMCAyOC44NzZjLTYuMTI0IDAtMTEuMTA2LTQuOTgzLTExLjEwNi0xMS4xMDZzNC45ODItMTEuMTA2IDExLjEwNi0xMS4xMDYgMTEuMTA2IDQuOTgyIDExLjEwNiAxMS4xMDYtNC45ODEgMTEuMTA2LTExLjEwNiAxMS4xMDZ6Ii8+CiA8cGF0aCBmaWxsPSJ3aGl0ZSIgZD0ibTYwLjMxNSAzNC4yMDZjLTAuNjA3LTAuMDY4LTEuMjE3LTAuMTA0LTEuODI3LTAuMTA4LTAuMzA0IDAtMC41OTUgOWUtMyAtMC44OTMgMC4wMTRzLTAuNTk0IDAuMDMzLTAuODkxIDAuMDUxYy0xLjE5NyAwLjA5NC0yLjM4MiAwLjI5OS0zLjU0MSAwLjYxMS0yLjMyOSAwLjYyOS00LjU3NCAxLjcyMy02LjUxNSAzLjI3Ny0xLjk3IDEuNTctMy41NDggMy41NzUtNC42MTEgNS44NTktMC41MyAxLjEzOC0wLjkyMSAyLjMzNi0xLjE2NSAzLjU2Ny0wLjEyMSAwLjYwOC0wLjIxIDEuMjIyLTAuMjY2IDEuODQtMC4wMiAwLjMwNy0wLjA1NSAwLjYxNS0wLjA1OSAwLjkyMWwtMC4wMTEgMC40NTktNWUtMyAwLjIzdjAuMTlsMC4wMyAxMS45MDIgMC4wNDEgNS45NWg2LjY2NGwwLjA0Mi01Ljk1IDAuMDE1LTUuOTUyIDAuMDE1LTUuOTUxdi0wLjE4Mmw1ZS0zIC0wLjE0MiA4ZS0zIC0wLjI4NWMwLTAuMTkxIDAuMDI4LTAuMzc1IDAuMDM5LTAuNTY0IDAuMDM2LTAuMzcgMC4wOTEtMC43MzggMC4xNjUtMS4xMDIgMC4xNDYtMC43MTYgMC4zNzQtMS40MTMgMC42NzgtMi4wNzcgMC42MTMtMS4zMzIgMS41MjgtMi41MDIgMi42NzMtMy40MTkgMS4xNTYtMC45MzIgMi41NDEtMS42MjggNC4wMzgtMi4wNDIgMC43NTctMC4yMDcgMS41MzItMC4zNDQgMi4zMTQtMC40MDggMC4xOTgtMC4wMTEgMC4zOTUtMC4wMyAwLjU5NC0wLjAzN3MwLjQwMi0wLjAxMyAwLjU5NS0wLjAxMmMwLjM4MyAwIDAuNzYgMC4wMjUgMS4xNDIgMC4wNiAxLjUxOCAwLjE1MyAyLjk4OSAwLjYxOSA0LjMxOCAxLjM2OGwzLjMyNi01Ljc3NmMtMi4xMjUtMS4yMzUtNC40OC0yLjAxNC02LjkxOC0yLjI5MnoiLz4KIDxwYXRoIGZpbGw9IndoaXRlIiBkPSJtMTcuNzcgMzQuMDQ4Yy05Ljc5OSAwLTE3Ljc3IDcuOTcxLTE3Ljc3IDE3Ljc2OXM3Ljk3MSAxNy43NyAxNy43NyAxNy43N2g1Ljg0NHYtNi42NjRoLTUuODQ0Yy02LjEyNCAwLTExLjEwNi00Ljk4Mi0xMS4xMDYtMTEuMTA2czQuOTgyLTExLjEwNiAxMS4xMDYtMTEuMTA2IDExLjEzMiA0Ljk4MiAxMS4xMzIgMTEuMTA2djE2LjM2NWMwIDYuMDg0LTQuOTU0IDExLjAzOS0xMS4wMjMgMTEuMTAzLTIuOTA0LTAuMDI0LTUuNjgxLTEuMTkxLTcuNzI5LTMuMjVsLTQuNzEyIDQuNzEyYzMuMjY2IDMuMjgzIDcuNjkxIDUuMTUxIDEyLjMyMSA1LjIwMXYzZS0zaDAuMTE5IDAuMTI1di0zZS0zYzkuNjU5LTAuMTMxIDE3LjQ4LTguMDA1IDE3LjUyNS0xNy42ODZsNmUtMyAtMTYuODgxYy0wLjIzMi05LjU5Ni04LjExMi0xNy4zMzMtMTcuNzY0LTE3LjMzM3oiLz4KIDxwYXRoIGZpbGw9IndoaXRlIiBkPSJtMTI0LjA4IDM0LjEzN2MtOS43OTggMC0xNy43NjkgNy45NzEtMTcuNzY5IDE3Ljc3czcuOTcxIDE3Ljc2OSAxNy43NjkgMTcuNzY5aDYuMDh2LTYuNjYzaC02LjA4Yy02LjEyNCAwLTExLjEwNi00Ljk4My0xMS4xMDYtMTEuMTA2czQuOTgyLTExLjEwNiAxMS4xMDYtMTEuMTA2YzUuNzk5IDAgMTAuNTcyIDQuNDY4IDExLjA2MiAxMC4xNDNoLTAuMDF2MzQuMTJoNi42NjR2LTMzLjE1N2MtMmUtMyAtOS43OTktNy45MTgtMTcuNzctMTcuNzE2LTE3Ljc3eiIvPgogPHBvbHlnb24gZmlsbD0id2hpdGUiIHBvaW50cz0iMTUxLjk4IDM1LjA0IDE1MS4wMyAzNS4wNCAxNDkuNzQgMzcuMDUzIDE0OC40IDM1LjA0IDE0Ny40NCAzNS4wNCAxNDcuNDQgMzguNjI0IDE0OC41MSAzOC42MjQgMTQ4LjUxIDM2Ljg4IDE0OS40NiAzOC4yODggMTQ5Ljk4IDM4LjI4OCAxNTAuOTEgMzYuODM2IDE1MC45MyAzOC42MjQgMTUyIDM4LjYyNCIvPgogPHBvbHlnb24gZmlsbD0id2hpdGUiIHBvaW50cz0iMTQzLjUyIDM1Ljg5NiAxNDQuNjggMzUuODk2IDE0NC42OCAzOC42MjQgMTQ1Ljg2IDM4LjYyNCAxNDUuODYgMzUuODk2IDE0Ny4wMyAzNS44OTYgMTQ3LjAzIDM1LjA0IDE0My41MiAzNS4wNCIvPgo8L3N2Zz4K&logoColor=white" alt="Groq">
  <img src="https://img.shields.io/badge/DeepSeek-4A6CF7?style=for-the-badge&logo=deepseek&logoColor=white" alt="DeepSeek">
  <img src="https://img.shields.io/badge/Ollama-222222?style=for-the-badge&logo=ollama&logoColor=white" alt="Ollama">
  <img src="https://img.shields.io/badge/OpenRouter-FF6B35?style=for-the-badge&logo=openrouter&logoColor=white" alt="OpenRouter">
</p>



<p align="left"><strong style="color:#58a6ff; font-size:1.3em">One interface client, multiple providers. 😎</strong></p>

Route AI requests across multiple LLM providers with automatic fallback, all through a unified `IChatClient` interface.

## Highlights

- **Unified interface** — single `IChatClient` that routes to OpenAI, Anthropic, Groq, DeepSeek, Ollama, OpenRouter, and any OpenAI-compatible provider
- **Automatic fallback** — define fallback chains per model; if a provider fails, BYOK tries the next one
- **Model aliases** — friendly names like `"fast"` or `"cheap"` that resolve to real model paths
- **Per-provider key rotation** — each provider gets its own API key provider; keys can rotate at runtime without restart
- **Function calling** — built-in support through `Microsoft.Extensions.AI.FunctionInvokingChatClient`
- **OpenTelemetry ready** — providers expose `ChatClientMetadata` for observability instrumentation
- **Dependency injection** — `AddBYOKClient()` extension for `IServiceCollection`

## Install

```bash
dotnet add package BYOK.Core
dotnet add package BYOK.OpenAI
dotnet add package BYOK.Anthropic
```

> Packages coming soon to NuGet. For now, clone and build locally.

## Quick Start

```csharp
using Microsoft.Extensions.AI;
using BYOK.Core;
using BYOK.OpenAI;
using BYOK.Anthropic;

var openAiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? "";
var anthropicKey = Environment.GetEnvironmentVariable("ANTHROPIC_API_KEY") ?? "";
var groqKey = Environment.GetEnvironmentVariable("GROQ_API_KEY") ?? "";

// ──────────────── Providers Setup ────────────────
var byok = BYOKClient.Create(options =>
{
    // built-in providers
    options.UseOpenAI(openAiKey);
    options.UseAnthropic(anthropicKey);
    options.UseGroq(groqKey);
    // ...

    // optional routing & alias
    options.Routing.ForModelId("openai:gpt-4o-mini")
        .FallbackTo("groq:groq/compound");

    options.Routing.ForAlias("mygroq")
        .Try("groq:groq/compound");

    // optional global timeout
    options.DefaultTimeout = TimeSpan.FromSeconds(100);
});

// ──────────────── Send a request ────────────────
var messages = new List<ChatMessage>
{
    new(ChatRole.System, "You are a helpful assistant."),
    new(ChatRole.User, "Hello!")
};

var chatOptions = new ChatOptions
{
    /* --- Change provider/model per request — no rebuild needed --- */
    ModelId = "openai:gpt-4o-mini" // "{providerName}:{modelId}" or "{alias}"
};

var response = await byok.GetResponseAsync(messages, chatOptions);

Console.WriteLine($"Result: {response.Text}");
```


### Dependency injection

```csharp
services.AddBYOKClient(options =>
{
    options.UseOpenAI(openAiKey);
    options.UseAnthropic(anthropicKey);

    options.Routing.ForModelId("openai:gpt-4o-mini")
        .FallbackTo("groq:groq/compound");
});
```

Then inject `IChatClient` anywhere — always pass `ModelId` with the route:

```csharp
public class MyService(IChatClient chatClient)
{
    public async Task RunAsync()
    {
        var response = await chatClient.GetResponseAsync(messages, new ChatOptions
        {
            ModelId = "openai:gpt-4o-mini" // ← providerName:modelId
        });
    }
}
```

## Usage

### Route format 

Set `ModelId` to `"{providerName}:{modelId}"` and BYOK routes to the right provider:

```csharp
ModelId = "openai:gpt-4o-mini"
```

| Route | Provider | Model |
|-------|----------|-------|
| `openai:gpt-4o-mini` | openai | gpt-4o-mini |
| `groq:groq/compound` | groq | groq/compound |
| `deepseek:deepseek-flash` | deepseek | deepseek-flash |
| `ollama:llama3` | ollama | llama3 |
| `anthropic:claude-sonnet-4-20250514` | anthropic | claude-sonnet-4-20250514 |

The provider name must match one registered via `UseOpenAICompatible()`, `UseAnthropicCompatible()`, or one of the `built-in` extensions.

### Built-in providers

| Extension | Provider name (default) | Notes |
|-----------|------------------------|-------|
| `UseOpenAI()` | `openai` | `api.openai.com/v1` |
| `UseOpenAICompatible()` | custom | Any OpenAI-compatible endpoint |
| `UseGroq()` | `groq` | `api.groq.com/openai/v1` |
| `UseOpenRouter()` | `openrouter` | `openrouter.ai/api/v1` |
| `UseOllama()` | `ollama` | `localhost:11434/v1` (no API key needed) |
| `UseDeepSeek()` | `deepseek` | `api.deepseek.com/v1` |
| `UseAnthropic()` | `anthropic` | `api.anthropic.com` |
| `UseAnthropicCompatible()` | custom | Any Anthropic-compatible endpoint |
| `UseDeepSeekAnthropic()` | `deepseek-anthropic` | `api.deepseek.com/anthropic` |
| `UseOllamaAnthropic()` | `ollama-anthropic` | `localhost:11434` (Anthropic-compatible) |



### Fallback

```csharp
options.Routing.ForModelId("openai:gpt-4o-mini")
    .FallbackTo("groq:groq/compound")
    .FallbackTo("anthropic:claude-sonnet-4-20250514");
```

If the primary fails, BYOK tries fallbacks in order.

### Aliases
`ForAlias()` sets an alias you can use as `ModelId`:

```csharp
options.Routing.ForAlias("fast").Try("groq:groq/compound");
options.Routing.ForAlias("cheap").Try("groq:llama-3-70b");
```

Use anywhere a route is expected: `ModelId = "fast"`.

### Custom providers

```csharp
options.UseOpenAICompatible(
    providerName: "myprovider"
    apiKey: openAiKey,
    configure: config =>
    {
        config.BaseUrl = "https://my-custom-endpoint.com/v1";
        config.AddHeader("X-Custom", "value");
        config.NativeOptions.ClientLoggingOptions.LogLevel = OpenAI.Diagnostics.ClientLogLevel.Informational;
    },);

options.UseAnthropicCompatible(
    providerName: "myanthropic"
    apiKey: anthropicKey,
    configure: config =>
    {
        config.BaseUrl = "https://my-custom-anthropic.com";
    },);
```

Then use: `ModelId = "myprovider:some-model"` or `ModelId = "myanthropic:some-model"`.

Or register any custom `IChatClient` with a factory:

```csharp
options.UseProvider("my-custom", () => new MyChatClient(apiKey));
```

### Streaming

```csharp
await foreach (var update in byok.GetStreamingResponseAsync(messages, chatOptions))
{
    Console.Write(update.Text);
}
```

### Function calling

```csharp
var fcMessages = new List<ChatMessage>
{
    new(ChatRole.System, "You are a helpful assistant that can use tools."),
    new(ChatRole.User, "What time is it?")
};

var fcOptions = new ChatOptions
{
    ModelId = "groq:openai/gpt-oss-120b",
    Tools = [AIFunctionFactory.Create(GetCurrentTime)]
};

var response = await byok.GetResponseAsync(fcMessages, fcOptions);
Console.WriteLine($"Result: {response.Text}"); 

// [AI]: The current time is 14:30:25...
```

Define your function tool:

```csharp
[Description("Returns the current time")]
static string GetCurrentTime() => DateTime.Now.ToString("HH:mm:ss");
```

## Architecture

```
┌──────────────────────────────────────────┐
│              BYOKClient                   │
│  Routing → Provider Factory → IChatClient │
└──────────────────────────────────────────┘
         │                     │
         ▼                     ▼
  BYOK.OpenAI           BYOK.Anthropic
  OpenAICompatibleClient AnthropicChatClient
  (IChatClient)          (IChatClient)
```

Each provider client caches the underlying SDK client and only rebuilds when the API key changes.


 
## License

[MIT] © BracoZS