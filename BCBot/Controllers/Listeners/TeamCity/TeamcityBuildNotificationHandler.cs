using System.Threading.Tasks;
using MediatR;

namespace BCBot.Controllers.Listeners.TeamCity
{
    public class TeamCityBuildNotificationHandler : AsyncNotificationHandler<TeamcityBuildNotification>
    {
        private TeamCityAggregator Aggregator { get; }

        public TeamCityBuildNotificationHandler(TeamCityAggregator aggregator)
        {
            Aggregator = aggregator;
        }

        protected override async Task HandleCore(TeamcityBuildNotification notification)
        {
            await Aggregator.Handle(notification);
        }
    }
}