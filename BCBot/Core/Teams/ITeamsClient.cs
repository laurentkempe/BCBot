using System.Threading.Tasks;

namespace BCBot.Core.Teams
{
    public interface ITeamsClient
    {
        Task SendActivityCardAsync(TeamsActivityCardData teamsActivityCardData, string teamsWebhookUrl);
    }
}