using SabreTools.Skippers.Tests;

namespace SabreTools.Skippers.Detectors
{
    /// <summary>
    /// Detector for Commodore PSID headers
    /// </summary>
    /// <remarks>Originally from psid.xml</remarks>
    internal class CommodorePSID : Detector
    {
        public CommodorePSID()
        {
            // Create tests
            var rule1Test1 = new DataTest("0", "5053494400010076", true);
            var rule2Test1 = new DataTest("0", "505349440003007c", true);
            var rule3Test1 = new DataTest("0", "505349440002007c", true);
            var rule4Test1 = new DataTest("0", "505349440001007c", true);
            var rule5Test1 = new DataTest("0", "525349440002007c", true);

            // Create rules
            var rule1 = new Rule("76", "EOF", HeaderSkipOperation.None, [rule1Test1], "psid");
            var rule2 = new Rule("76", "EOF", HeaderSkipOperation.None, [rule2Test1], "psid");
            var rule3 = new Rule("7c", "EOF", HeaderSkipOperation.None, [rule3Test1], "psid");
            var rule4 = new Rule("7c", "EOF", HeaderSkipOperation.None, [rule4Test1], "psid");
            var rule5 = new Rule("7c", "EOF", HeaderSkipOperation.None, [rule5Test1], "psid");

            // Create file
            Name = "psid";
            Author = "Yori Yoshizuki";
            Version = "1.2";
            SourceFile = "psid";
            Rules =
            [
                rule1,
                rule2,
                rule3,
                rule4,
                rule5,
            ];
        }
    }
}
