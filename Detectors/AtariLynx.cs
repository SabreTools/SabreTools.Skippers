using SabreTools.Skippers.Tests;

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
            var rule1Test1 = new DataTest("0", "4C594E58", true);
            var rule2Test1 = new DataTest("6", "425339", true);

            // Create rules
            var rule1 = new Rule("40", "EOF", HeaderSkipOperation.None, [rule1Test1], "lynx");
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
