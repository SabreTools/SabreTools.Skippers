using SabreTools.Skippers.Tests;

namespace SabreTools.Skippers.Detectors
{
    /// <summary>
    /// Detector for Super Famicom SPC headers
    /// </summary>
    /// <remarks>Originally from spc.xml</remarks>
    internal class SuperFamicomSPC : Detector
    {
        public SuperFamicomSPC()
        {
            // Create tests
            var rule1Test1 = new DataTest("0", "534E45532D535043", true);

            // Create rules
            var rule1 = new Rule("00100", "EOF", HeaderSkipOperation.None, [rule1Test1], "spc");

            // Create file
            Name = "Nintendo Super Famicon SPC";
            Author = "Yori Yoshizuki";
            Version = "1.0";
            SourceFile = "spc";
            Rules =
            [
                rule1,
            ];
        }
    }
}
