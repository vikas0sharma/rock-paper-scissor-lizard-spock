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
            GetWinner();
        }

        public void GetWinner()
        {
            ChooseWinner(Players);
        }
        public void ChooseWinner(Player[] players)
        {
            Result.Winner = "";
            Result.Win = "";
            Result.Choices = null;

            if (players.Any(p => p.Choice == Choice.Unknown)) return;
            List<string> resultText = new List<string>();
            Player winner = players[0];
            for (int j = 1; j < players.Length; j++)
            {

                if (players[j].Choice == Choice.Rock)
                {
                    if (winner.Choice == Choice.Lizard)
                    {
                        resultText.Add($"{players[j].Choice} chrushes {winner.Choice}");
                        winner = players[j];
                    }
                    else if (winner.Choice == Choice.Scissor)
                    {
                        resultText.Add($"{players[j].Choice} smashes {winner.Choice}");
                        winner = players[j];
                    }
                    else if (winner.Choice == Choice.Spock)
                    {
                        resultText.Add($"{winner.Choice} vaporizes {players[j].Choice}");
                    }
                    else if (winner.Choice == Choice.Paper)
                    {
                        resultText.Add($"{winner.Choice} covers {players[j].Choice}");
                    }

                }
                else if (players[j].Choice == Choice.Paper)
                {
                    if (winner.Choice == Choice.Rock)
                    {
                        resultText.Add($"{players[j].Choice} covers {winner.Choice}");
                        winner = players[j];
                    }
                    else if (winner.Choice == Choice.Spock)
                    {
                        resultText.Add($"{players[j].Choice} disproves {winner.Choice}");
                        winner = players[j];
                    }
                    else if (winner.Choice == Choice.Scissor)
                    {
                        resultText.Add($"{winner.Choice} cuts {players[j].Choice}");
                    }
                    else if (winner.Choice == Choice.Lizard)
                    {
                        resultText.Add($"{winner.Choice} eats {players[j].Choice}");
                    }
                }
                else if (players[j].Choice == Choice.Scissor)
                {
                    if (winner.Choice == Choice.Lizard)
                    {
                        resultText.Add($"{players[j].Choice} decapitates {winner.Choice}");
                        winner = players[j];
                    }
                    else if (winner.Choice == Choice.Paper)
                    {
                        resultText.Add($"{players[j].Choice} cuts {winner.Choice}");
                        winner = players[j];
                    }
                    else if (winner.Choice == Choice.Spock)
                    {
                        resultText.Add($"{winner.Choice} smashes {players[j].Choice}");
                    }
                    else if (winner.Choice == Choice.Rock)
                    {
                        resultText.Add($"{winner.Choice} smashes {players[j].Choice}");
                    }
                }
                else if (players[j].Choice == Choice.Lizard)
                {
                    if (winner.Choice == Choice.Paper)
                    {
                        resultText.Add($"{players[j].Choice} eats {winner.Choice}");
                        winner = players[j];
                    }
                    else if (winner.Choice == Choice.Spock)
                    {
                        resultText.Add($"{players[j].Choice} poisons {winner.Choice}");
                        winner = players[j];
                    }
                    else if (winner.Choice == Choice.Rock)
                    {
                        resultText.Add($"{winner.Choice} smashes {players[j].Choice}");
                    }
                    else if (winner.Choice == Choice.Scissor)
                    {
                        resultText.Add($"{winner.Choice} decapitates {players[j].Choice}");
                    }
                }
                else if (players[j].Choice == Choice.Spock)
                {
                    if (winner.Choice == Choice.Scissor)
                    {
                        resultText.Add($"{players[j].Choice} smashes {winner.Choice}");
                        winner = players[j];
                    }
                    else if (winner.Choice == Choice.Rock)
                    {
                        resultText.Add($"{players[j].Choice} vaporizes {winner.Choice}");
                        winner = players[j];
                    }
                    else if (winner.Choice == Choice.Paper)
                    {
                        resultText.Add($"{winner.Choice} disproves {players[j].Choice}");
                    }
                    else if (winner.Choice == Choice.Lizard)
                    {
                        resultText.Add($"{winner.Choice} poisons {players[j].Choice}");
                    }
                }
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

            Players = Players.Select(p => new Player { Id = p.Id, Name = p.Name }).ToArray();
        }

        public override string ToString() => $"Id: {Id}, Players: {Players}";
    }
}
