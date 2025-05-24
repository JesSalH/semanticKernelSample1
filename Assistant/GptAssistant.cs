using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace semanticKernelSample1.Assistant
{
    public class GptAssistant
    {
        private readonly IChatCompletionService _chatCompletionService;
        private readonly Kernel _kernel;
        private readonly OpenAIPromptExecutionSettings _settings;
        private readonly ILogger<GptAssistant> _logger;

        public GptAssistant(Kernel kernel, IChatCompletionService chatCompletionService, ILogger<GptAssistant> logger)
        {
            _kernel = kernel;
            _chatCompletionService = chatCompletionService;
            _logger = logger;
            _settings = new OpenAIPromptExecutionSettings
            {
                FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(),
            };
        }        public async Task RunAsync()
        {
            var history = new ChatHistory();
            string? userInput;
            try
            {
                do
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("User: ");
                    userInput = Console.ReadLine();
                    Console.ResetColor();
                    
                    // Exit conditions
                    if (string.IsNullOrWhiteSpace(userInput) || 
                        userInput.Equals("exit", StringComparison.OrdinalIgnoreCase) ||
                        userInput.Equals("quit", StringComparison.OrdinalIgnoreCase) ||
                        userInput.Equals("close", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Goodbyeee!");
                        break;
                    }

                    history.AddUserMessage(userInput);
                    
                    var response = await _chatCompletionService.GetChatMessageContentAsync(
                        history,
                        executionSettings: _settings,
                        kernel: _kernel
                    );
                    
                    var responseContent = response.Content ?? string.Empty;
                    history.AddAssistantMessage(responseContent);
                    
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Assistant: {responseContent}");
                    Console.ResetColor();
                    
                } while (true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the GPT assistant");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred in the GPT assistant: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
