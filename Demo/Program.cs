using System.Diagnostics;
using Microsoft.Extensions.AI;
using BYOK.Core;
using BYOK.Core.Exceptions;
using BYOK.OpenAI;
using BYOK.Anthropic;

// ──────────────── API Keys ────────────────

var openaiApiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? "";
var groqApiKey = Environment.GetEnvironmentVariable("GROQ_API_KEY") ?? "";
var deepseekApiKey = Environment.GetEnvironmentVariable("DEEPSEEK_API_KEY") ?? "";
var anthropicApiKey = Environment.GetEnvironmentVariable("ANTHROPIC_API_KEY") ?? "";
var openrouterApiKey = Environment.GetEnvironmentVariable("OPENROUTER_API_KEY") ?? "";

// ──────────────── Provider Setup ────────────────

var byok = BYOKClient.Create(options =>
{
    /* ── OpenAI-compatible (pre-configured) ── */
    options.UseOpenAI(openaiApiKey);
    options.UseGroq(groqApiKey);
    options.UseDeepSeek(deepseekApiKey);
    options.UseOllama();
    options.UseOpenRouter(openrouterApiKey, referer: "https://mi-app.com", title: "Mi App");
    //options.UseOpenAICompatible(
    //    providerName: "myprovider",
    //    apiKey: openaiApiKey,
    //    configure: config =>
    //    {
    //        config.BaseUrl = "https://my-custom-endpoint.com/v1";
    //        config.AddHeader("X-Custom", "value");
    //});

    /* ── Anthropic-compatible (pre-configured) ── */
    options.UseAnthropic(anthropicApiKey);
    options.UseDeepSeekAnthropic(deepseekApiKey);
    options.UseOllamaAnthropic();
    // options.UseAnthropic("myanthropic", () => apiKey, config =>
    // {
    //     config.BaseUrl = "https://my-custom-anthropic-endpoint.com";
    // });

    /* ── Routing ── */
    //options.Routing.ForAlias("fast").Try("groq:groq/compound");
    // options.Routing.ForAlias("cheap").Try("groq:groq/compound");
    // options.Routing.ForAlias("migroq").Try("groq:llama-3-70b");
    //options.Routing.ForModelId("groq:groq/compound_INVALID_MODEL").FallbackTo("groq:groq/compound");      
    // options.Routing.ForModelId("groq:llama-3-70b_INVALID_MODEL").FallbackTo("anthropic:claude-sonnet-4-20250514");

    options.DefaultTimeout = TimeSpan.FromSeconds(40);
});


// ──────────────── Single response ────────────────

var messages = new List<ChatMessage>
{
    new(ChatRole.Assistant, "You are a helpfull asistant"),
    new(ChatRole.User, "Hello!")
};

var chatOptions = new ChatOptions
{
    // ModelId format: "providerName:modelId" or "alias"
    //      Providers: openai, groq, deepseek, ollama, openrouter, anthropic
    //      Aliases (configured above via ForAlias()): "fast", "cheap", "migroq"
    // Examples:
    //   "openai:gpt-5o-mini"
    //   "groq:groq/compound"
    //   "deepseek:deepseek-chat"
    //   "my-groq-personal:groq/compound"
    //   "my-groq-empresarial:groq/compound"
    //   "groq-with-function-tool:openai/gpt-oss-120b"
    //   "fast"
    //   "cheap"
    ModelId = "deepseek:deepseek-v4-flash",
};

Console.WriteLine("\n=== Single response ===\n");
var response = await byok.GetResponseAsync(messages, chatOptions);
Console.WriteLine($"Result: {response.Text}");



// ──────────────── Streaming ────────────────

//var messages = new List<ChatMessage>
//{
//    new(ChatRole.Assistant, "You are a helpfull asistant"),
//    new(ChatRole.User, "Write a short poem!")
//};
//var chatOptions = new ChatOptions
//{
//    ModelId = "groq:groq/compound"
//};
//Console.WriteLine("\n=== Streaming ===\n");

//await foreach(var update in byok.GetStreamingResponseAsync(messages, chatOptions))
//{
//    if(!string.IsNullOrEmpty(update.Text))
//    {
//        Console.Write(update.Text);
//        await Task.Delay(30);
//    }
//}

//Console.WriteLine("\n\nDone.");



// ──────────────── Funciton Calling ────────────────

//Console.WriteLine("\n=== Function Calling ===");

//var fcMessages = new List<ChatMessage>
//{
//    new(ChatRole.System, "You are a helpful assistant that can use tools."),
//    new(ChatRole.User, "What time is it?")
//};

//var fcOptions = new ChatOptions
//{
//    ModelId = "groq:openai/gpt-oss-120b",
//    Tools =
//    [
//        AIFunctionFactory.Create(MYFunctionCalling.GetCurrentTime)
//    ],
//};

//var fcResponse = await byok.GetResponseAsync(fcMessages, fcOptions);
//Console.WriteLine($"Result: {fcResponse.Text}");



// ──────────────── Error Handling ────────────────

//try
//{
//    Console.WriteLine("\n=== Error handling ===\n");
//    var result = await byok.GetResponseAsync(messages, chatOptions);
//    Console.WriteLine($"OK: {result.Text}");
//}
//catch (BYOKRoutingException ex)
//{
//    Console.WriteLine($"\nAll fallbacks failed: {ex.Message}");
//}
//catch (BYOKConfigurationException ex)
//{
//    Console.WriteLine($"\nProvider config error: {ex.Message}");
//}



// ──────────────── Loop ──────────────── 
// Select a model and it will respond with its name

//set your models here:

//string[] models =
//{
//    "openai:gpt-4o-mini",
//    "groq:groq/compound",
//    "groq:openai/gpt-oss-120b",
//    "deepseek-anthropic:deepseek-v4-flash",
//    "ollama:llama3",
//    "openrouter:nex-agi/nex-n2-pro:free",
//    "anthropic:claude-sonnet-4-20250514",
//}
//;

//Console.WriteLine("Available models:");
//for(int i = 0; i < models.Length; i++)
//{
//    Console.WriteLine($"[{i + 1}]  {models[i]}");
//}
//Console.WriteLine();

//while(true)
//{
//    Console.Write("Select Model (1-7): ");
//    string? input = Console.ReadLine();
//    if(string.IsNullOrWhiteSpace(input) || input == "exit")
//        break;

//    if(!int.TryParse(input, out int n) || n < 1 || n > models.Length) continue;

//    Console.Write($"[{models[n - 1]}]: ");
//    await foreach(var update in byok.GetStreamingResponseAsync(
//        [new ChatMessage(ChatRole.User, "Say your name. eres flash o pro?")],
//        new ChatOptions { ModelId = models[n - 1] }))
//    {
//        if(!string.IsNullOrEmpty(update.Text))
//            Console.Write(update.Text);
//    }

//    Console.WriteLine("\n");
//}