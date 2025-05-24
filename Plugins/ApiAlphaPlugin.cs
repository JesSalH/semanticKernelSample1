using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using System.ComponentModel;
using System.Net.Http.Json;
using semanticKernelSample1.Plugins.DTOs;
using semanticKernelSample1.Plugins.DTOs.BaseCharacter;
using semanticKernelSample1.Plugins.DTOs.ExtendedCharacter;

namespace semanticKernelSample1.Plugins
{
    public class ApiAlphaPlugin
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly ILogger<ApiAlphaPlugin> _logger;
        
        public ApiAlphaPlugin(
            IConfiguration configuration, 
            HttpClient httpClient,
            ILogger<ApiAlphaPlugin> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _baseUrl = configuration["ApiAlpha:BaseUrl"] ?? "http://localhost:5163";
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }               

        [KernelFunction("GetBaseCharacterNames")]
        [Description("Gets a list of of the character names and the number of characters found")]
        [return: Description("a number of characters found and a list of character names")]
        public async Task<CharacterNamesResponse?> GetBaseCharacterNames(int id, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Requesting base character names for id: {Id}", id);
            var response = await GetAsync<CharacterNamesResponse>($"test/{id}", cancellationToken);
            return response;
        }

        [KernelFunction("GetBaseCharacter")]
        [Description("Gets a base character by its ID")]
        [return: Description("the character details including name, attributes, and other properties")]
        public async Task<BaseCharacter?> GetBaseCharacter(int id, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Requesting base character for id: {Id}", id);
            var response = await GetAsync<BaseCharacter>($"characters/{id}", cancellationToken);
            return response;
        }

        [KernelFunction("GetExtendedCharacter")]
        [Description("Gets an extended character with detailed information by its ID")]
        [return: Description("the extended character details including additional properties, stats, and comprehensive information")]
        public async Task<ExtendedCharacter?> GetExtendedCharacter(int id, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Requesting extended character for id: {Id}", id);
            var response = await GetAsync<ExtendedCharacter>($"characters/{id}/extended", cancellationToken);
            return response;
        }

        private async Task<T?> GetAsync<T>(string endpoint, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("GET request to {BaseUrl}/{Endpoint}", _baseUrl, endpoint);
            var response = await _httpClient.GetAsync(endpoint, cancellationToken);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching data from {BaseUrl}/{Endpoint}", _baseUrl, endpoint);
                return default;
            }
            var model = await response.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken);
            _logger.LogInformation("Response received from {BaseUrl}/{Endpoint}", _baseUrl, endpoint);
            return model;
        }
    }
}