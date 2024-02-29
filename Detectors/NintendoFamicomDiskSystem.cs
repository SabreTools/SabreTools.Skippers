using SabreTools.Skippers.Tests;

namespace SabreTools.Skippers.Detectors
{
    /// <summary>
    /// Detector for Nintendo Famicom Disk System headers
    /// </summary>
    /// <remarks>Originally from fds.xml</remarks>
    internal class NintendoFamicomDiskSystem : Detector
    {
        public NintendoFamicomDiskSystem()
        {
            // Create tests
            var rule1Test1 = new DataTest("0", "4644531A010000000000000000000000", true);
            var rule2Test1 = new DataTest("0", "4644531A020000000000000000000000", true);
            var rule3Test1 = new DataTest("0", "4644531A030000000000000000000000", true);
            var rule4Test1 = new DataTest("0", "4644531A040000000000000000000000", true);

            // Create rules
            var rule1 = new Rule("10", null, HeaderSkipOperation.None, [rule1Test1], "fds");
            var rule2 = new Rule("10", null, HeaderSkipOperation.None, [rule2Test1], "fds");
            var rule3 = new Rule("10", null, HeaderSkipOperation.None, [rule3Test1], "fds");
            var rule4 = new Rule("10", null, HeaderSkipOperation.None, [rule4Test1], "fds");

            // Create file
            Name = "fds";
            Author = "Yori Yoshizuki";
            Version = "1.0";
            SourceFile = "fds";
            Rules =
            [
                rule1,
                rule2,
                rule3,
                rule4,
            ];
        }
    }
}
