using SabreTools.Skippers.Tests;

namespace SabreTools.Skippers.Detectors
{
    /// <summary>
    /// Detector for Nintendo Entertainment System headers
    /// </summary>
    /// <remarks>Originally from nes.xml</remarks>
    internal class NintendoEntertainmentSystem : Detector
    {
        public NintendoEntertainmentSystem()
        {
            // Create tests
            var inesTest = new DataTest("0", "4E45531A", true);

            // Create rules
            var inesRule = new Rule("10", "EOF", HeaderSkipOperation.None, [inesTest], "nes");

            // Create file
            Name = "Nintendo Famicon/NES";
            Author = "Roman Scherzer";
            Version = "1.1";
            SourceFile = "nes";
            Rules =
            [
                inesRule,
            ];
        }
    }
}
