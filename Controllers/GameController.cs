using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using RockPaperScissorLizardSpock.Infrastructure;
using RockPaperScissorLizardSpock.Infrastructure.Database;
using RockPaperScissorLizardSpock.Infrastructure.Notifications;
using RockPaperScissorLizardSpock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockPaperScissorLizardSpock.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {

        private readonly ILogger<GameController> logger;
        private readonly IGameRepository gameRepository;
        private readonly IHubContext<GameHub> hub;

        public GameController(ILogger<GameController> logger,
            IGameRepository gameRepository,
            IHubContext<GameHub> hub)
        {
            this.logger = logger;
            this.gameRepository = gameRepository;
            this.hub = hub;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGame() => Ok(await gameRepository.CreateGame());

        [HttpPost("{gameId}/players")]
        public async Task<IActionResult> AddPlayer(string gameId, [FromBody] Player player)
        {
            var plyr = await gameRepository.AddPlayer(gameId, player.Name);
            await hub.Clients.Group(gameId).SendAsync("PlayersUpdated", await gameRepository.GetPlayers(gameId));
            return Ok(plyr.Id);
        }

        [HttpPut("{gameId}/players")]
        public async Task<IActionResult> UpdatePlayerChoice(string gameId, [FromBody] Player player)
        {
            var game = await gameRepository.UpdatePlayerChoice(gameId, player);
            await hub.Clients.Group(gameId).SendAsync("WinnerSelected", game.Result);

            return Ok();
        }

        [HttpGet("{gameId}/players")]
        public async Task<IActionResult> GetPlayers(string gameId)
        {
            var data = await gameRepository.GetPlayers(gameId);
            return Ok(data);
        }

    }
}
