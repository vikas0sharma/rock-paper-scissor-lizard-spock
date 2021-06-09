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
        public string Winner { get; set; }
        public void GetWinner()
        {
            Player[] winners = ChooseWinner(Players);
        }
        public Player[] ChooseWinner(Player[] players)
        {
            if (players.Any(p => p.Choice == Choice.Unknown)) return new Player[] { };
            if (players.Length == 1) return players;

            List<Player> newWinners = new List<Player>();

            for (int i = 0; i < players.Length - 1; i++)
            {
                for (int j = 0; j < players.Length - i - 1; j++)
                {
                    int nxt = j + 1;
                    if (players[j].Choice == players[nxt].Choice) break;

                    if(players[j].Choice == Choice.Rock)
                    {
                        if(players[nxt].Choice == Choice.Lizard)
                        {
                            newWinners.Add(players[j]);
                        }
                        else if(players[nxt].Choice == Choice.Scissor)
                        {
                            newWinners.Add(players[j]);
                        }
                        else if (players[nxt].Choice == Choice.Paper)
                        {
                            newWinners.Add(players[nxt]);
                        }
                        else if (players[nxt].Choice == Choice.Spock)
                        {
                            newWinners.Add(players[nxt]);
                        }
                    }
                    else if(players[j].Choice == Choice.Paper)
                    {
                        if (players[nxt].Choice == Choice.Lizard)
                        {
                            newWinners.Add(players[nxt]);
                        }
                        else if (players[nxt].Choice == Choice.Scissor)
                        {
                            newWinners.Add(players[nxt]);
                        }
                        else if (players[nxt].Choice == Choice.Rock)
                        {
                            newWinners.Add(players[j]);
                        }
                        else if (players[nxt].Choice == Choice.Spock)
                        {
                            newWinners.Add(players[j]);
                        }
                    }
                    else if (players[j].Choice == Choice.Scissor)
                    {
                        if (players[nxt].Choice == Choice.Lizard)
                        {
                            newWinners.Add(players[j]);
                        }
                        else if (players[nxt].Choice == Choice.Paper)
                        {
                            newWinners.Add(players[j]);
                        }
                        else if (players[nxt].Choice == Choice.Rock)
                        {
                            newWinners.Add(players[nxt]);
                        }
                        else if (players[nxt].Choice == Choice.Spock)
                        {
                            newWinners.Add(players[nxt]);
                        }
                    }
                    else if (players[j].Choice == Choice.Lizard)
                    {
                        if (players[nxt].Choice == Choice.Paper)
                        {
                            newWinners.Add(players[j]);
                        }
                        else if (players[nxt].Choice == Choice.Scissor)
                        {
                            newWinners.Add(players[nxt]);
                        }
                        else if (players[nxt].Choice == Choice.Rock)
                        {
                            newWinners.Add(players[nxt]);
                        }
                        else if (players[nxt].Choice == Choice.Spock)
                        {
                            newWinners.Add(players[j]);
                        }
                    }
                    else if (players[j].Choice == Choice.Spock)
                    {
                        if (players[nxt].Choice == Choice.Paper)
                        {
                            newWinners.Add(players[nxt]);
                        }
                        else if (players[nxt].Choice == Choice.Scissor)
                        {
                            newWinners.Add(players[j]);
                        }
                        else if (players[nxt].Choice == Choice.Rock)
                        {
                            newWinners.Add(players[j]);
                        }
                        else if (players[nxt].Choice == Choice.Lizard)
                        {
                            newWinners.Add(players[nxt]);
                        }
                    }
                }
            }

            return ChooseWinner(newWinners.ToArray());
        }

        public override string ToString() => $"Id: {Id}, Players: {Players}";
    }
}
