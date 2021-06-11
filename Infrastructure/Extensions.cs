using System;

namespace RockPaperScissorLizardSpock.Infrastructure
{
    public static class Extensions
    {
        public static string ToShortGuid(this Guid input) =>
            Convert.ToBase64String(input.ToByteArray())
            .Replace("=", "")
            .Replace("+", "")
            .Replace("/", "");

    }
}
