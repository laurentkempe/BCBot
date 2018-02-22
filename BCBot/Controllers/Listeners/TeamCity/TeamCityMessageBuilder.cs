using System;
using System.Collections.Generic;
using System.Linq;
using BCBot.Controllers.Listeners.TeamCity.Models;
using BCBot.Core.Teams;

namespace BCBot.Controllers.Listeners.TeamCity
{
    public class TeamCityMessageBuilder
    {
        private readonly int _expectedBuildCount;

        public TeamCityMessageBuilder(int expectedBuildCount)
        {
            _expectedBuildCount = expectedBuildCount;
        }

        private static bool IsSuccessfulBuild(Build b)
        {
            return b.buildResult.Equals("success", StringComparison.OrdinalIgnoreCase);
        }

        public TeamsActivityCardData BuildTeamsActivityCard(List<TeamCityModel> buildStatuses)
        {
            var success = buildStatuses.Count == _expectedBuildCount &&
                          buildStatuses.All(buildStatus => IsSuccessfulBuild(buildStatus.build));

            return success
                ? BuildSuccessTeamsActivityCard(buildStatuses)
                : BuildFailureTeamsActivityCard(buildStatuses);
        }

        private TeamsActivityCardData BuildFailureTeamsActivityCard(List<TeamCityModel> buildStatuses)
        {
            var build = buildStatuses.First().build;

            return new FailedTeamsActivityCardData
            {
                Title = $"Failed to build branch {build.branchName}",
                Text = $@"Failed to build {build.projectName} branch {build.branchName} with [build number {build.buildNumber}]({build.buildStatusUrl})."
            };
        }

        private TeamsActivityCardData BuildSuccessTeamsActivityCard(List<TeamCityModel> buildStatuses)
        {
            var build = buildStatuses.First().build;

            return new SuccessfulTeamsActivityCardData
            {
                Title = $"Successfully built branch {build.branchName}",
                Text = $@"Successfully built {build.projectName} branch {build.branchName} with [build number {build.buildNumber}]({build.buildStatusUrl})."
            };
        }
    }
}