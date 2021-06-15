using System.Collections.Generic;

namespace RockPaperScissorLizardSpock.Models
{
    public class Result
    {
        public int Round { get; set; } = 0;
        public string Win { get; set; } = "";
        public string Winner { get; set; } = "";
        public Dictionary<string, ScoreResult> Choices { get; set; }
    }
    public class ScoreResult
    {
        public Choice Choice { get; set; }
        public int Score { get; set; }
    }
}
