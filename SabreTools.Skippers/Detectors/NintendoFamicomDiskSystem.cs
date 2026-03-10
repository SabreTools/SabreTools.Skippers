using SabreTools.Skippers.TestTypes;

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

            // "FDS{0x1A}{0x01}" - FDS magic string with 1 disk side and 0x00 padding
            var rule1Test1 = new DataTest("0", "4644531A010000000000000000000000", true);

            // "FDS{0x1A}{0x02}" - FDS magic string with 2 disk sides and 0x00 padding
            var rule2Test1 = new DataTest("0", "4644531A020000000000000000000000", true);

            // "FDS{0x1A}{0x03}" - FDS magic string with 3 disk sides and 0x00 padding
            var rule3Test1 = new DataTest("0", "4644531A030000000000000000000000", true);

            // "FDS{0x1A}{0x04}" - FDS magic string with 4 disk sides and 0x00 padding
            var rule4Test1 = new DataTest("0", "4644531A040000000000000000000000", true);

            // Create rules

            // FDS header is 16 (0x10) bytes long
            var rule1 = new Rule("10", null, HeaderSkipOperation.None, [rule1Test1], "fds");

            // FDS header is 16 (0x10) bytes long
            var rule2 = new Rule("10", null, HeaderSkipOperation.None, [rule2Test1], "fds");

            // FDS header is 16 (0x10) bytes long
            var rule3 = new Rule("10", null, HeaderSkipOperation.None, [rule3Test1], "fds");

            // FDS header is 16 (0x10) bytes long
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
