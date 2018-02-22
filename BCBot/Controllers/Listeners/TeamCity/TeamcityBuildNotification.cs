using BCBot.Controllers.Listeners.TeamCity.Models;
using MediatR;

namespace BCBot.Controllers.Listeners.TeamCity
{
    public class TeamcityBuildNotification : INotification
    {
        public TeamCityModel TeamCityModel { get; }

        public TeamcityBuildNotification(TeamCityModel teamCityModel)
        {
            TeamCityModel = teamCityModel;
        }
    }
}