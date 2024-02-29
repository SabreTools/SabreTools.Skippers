using SabreTools.Skippers.Tests;

namespace SabreTools.Skippers.Detectors
{
    /// <summary>
    /// Detector for NEC PC-Engine / TurboGrafx 16 headers
    /// </summary>
    /// <remarks>Originally from pce.xml</remarks>
    internal class NECPCEngine : Detector
    {
        public NECPCEngine()
        {
            // Create tests
            var rule1Test1 = new DataTest("0", "4000000000000000AABB02", true);

            // Create rules
            var rule1 = new Rule("200", null, HeaderSkipOperation.None, [rule1Test1], "pce");

            // Create file
            Name = "NEC TurboGrafx-16/PC-Engine";
            Author = "Matt Nadareski (darksabre76)";
            Version = "1.0";
            SourceFile = "pce";
            Rules =
            [
                rule1,
            ];
        }
    }
}
