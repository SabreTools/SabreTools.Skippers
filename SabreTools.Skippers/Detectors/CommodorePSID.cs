using SabreTools.Skippers.TestTypes;

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

            // "PSID{0x0001}{0x0076}" - SID magic string, version 1, data offset 0x0076
            var rule1Test1 = new DataTest("0", "5053494400010076", true);

            // "PSID{0x0002}{0x0076}" - SID magic string, version 2, data offset 0x0076
            var rule2Test1 = new DataTest("0", "5053494400020076", true);

            // "PSID{0x0003}{0x0076}" - SID magic string, version 3, data offset 0x0076
            var rule3Test1 = new DataTest("0", "5053494400030076", true);

            // "PSID{0x0004}{0x0076}" - SID magic string, version 4, data offset 0x0076
            var rule4Test1 = new DataTest("0", "5053494400040076", true);

            // "PSID{0x0001}{0x007C}" - SID magic string, version 1, data offset 0x007C
            var rule5Test1 = new DataTest("0", "505349440001007c", true);

            // "PSID{0x0002}{0x007C}" - SID magic string, version 2, data offset 0x007C
            var rule6Test1 = new DataTest("0", "505349440002007c", true);

            // "PSID{0x0003}{0x007C}" - SID magic string, version 3, data offset 0x007C
            var rule7Test1 = new DataTest("0", "505349440003007c", true);

            // "PSID{0x0004}{0x007C}" - SID magic string, version 4, data offset 0x007C
            var rule8Test1 = new DataTest("0", "505349440004007c", true);

            // "RSID{0x0001}{0x0076}" - Real SID magic string, version 1, data offset 0x0076
            var rule9Test1 = new DataTest("0", "5253494400010076", true);

            // "RSID{0x0002}{0x0076}" - Real SID magic string, version 2, data offset 0x0076
            var rule10Test1 = new DataTest("0", "5253494400020076", true);

            // "RSID{0x0003}{0x0076}" - Real SID magic string, version 3, data offset 0x0076
            var rule11Test1 = new DataTest("0", "5253494400030076", true);

            // "RSID{0x0004}{0x0076}" - Real SID magic string, version 4, data offset 0x0076
            var rule12Test1 = new DataTest("0", "5253494400040076", true);

            // "RSID{0x0001}{0x007C}" - Real SID magic string, version 1, data offset 0x007C
            var rule13Test1 = new DataTest("0", "525349440001007c", true);

            // "RSID{0x0002}{0x007C}" - Real SID magic string, version 2, data offset 0x007C
            var rule14Test1 = new DataTest("0", "525349440002007c", true);

            // "RSID{0x0003}{0x007C}" - Real SID magic string, version 3, data offset 0x007C
            var rule15Test1 = new DataTest("0", "525349440003007c", true);

            // "RSID{0x0004}{0x007C}" - Real SID magic string, version 4, data offset 0x007C
            var rule16Test1 = new DataTest("0", "525349440004007c", true);

            // Create rules - Offset determines header skip length

            // PSID
            var rule1 = new Rule("76", "EOF", HeaderSkipOperation.None, [rule1Test1], "psid");
            var rule2 = new Rule("76", "EOF", HeaderSkipOperation.None, [rule2Test1], "psid");
            var rule3 = new Rule("76", "EOF", HeaderSkipOperation.None, [rule3Test1], "psid");
            var rule4 = new Rule("76", "EOF", HeaderSkipOperation.None, [rule4Test1], "psid");
            var rule5 = new Rule("7c", "EOF", HeaderSkipOperation.None, [rule5Test1], "psid");
            var rule6 = new Rule("7c", "EOF", HeaderSkipOperation.None, [rule6Test1], "psid");
            var rule7 = new Rule("7c", "EOF", HeaderSkipOperation.None, [rule7Test1], "psid");
            var rule8 = new Rule("7c", "EOF", HeaderSkipOperation.None, [rule8Test1], "psid");

            // RSID
            var rule9 = new Rule("76", "EOF", HeaderSkipOperation.None, [rule9Test1], "psid");
            var rule10 = new Rule("76", "EOF", HeaderSkipOperation.None, [rule10Test1], "psid");
            var rule11 = new Rule("76", "EOF", HeaderSkipOperation.None, [rule11Test1], "psid");
            var rule12 = new Rule("76", "EOF", HeaderSkipOperation.None, [rule12Test1], "psid");
            var rule13 = new Rule("7c", "EOF", HeaderSkipOperation.None, [rule13Test1], "psid");
            var rule14 = new Rule("7c", "EOF", HeaderSkipOperation.None, [rule14Test1], "psid");
            var rule15 = new Rule("7c", "EOF", HeaderSkipOperation.None, [rule15Test1], "psid");
            var rule16 = new Rule("7c", "EOF", HeaderSkipOperation.None, [rule16Test1], "psid");

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
                rule6,
                rule7,
                rule8,
                rule9,
                rule10,
                rule11,
                rule12,
                rule13,
                rule14,
                rule15,
                rule16,
            ];
        }
    }
}
