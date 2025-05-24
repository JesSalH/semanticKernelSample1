using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// Build configuration
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("MyAppSettings.json", optional: false, reloadOnChange: true)
    .Build();

// Set up dependency injection
var services = new ServiceCollection();
services.AddSingleton<IConfiguration>(configuration);
services.AddHttpClient<ApiAlphaPlugin>();
services.AddTransient<ApiAlphaPlugin>();

var serviceProvider = services.BuildServiceProvider();

var modelId = "gpt-4";
var apiKey = configuration["OpenAIKey"] ?? throw new InvalidOperationException("OpenAIKey not found in configuration");

var builder = Kernel.CreateBuilder().AddOpenAIChatCompletion(modelId, apiKey);

var kernel = builder.Build();
var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

// Get the ApiAlphaPlugin from DI container
var apiAlphaPlugin = serviceProvider.GetRequiredService<ApiAlphaPlugin>();

OpenAIPromptExecutionSettings settings = new()
{
    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(),
};
var history = new ChatHistory();

string? userInput;

do
{
    Console.Write("User: ");
    userInput = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(userInput)) continue;


    history.AddUserMessage(userInput);

    // this is what gets the resopnse from the assistant
    
    var response = await chatCompletionService.GetChatMessageContentAsync(
        history,
        executionSettings: settings,
        kernel: kernel
        );
    history.AddAssistantMessage(response.Content);

    Console.WriteLine($"Assistant: {response.Content}");
} while (!string.IsNullOrWhiteSpace(userInput));