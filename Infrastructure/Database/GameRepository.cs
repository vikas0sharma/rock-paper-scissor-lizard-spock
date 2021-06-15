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
        public async Task<string> CreateGame()
        {
            Game game = new Game
            {
                Id = Guid.NewGuid().ToShortGuid()
            };
            await database.StringSetAsync(game.Id, JsonConvert.SerializeObject(game), expiry: TimeSpan.FromDays(1));

            return game.Id;
        }
        public async Task<Player> AddPlayer(string gameId, string playerName)
        {
            var data = await database.StringGetAsync(gameId);
            if (data.IsNullOrEmpty) return null;
            
            var game = JsonConvert.DeserializeObject<Game>(data);
            var player = game.AddPlayer(playerName);

            await database.StringSetAsync(game.Id, JsonConvert.SerializeObject(game), expiry: TimeSpan.FromDays(1));

            return player;
        }
        public async Task<Player[]> GetPlayers(string gameId)
        {
            var data = await database.StringGetAsync(gameId);
            if (data.IsNullOrEmpty)
            {
                return new Player[] { };
            }
            var game = JsonConvert.DeserializeObject<Game>(data);

            return game.Players
                .Select(p => new Player { Id = p.Id, Name = p.Name})
                .ToArray(); // Hide the players choice
        }
        public async Task<Game> UpdatePlayerChoice(string gameId, Player player)
        {
            var data = await database.StringGetAsync(gameId);
            if (data.IsNullOrEmpty)
            {
                return null;
            }
            var game = JsonConvert.DeserializeObject<Game>(data);
            game.AddPlayerChoice(player.Id, player.Choice);
            await database.StringSetAsync(game.Id, JsonConvert.SerializeObject(game), expiry: TimeSpan.FromDays(1));
            return game;
        }
    }
}
