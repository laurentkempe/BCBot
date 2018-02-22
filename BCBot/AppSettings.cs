using BCBot.Controllers.Listeners.TeamCity;

namespace BCBot
{
    public class AppSettings
    {
        public string GithubIncomingWebhookUrl { get; set; }

        public string TeamCityIncomingWebhookUrl { get; set; }

        public ServerBuildConfiguration[] BuildConfigurations { get; set; }
    }
}