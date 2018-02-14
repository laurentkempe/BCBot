namespace BCBot.Core.Teams
{
    public class TeamsActivityCardData
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Color { get; set; }

        public string Json => $@"
            {{
                ""title"": ""{Title}"",
                ""text"": ""{Text}"",
                ""themeColor"": ""{Color}""
            }}";
    }

    public class SuccessfulTeamsActivityCardData : TeamsActivityCardData
    {
        public SuccessfulTeamsActivityCardData()
        {
            Color = "00FF00";
        }
    }

    public class FailedTeamsActivityCardData : TeamsActivityCardData
    {
        public FailedTeamsActivityCardData()
        {
            Color = "FF0000";
        }
    }

    public class GithubPushActivityCardData : TeamsActivityCardData
    {
        public GithubPushActivityCardData()
        {
        }
    }}