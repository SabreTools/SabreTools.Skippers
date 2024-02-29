using SabreTools.Skippers.Tests;

namespace SabreTools.Skippers.Detectors
{
    /// <summary>
    /// Detector for Nintendo 64 headers
    /// </summary>
    /// <remarks>Originally from n64.xml</remarks>
    internal class Nintendo64 : Detector
    {
        public Nintendo64()
        {
            // Create tests
            var v64Test = new DataTest("0", "80371240", true);
            var z64Test = new DataTest("0", "37804012", true);
            var n64Test = new DataTest("0", "40123780", true);

            // Create rules
            var v64Rule = new Rule("0", "EOF", HeaderSkipOperation.None, [v64Test], "n64");
            var z64Rule = new Rule("0", "EOF", HeaderSkipOperation.Byteswap, [z64Test], "n64");
            var n64Rule = new Rule("0", "EOF", HeaderSkipOperation.Wordswap, [n64Test], "n64");

            // Create file
            Name = "Nintendo 64 - ABCD";
            Author = "CUE";
            Version = "1.1";
            SourceFile = "n64";
            Rules =
            [
                v64Rule,
                z64Rule,
                n64Rule,
            ];
        }
    }
}
