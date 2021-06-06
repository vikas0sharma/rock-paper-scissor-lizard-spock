using RockPaperScissorLizardSpock.Models;
using System.Threading.Tasks;

namespace RockPaperScissorLizardSpock.Infrastructure.Database
{
    public interface IGameRepository
    {
        Task<bool> CreateGame(Game game);
        Task<Player[]> GetPlayers(string gameId);
    }
}