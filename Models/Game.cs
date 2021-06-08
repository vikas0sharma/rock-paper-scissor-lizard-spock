using RockPaperScissorLizardSpock.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RockPaperScissorLizardSpock.Models
{
    public class Game
    {
        public string Id { get; set; }
        public Player[] Players { get; set; } = new Player[] { };
        public int Round { get; set; } = 1;

        // Behaviour
        public Player AddPlayer(string playerName)
        {
            var player = new Player
            {
                Id = Guid.NewGuid().ToShortGuid(),
                Name = playerName,
                Choice = Choice.Unknown,
            };
            Players = Players.Append(player).ToArray();
            return player;
        }
        public void AddPlayerChoice(string playerId, Choice choice)
        {
            var player = Players.FirstOrDefault(p => p.Id == playerId);
            player.Choice = choice;
        }

        public override string ToString() => $"Id: {Id}, Players: {Players}";
    }
}
