using SabreTools.Skippers.Tests;

namespace SabreTools.Skippers.Detectors
{
    /// <summary>
    /// Detector for Super Nintendo Entertainment System headers
    /// </summary>
    /// <remarks>Originally from snes.xml</remarks>
    internal class SuperNintendoEntertainmentSystem : Detector
    {
        public SuperNintendoEntertainmentSystem()
        {
            // Create tests
            var figTest = new DataTest("16", "0000000000000000", true);
            var smcTest = new DataTest("16", "AABB040000000000", true);
            var ufoTest = new DataTest("16", "535550455255464F", true);

            // Create rules
            var figRule = new Rule("200", null, HeaderSkipOperation.None, [figTest], "snes");
            var smcRule = new Rule("200", null, HeaderSkipOperation.None, [smcTest], "snes");
            var ufoRule = new Rule("200", null, HeaderSkipOperation.None, [ufoTest], "snes");

            // Create file
            Name = "Nintendo Super Famicom/SNES";
            Author = "Matt Nadareski (darksabre76)";
            Version = "1.0";
            SourceFile = "snes";
            Rules =
            [
                figRule,
                smcRule,
                ufoRule,
            ];
        }
    }
}
