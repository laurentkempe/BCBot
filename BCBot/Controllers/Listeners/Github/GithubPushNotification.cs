using BCBot.Controllers.Listeners.Github.Models;
using MediatR;

namespace BCBot.Controllers.Listeners.Github
{
    public class GithubPushNotification : INotification
    {
        public GithubModel GithubModel { get; }

        public GithubPushNotification(GithubModel githubModel)
        {
            GithubModel = githubModel;
        }
    }
}