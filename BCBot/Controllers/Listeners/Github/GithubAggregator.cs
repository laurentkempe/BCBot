using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BCBot.Controllers.Listeners.Github.Models;
using BCBot.Core.Teams;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BCBot.Controllers.Listeners.Github
{
    public class GithubAggregator
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<AppSettings> _settings;
        private readonly ILogger<GithubAggregator> _logger;

        public GithubAggregator(HttpClient httpClient, IOptions<AppSettings> settings, ILogger<GithubAggregator> logger)
        {
            _httpClient = httpClient;
            _settings = settings;
            _logger = logger;
        }

        public async Task Handle(GithubPushNotification notification)
        {
            await SendTeamsInformationAsync(notification);
        }

        private async Task SendTeamsInformationAsync(GithubPushNotification notification)
        {
            var githubModel = notification.GithubModel;

            if (githubModel.Deleted) return;

            (var title, var text) = BuildMessage(githubModel);

            var cardData = new SuccessfulTeamsActivityCardData
            {
                Title = title,
                Text = text
            };

            await SendTeamsActivityCardAsync(cardData);
        }

        private async Task SendTeamsActivityCardAsync(TeamsActivityCardData teamsActivityCardData)
        {
            var url = _settings?.Value.GithubIncomingWebhookUrl;

            if (url == null) return;


            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var stringContent = new StringContent(teamsActivityCardData.Json);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var teamsWebHookUrl = new Uri(url);

            _logger.LogInformation($"Sending [{teamsActivityCardData.Json}] to Teams.");

            var httpResponseMessage = await _httpClient.PostAsync(teamsWebHookUrl, stringContent);

            _logger.LogInformation(
                $"PostAsync result {httpResponseMessage.StatusCode}, {httpResponseMessage.ReasonPhrase}");

            httpResponseMessage.EnsureSuccessStatusCode();

        }

        private static (string Title, string Text) BuildMessage(GithubModel model)
        {
            var branch = model.Ref.Replace("refs/heads/", "");
            var authorNames = model.Commits.Select(c => c.Author.Name).Distinct().ToList();


            var title = $"{string.Join(", ", authorNames)} committed on {branch}";

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(
                $"**{string.Join(", ", authorNames)}** committed on [{branch}]({model.Repository.HtmlUrl + "/tree/" + branch})");
            stringBuilder.AppendLine();

            foreach (var commit in model.Commits.Reverse())
            {
                stringBuilder.AppendLine($@"* {commit.Message} [{commit.Id.Substring(0, 11)}]({commit.Url})");
                stringBuilder.AppendLine();
            }

            return (title, stringBuilder.ToString());
        }
    }
}