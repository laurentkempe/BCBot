namespace BCBot.Controllers.Listeners.TeamCity
{
    public class BuildConfiguration
    {
        public BuildConfiguration()
        {
            Name = string.Empty;
            TeamCityIncomingWebhookUrl = string.Empty;
            MaxWaitDurationInMinutes = 0.0;
            BuildConfigurationIds = string.Empty;
        }

        public string Name { get; set; }

        public string TeamCityIncomingWebhookUrl { get; set; }

        public double MaxWaitDurationInMinutes { get; set; }

        public string BuildConfigurationIds { get; set; }
    }
}