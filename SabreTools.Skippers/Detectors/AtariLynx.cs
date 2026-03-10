using SabreTools.Skippers.TestTypes;

namespace SabreTools.Skippers.Detectors
{
    /// <summary>
    /// Detector for Atari Lynx headers
    /// </summary>
    /// <remarks>Originally from lynx.xml</remarks>
    internal class AtariLynx : Detector
    {
        public AtariLynx()
        {
            // Create tests

            // "LYNX" - LNX magic string
            var rule1Test1 = new DataTest("0", "4C594E58", true);

            // "BS9" - Homebrew software magic string
            var rule2Test1 = new DataTest("6", "425339", true);

            // Create rules

            // LNX header is 64 (0x40) bytes long
            var rule1 = new Rule("40", "EOF", HeaderSkipOperation.None, [rule1Test1], "lynx");

            // Homebrew header is 64 (0x40) bytes long(?)
            var rule2 = new Rule("40", "EOF", HeaderSkipOperation.None, [rule2Test1], "lynx");

            // Create file
            Name = "Atari Lynx";
            Author = "Roman Scherzer";
            Version = "1.0";
            SourceFile = "lynx";
            Rules =
            [
                rule1,
                rule2,
            ];
        }
    }
}
