using System.Collections.Generic;

namespace RockPaperScissorLizardSpock.Models
{
    public class Result
    {
        public int Round { get; set; } = 0;
        public string Win { get; set; } = "";
        public Dictionary<string, int> Scores { get; set; } = new Dictionary<string, int>();

    }
}
