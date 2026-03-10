using SabreTools.Skippers.TestTypes;

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

            // "NES{0x1A}" - iNES/NES magic string
            var inesTest = new DataTest("0", "4E45531A", true);

            // Create rules

            // iNES/NES header is 16 (0x10) bytes long
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
