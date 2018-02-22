using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCBot.Controllers.Listeners.Github.Models;
using BCBot.Core.Teams;
using Microsoft.Extensions.Options;

namespace BCBot.Controllers.Listeners.Github
{
    public class GithubAggregator
    {
        private readonly ITeamsClient _teamsClient;
        private readonly IOptions<AppSettings> _settings;

        public GithubAggregator(ITeamsClient teamsClient, IOptions<AppSettings> settings)
        {
            _teamsClient = teamsClient;
            _settings = settings;
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

            var cardData = new GithubPushActivityCardData
            {
                Title = title,
                Text = text
            };

            await _teamsClient.SendActivityCardAsync(cardData, _settings?.Value.GithubIncomingWebhookUrl);
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