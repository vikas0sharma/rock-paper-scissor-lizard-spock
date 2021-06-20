using RockPaperScissorLizardSpock.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using static RockPaperScissorLizardSpock.Models.Choice;

namespace RockPaperScissorLizardSpock.Models
{
    public class Game
    {
        public string Id { get; set; }
        public Player[] Players { get; set; } = new Player[] { };
        public int Round { get; set; } = 1;
        public Result Result { get; set; } = new Result();

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
            ChooseWinner();
        }

        public void ChooseWinner()
        {
            Result.Winner = "";
            Result.Win = "";
            Result.Choices = null;

            if (Players.Any(p => p.Choice == Unknown)) return;
            List<string> resultText = new List<string>();
            Player winner = Players[0];
            for (int j = 1; j < Players.Length; j++)
            {
                string result = "";

                (winner, result) = (Players[j].Choice, winner.Choice) switch
                {
                    (Rock, Lizard) => (Players[j], $"{Rock} crushes {Lizard}"),
                    (Rock, Scissor) => (Players[j], $"{Rock} smashes {Scissor}"),
                    (Rock, Spock) => (winner, $"{Spock} vaporizes {Rock}"),
                    (Rock, Paper) => (winner, $"{Paper} covers {Rock}"),
                    (Paper, Rock) => (Players[j], $"{Paper} covers {Rock}"),
                    (Paper, Spock) => (Players[j], $"{Paper} disproves {Spock}"),
                    (Paper, Scissor) => (winner, $"{Scissor} cuts {Paper}"),
                    (Paper, Lizard) => (winner, $"{Lizard} eats {Paper}"),
                    (Scissor, Lizard) => (Players[j], $"{Scissor} decapitates {Lizard}"),
                    (Scissor, Paper) => (Players[j], $"{Paper} cuts {Scissor}"),
                    (Scissor, Spock) => (winner, $"{Spock} smashes {Scissor}"),
                    (Scissor, Rock) => (winner, $"{Rock} crushes {Scissor}"),
                    (Lizard, Paper) => (Players[j], $"{Lizard} eats {Paper}"),
                    (Lizard, Spock) => (Players[j], $"{Lizard} poisons {Spock}"),
                    (Lizard, Rock) => (winner, $"{Rock} smashes {Lizard}"),
                    (Lizard, Scissor) => (winner, $"{Scissor} decapitates {Lizard}"),
                    (Spock, Scissor) => (Players[j], $"{Spock} smashes {Scissor}"),
                    (Spock, Rock) => (Players[j], $"{Spock} vaporizes {Rock}"),
                    (Spock, Paper) => (winner, $"{Paper} disproves {Spock}"),
                    (Spock, Lizard) => (winner, $"{Lizard} poisons {Spock}"),
                    (_, _) => throw new InvalidOperationException("Invalid Choices")
                };
                resultText.Add(result);
            }
            var ties = Players.Where(p => p.Choice == winner.Choice);
            if (ties.Count() > 1)
            {
                resultText.Append($"Tie between {string.Join(",", ties.Select(t => t.Name))}");
            }
            else
            {
                Result.Winner = winner.Name;
                Players.FirstOrDefault(p => p.Id == winner.Id).Score++;
            }
            Result.Win = string.Join(",", resultText);
            Result.Round++;
            Result.Choices = Players.Select(p => new { p.Id, p.Choice, p.Score }).ToDictionary(p => p.Id, p => new ScoreResult { Choice = p.Choice, Score = p.Score });

            Players = Players.Select(p => new Player { Id = p.Id, Name = p.Name, Score = p.Score }).ToArray();
        }

        public override string ToString() => $"Id: {Id}, Players: {Players}";
    }
}
