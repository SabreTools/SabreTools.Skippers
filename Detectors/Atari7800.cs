using SabreTools.Skippers.Tests;

namespace SabreTools.Skippers.Detectors
{
    /// <summary>
    /// Detector for Atari 7800 headers
    /// </summary>
    /// <remarks>Originally from a7800.xml</remarks>
    internal class Atari7800 : Detector
    {
        public Atari7800()
        {
            // Create tests
            var rule1Test1 = new DataTest("1", "415441524937383030", true);
            var rule2Test1 = new DataTest("64", "41435455414C20434152542044415441205354415254532048455245", true);

            // Create rules
            var rule1 = new Rule("80", "EOF", HeaderSkipOperation.None, [rule1Test1], "a7800");
            var rule2 = new Rule("80", "EOF", HeaderSkipOperation.None, [rule2Test1], "a7800");

            // Create file
            Name = "Atari 7800";
            Author = "Roman Scherzer";
            Version = "1.0";
            SourceFile = "a7800";
            Rules =
            [
                rule1,
                rule2,
            ];
        }
    }
}
