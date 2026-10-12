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

            // "FDS{0x1A}" - FDS magic string, ignoring disk side count
            var fdsTest = new DataTest("0", "4644531A", true);

            // Create rules

            // FDS header is 16 (0x10) bytes long
            var fdsRule = new Rule("10", null, HeaderSkipOperation.None, [fdsTest], "fds");

            // Create file
            Name = "fds";
            Author = "Yori Yoshizuki";
            Version = "1.0";
            SourceFile = "fds";
            Rules =
            [
                fdsRule,
            ];
        }
    }
}
