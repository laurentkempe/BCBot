using System.Threading.Tasks;
using MediatR;

namespace BCBot.Controllers.Listeners.Github
{
    public class GithubPushNotificationHandler : AsyncNotificationHandler<GithubPushNotification>
    {
        private GithubAggregator Aggregator { get; }

        public GithubPushNotificationHandler(GithubAggregator  githubAggregator)
        {
            Aggregator = githubAggregator;
        }

        protected override Task HandleCore(GithubPushNotification notification)
        {
            return Aggregator.Handle(notification);
        }
    }
}