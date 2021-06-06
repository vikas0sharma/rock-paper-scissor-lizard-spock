namespace RockPaperScissorLizardSpock.Models
{
    public class Game
    {
        public string Id { get; set; }
        public Player[] Players { get; set; }
        public override string ToString() => $"Id: {Id}, Players: {Players}";
    }
}
