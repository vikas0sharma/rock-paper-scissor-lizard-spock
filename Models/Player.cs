namespace RockPaperScissorLizardSpock.Models
{
    public class Player
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Choice Choice { get; set; }
        public override string ToString() => $"Id: {Id}, Name: {Name}, Choice: {Choice}";
    }
    public enum Choice
    {
        Unknown = 0,
        Rock = 1,
        Paper = 2,
        Scissor = 3,
        Lizard = 4,
        Spock = 5
    }
}
