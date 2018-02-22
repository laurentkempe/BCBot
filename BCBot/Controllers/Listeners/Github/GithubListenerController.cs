using System.Net;
using System.Threading.Tasks;
using BCBot.Controllers.Listeners.Github.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BCBot.Controllers.Listeners.Github
{
    [Route("/github/listener")]
    public class GithubListenerController : Controller
    {
        private readonly IMediator _mediator;

        public GithubListenerController(IMediator mediator, IOptions<AppSettings> settings)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<HttpStatusCode> Build([FromBody] GithubModel githubModel)
        {
            await _mediator.Publish(new GithubPushNotification(githubModel));

            return HttpStatusCode.OK;
        }

    }
}
