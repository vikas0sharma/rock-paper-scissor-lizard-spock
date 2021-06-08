using RockPaperScissorLizardSpock.Models;
using System.Threading.Tasks;

namespace RockPaperScissorLizardSpock.Infrastructure.Database
{
    public interface IGameRepository
    {
        Task<string> CreateGame();
        Task<Player[]> GetPlayers(string gameId);
        Task<Player> AddPlayer(string gameId, string playerName);
    }
}