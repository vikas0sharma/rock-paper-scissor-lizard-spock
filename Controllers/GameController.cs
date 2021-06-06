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
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<GameController> logger;
        private readonly IGameRepository gameRepository;

        public GameController(ILogger<GameController> logger, IGameRepository gameRepository)
        {
            this.logger = logger;
            this.gameRepository = gameRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Game game = new Game
            {
                Id = Guid.NewGuid().ToShortGuid()
            };
            await gameRepository.CreateGame(game);
            return Ok(game.Id);
        }
        [HttpGet("players")]
        public async Task<IActionResult> GetPlayers(string gameId)
        {
            var data = await gameRepository.GetPlayers(gameId);
            return Ok();
        }

    }
}
