using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RockPaperScissorLizardSpock.Models;
using StackExchange.Redis;
namespace RockPaperScissorLizardSpock.Infrastructure.Database
{
    public class GameRepository : IGameRepository
    {
        private readonly IDatabase database;
        public GameRepository(ConnectionMultiplexer redis)
        {
            this.database = redis.GetDatabase();
        }
        public async Task<bool> CreateGame(Game game)
        {
            var done = await database.StringSetAsync(game.Id, JsonConvert.SerializeObject(game), expiry: TimeSpan.FromDays(1));
            return done;
        }
        public async Task<Player[]> GetPlayers(string gameId)
        {
            var data = await database.StringGetAsync(gameId);
            if (data.IsNullOrEmpty)
            {
                return new Player[] { };
            }
            var game = JsonConvert.DeserializeObject<Game>(data);
            return game.Players;
        }
    }
}
