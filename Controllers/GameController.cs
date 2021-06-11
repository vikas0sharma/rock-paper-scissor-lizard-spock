using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RockPaperScissorLizardSpock.Infrastructure;
using RockPaperScissorLizardSpock.Infrastructure.Database;
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

        public GameController(ILogger<GameController> logger, IGameRepository gameRepository)
        {
            this.logger = logger;
            this.gameRepository = gameRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGame() => Ok(await gameRepository.CreateGame());

        [HttpPost("{gameId}/players")]
        public async Task<IActionResult> AddPlayer(string gameId, [FromBody] Player player)
        {
            var p = await gameRepository.AddPlayer(gameId, player.Name);
            return Ok(p.Id);
        }

        public async Task<IActionResult> UpdatePlayerChoice(string gameId, [FromBody] Player player)
        {
            return Ok();
        }

        [HttpGet("players")]
        public async Task<IActionResult> GetPlayers(string gameId)
        {
            var data = await gameRepository.GetPlayers(gameId);
            return Ok();
        }

    }
}
