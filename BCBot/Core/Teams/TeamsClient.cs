using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace BCBot.Core.Teams
{
    public class TeamsClient : ITeamsClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<TeamsClient> _logger;

        public TeamsClient(HttpClient httpClient, ILogger<TeamsClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task SendActivityCardAsync(TeamsActivityCardData teamsActivityCardData, string teamsWebhookUrl)
        {
            if (teamsWebhookUrl == null) return;

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var stringContent = new StringContent(teamsActivityCardData.Json);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var teamsWebHookUrl = new Uri(teamsWebhookUrl);

            _logger.LogInformation($"Sending [{teamsActivityCardData.Json}] to Teams.");

            var httpResponseMessage = await _httpClient.PostAsync(teamsWebHookUrl, stringContent);

            _logger.LogInformation(
                $"PostAsync result {httpResponseMessage.StatusCode}, {httpResponseMessage.ReasonPhrase}");

            httpResponseMessage.EnsureSuccessStatusCode();

        }

    }
}