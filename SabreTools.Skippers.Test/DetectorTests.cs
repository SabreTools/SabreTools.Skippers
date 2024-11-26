using System.IO;
using System.Linq;
using SabreTools.Skippers.Detectors;
using Xunit;

namespace SabreTools.Skippers.Test
{
    public class DetectorTests
    {
        #region Atari 7800

        [Fact]
        public void Atari7800_EmptyStream_Null()
        {
            Stream input = new MemoryStream();
            string? skipperName = null;

            var detector = new Atari7800();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Fact]
        public void Atari7800_NoNameInvalidData_Null()
        {
            Stream input = new MemoryStream([0xFF, 0xFF, 0xFF, 0xFF]);
            string? skipperName = null;

            var detector = new Atari7800();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Theory]
        [InlineData(0x01, new byte[] { 0x41, 0x54, 0x41, 0x52, 0x49, 0x37, 0x38, 0x30, 0x30 })]
        [InlineData(0x64, new byte[] { 0x41, 0x43, 0x54, 0x55, 0x41, 0x4C, 0x20, 0x43, 0x41, 0x52, 0x54, 0x20, 0x44, 0x41, 0x54, 0x41, 0x20, 0x53, 0x54, 0x41, 0x52, 0x54, 0x53, 0x20, 0x48, 0x45, 0x52, 0x45 })]
        public void Atari7800_NoNameValidData_Rule(int offset, byte[] content)
        {
            byte[] data = [.. Enumerable.Repeat<byte>(0xFF, offset), .. content];
            Stream input = new MemoryStream(data);
            string? skipperName = null;

            var detector = new Atari7800();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.NotNull(rule);
            Assert.Equal("a7800", rule.SourceFile);
        }

        [Fact]
        public void Atari7800_ValidNameInvalidData_Null()
        {
            Stream input = new MemoryStream([0xFF, 0xFF, 0xFF, 0xFF]);
            string? skipperName = "Atari 7800";

            var detector = new Atari7800();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Theory]
        [InlineData(0x01, new byte[] { 0x41, 0x54, 0x41, 0x52, 0x49, 0x37, 0x38, 0x30, 0x30 })]
        [InlineData(0x64, new byte[] { 0x41, 0x43, 0x54, 0x55, 0x41, 0x4C, 0x20, 0x43, 0x41, 0x52, 0x54, 0x20, 0x44, 0x41, 0x54, 0x41, 0x20, 0x53, 0x54, 0x41, 0x52, 0x54, 0x53, 0x20, 0x48, 0x45, 0x52, 0x45 })]
        public void Atari7800_ValidNameValidData_Rule(int offset, byte[] content)
        {
            byte[] data = [.. Enumerable.Repeat<byte>(0xFF, offset), .. content];
            Stream input = new MemoryStream(data);
            string? skipperName = "Atari 7800";

            var detector = new Atari7800();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.NotNull(rule);
            Assert.Equal("a7800", rule.SourceFile);
        }

        [Fact]
        public void Atari7800_ValidSourceInvalidData_Rule()
        {
            Stream input = new MemoryStream([0xFF, 0xFF, 0xFF, 0xFF]);
            string? skipperName = "a7800";

            var detector = new Atari7800();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Theory]
        [InlineData(0x01, new byte[] { 0x41, 0x54, 0x41, 0x52, 0x49, 0x37, 0x38, 0x30, 0x30 })]
        [InlineData(0x64, new byte[] { 0x41, 0x43, 0x54, 0x55, 0x41, 0x4C, 0x20, 0x43, 0x41, 0x52, 0x54, 0x20, 0x44, 0x41, 0x54, 0x41, 0x20, 0x53, 0x54, 0x41, 0x52, 0x54, 0x53, 0x20, 0x48, 0x45, 0x52, 0x45 })]
        public void Atari7800_ValidSourceValidData_Null(int offset, byte[] content)
        {
            byte[] data = [.. Enumerable.Repeat<byte>(0xFF, offset), .. content];
            Stream input = new MemoryStream(data);
            string? skipperName = "a7800";

            var detector = new Atari7800();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.NotNull(rule);
            Assert.Equal("a7800", rule.SourceFile);
        }

        [Fact]
        public void Atari7800_InvalidName_Null()
        {
            Stream input = new MemoryStream();
            string? skipperName = "INVALID";

            var detector = new Atari7800();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        #endregion

        #region Atari Lynx

        [Fact]
        public void AtariLynx_EmptyStream_Null()
        {
            Stream input = new MemoryStream();
            string? skipperName = null;

            var detector = new AtariLynx();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Fact]
        public void AtariLynx_NoNameInvalidData_Null()
        {
            Stream input = new MemoryStream([0xFF, 0xFF, 0xFF, 0xFF]);
            string? skipperName = null;

            var detector = new AtariLynx();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Theory]
        [InlineData(0x00, new byte[] { 0x4C, 0x59, 0x4E, 0x58 })]
        [InlineData(0x06, new byte[] { 0x42, 0x53, 0x39 })]
        public void AtariLynx_NoNameValidData_Rule(int offset, byte[] content)
        {
            byte[] data = [.. Enumerable.Repeat<byte>(0xFF, offset), .. content];
            Stream input = new MemoryStream(data);
            string? skipperName = null;

            var detector = new AtariLynx();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.NotNull(rule);
            Assert.Equal("lynx", rule.SourceFile);
        }

        [Fact]
        public void AtariLynx_ValidNameInvalidData_Null()
        {
            Stream input = new MemoryStream([0xFF, 0xFF, 0xFF, 0xFF]);
            string? skipperName = "Atari Lynx";

            var detector = new AtariLynx();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Theory]
        [InlineData(0x00, new byte[] { 0x4C, 0x59, 0x4E, 0x58 })]
        [InlineData(0x06, new byte[] { 0x42, 0x53, 0x39 })]
        public void AtariLynx_ValidNameValidData_Rule(int offset, byte[] content)
        {
            byte[] data = [.. Enumerable.Repeat<byte>(0xFF, offset), .. content];
            Stream input = new MemoryStream(data);
            string? skipperName = "Atari Lynx";

            var detector = new AtariLynx();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.NotNull(rule);
            Assert.Equal("lynx", rule.SourceFile);
        }

        [Fact]
        public void AtariLynx_ValidSourceInvalidData_Rule()
        {
            Stream input = new MemoryStream([0xFF, 0xFF, 0xFF, 0xFF]);
            string? skipperName = "lynx";

            var detector = new AtariLynx();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Theory]
        [InlineData(0x00, new byte[] { 0x4C, 0x59, 0x4E, 0x58 })]
        [InlineData(0x06, new byte[] { 0x42, 0x53, 0x39 })]
        public void AtariLynx_ValidSourceValidData_Null(int offset, byte[] content)
        {
            byte[] data = [.. Enumerable.Repeat<byte>(0xFF, offset), .. content];
            Stream input = new MemoryStream(data);
            string? skipperName = "lynx";

            var detector = new AtariLynx();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.NotNull(rule);
            Assert.Equal("lynx", rule.SourceFile);
        }

        [Fact]
        public void AtariLynx_InvalidName_Null()
        {
            Stream input = new MemoryStream();
            string? skipperName = "INVALID";

            var detector = new AtariLynx();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        #endregion

        #region Commodore PSID

        [Fact]
        public void CommodorePSID_EmptyStream_Null()
        {
            Stream input = new MemoryStream();
            string? skipperName = null;

            var detector = new CommodorePSID();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Fact]
        public void CommodorePSID_NoNameInvalidData_Null()
        {
            Stream input = new MemoryStream([0xFF, 0xFF, 0xFF, 0xFF]);
            string? skipperName = null;

            var detector = new CommodorePSID();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Theory]
        [InlineData(new byte[] { 0x50, 0x53, 0x49, 0x44, 0x00, 0x01, 0x00, 0x76 })]
        [InlineData(new byte[] { 0x50, 0x53, 0x49, 0x44, 0x00, 0x03, 0x00, 0x7c })]
        [InlineData(new byte[] { 0x50, 0x53, 0x49, 0x44, 0x00, 0x02, 0x00, 0x7c })]
        [InlineData(new byte[] { 0x50, 0x53, 0x49, 0x44, 0x00, 0x01, 0x00, 0x7c })]
        [InlineData(new byte[] { 0x52, 0x53, 0x49, 0x44, 0x00, 0x02, 0x00, 0x7c })]
        public void CommodorePSID_NoNameValidData_Rule(byte[] content)
        {
            Stream input = new MemoryStream(content);
            string? skipperName = null;

            var detector = new CommodorePSID();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.NotNull(rule);
            Assert.Equal("psid", rule.SourceFile);
        }

        [Fact]
        public void CommodorePSID_ValidNameInvalidData_Null()
        {
            Stream input = new MemoryStream([0xFF, 0xFF, 0xFF, 0xFF]);
            string? skipperName = "psid";

            var detector = new CommodorePSID();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Theory]
        [InlineData(new byte[] { 0x50, 0x53, 0x49, 0x44, 0x00, 0x01, 0x00, 0x76 })]
        [InlineData(new byte[] { 0x50, 0x53, 0x49, 0x44, 0x00, 0x03, 0x00, 0x7c })]
        [InlineData(new byte[] { 0x50, 0x53, 0x49, 0x44, 0x00, 0x02, 0x00, 0x7c })]
        [InlineData(new byte[] { 0x50, 0x53, 0x49, 0x44, 0x00, 0x01, 0x00, 0x7c })]
        [InlineData(new byte[] { 0x52, 0x53, 0x49, 0x44, 0x00, 0x02, 0x00, 0x7c })]
        public void CommodorePSID_ValidNameValidData_Rule(byte[] content)
        {
            Stream input = new MemoryStream(content);
            string? skipperName = "psid";

            var detector = new CommodorePSID();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.NotNull(rule);
            Assert.Equal("psid", rule.SourceFile);
        }

        [Fact]
        public void CommodorePSID_ValidSourceInvalidData_Rule()
        {
            Stream input = new MemoryStream([0xFF, 0xFF, 0xFF, 0xFF]);
            string? skipperName = "psid";

            var detector = new CommodorePSID();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Theory]
        [InlineData(new byte[] { 0x50, 0x53, 0x49, 0x44, 0x00, 0x01, 0x00, 0x76 })]
        [InlineData(new byte[] { 0x50, 0x53, 0x49, 0x44, 0x00, 0x03, 0x00, 0x7c })]
        [InlineData(new byte[] { 0x50, 0x53, 0x49, 0x44, 0x00, 0x02, 0x00, 0x7c })]
        [InlineData(new byte[] { 0x50, 0x53, 0x49, 0x44, 0x00, 0x01, 0x00, 0x7c })]
        [InlineData(new byte[] { 0x52, 0x53, 0x49, 0x44, 0x00, 0x02, 0x00, 0x7c })]
        public void CommodorePSID_ValidSourceValidData_Null(byte[] content)
        {
            Stream input = new MemoryStream(content);
            string? skipperName = "psid";

            var detector = new CommodorePSID();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.NotNull(rule);
            Assert.Equal("psid", rule.SourceFile);
        }

        [Fact]
        public void CommodorePSID_InvalidName_Null()
        {
            Stream input = new MemoryStream();
            string? skipperName = "INVALID";

            var detector = new CommodorePSID();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        #endregion

        #region NEC PC-Engine / TurboGrafx

        [Fact]
        public void NECPCEngine_EmptyStream_Null()
        {
            Stream input = new MemoryStream();
            string? skipperName = null;

            var detector = new NECPCEngine();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Fact]
        public void NECPCEngine_NoNameInvalidData_Null()
        {
            Stream input = new MemoryStream([0xFF, 0xFF, 0xFF, 0xFF]);
            string? skipperName = null;

            var detector = new NECPCEngine();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Theory]
        [InlineData(new byte[] { 0x40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xAA, 0xBB, 0x02 })]
        public void NECPCEngine_NoNameValidData_Rule(byte[] content)
        {
            Stream input = new MemoryStream(content);
            string? skipperName = null;

            var detector = new NECPCEngine();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.NotNull(rule);
            Assert.Equal("pce", rule.SourceFile);
        }

        [Fact]
        public void NECPCEngine_ValidNameInvalidData_Null()
        {
            Stream input = new MemoryStream([0xFF, 0xFF, 0xFF, 0xFF]);
            string? skipperName = "NEC TurboGrafx-16/PC-Engine";

            var detector = new NECPCEngine();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Theory]
        [InlineData(new byte[] { 0x40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xAA, 0xBB, 0x02 })]
        public void NECPCEngine_ValidNameValidData_Rule(byte[] content)
        {
            Stream input = new MemoryStream(content);
            string? skipperName = "NEC TurboGrafx-16/PC-Engine";

            var detector = new NECPCEngine();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.NotNull(rule);
            Assert.Equal("pce", rule.SourceFile);
        }

        [Fact]
        public void NECPCEngine_ValidSourceInvalidData_Rule()
        {
            Stream input = new MemoryStream([0xFF, 0xFF, 0xFF, 0xFF]);
            string? skipperName = "pce";

            var detector = new NECPCEngine();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Theory]
        [InlineData(new byte[] { 0x40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xAA, 0xBB, 0x02 })]
        public void NECPCEngine_ValidSourceValidData_Null(byte[] content)
        {
            Stream input = new MemoryStream(content);
            string? skipperName = "pce";

            var detector = new NECPCEngine();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.NotNull(rule);
            Assert.Equal("pce", rule.SourceFile);
        }

        [Fact]
        public void NECPCEngine_InvalidName_Null()
        {
            Stream input = new MemoryStream();
            string? skipperName = "INVALID";

            var detector = new NECPCEngine();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        #endregion

        #region Nintendo 64

        [Fact]
        public void Nintendo64_EmptyStream_Null()
        {
            Stream input = new MemoryStream();
            string? skipperName = null;

            var detector = new Nintendo64();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Fact]
        public void Nintendo64_NoNameInvalidData_Null()
        {
            Stream input = new MemoryStream([0xFF, 0xFF, 0xFF, 0xFF]);
            string? skipperName = null;

            var detector = new Nintendo64();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Theory]
        [InlineData(new byte[] { 0x80, 0x37, 0x12, 0x40 })]
        [InlineData(new byte[] { 0x37, 0x80, 0x40, 0x12 })]
        [InlineData(new byte[] { 0x40, 0x12, 0x37, 0x80 })]
        public void Nintendo64_NoNameValidData_Rule(byte[] content)
        {
            Stream input = new MemoryStream(content);
            string? skipperName = null;

            var detector = new Nintendo64();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.NotNull(rule);
            Assert.Equal("n64", rule.SourceFile);
        }

        [Fact]
        public void Nintendo64_ValidNameInvalidData_Null()
        {
            Stream input = new MemoryStream([0xFF, 0xFF, 0xFF, 0xFF]);
            string? skipperName = "Nintendo 64 - ABCD";

            var detector = new Nintendo64();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Theory]
        [InlineData(new byte[] { 0x80, 0x37, 0x12, 0x40 })]
        [InlineData(new byte[] { 0x37, 0x80, 0x40, 0x12 })]
        [InlineData(new byte[] { 0x40, 0x12, 0x37, 0x80 })]
        public void Nintendo64_ValidNameValidData_Rule(byte[] content)
        {
            Stream input = new MemoryStream(content);
            string? skipperName = "Nintendo 64 - ABCD";

            var detector = new Nintendo64();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.NotNull(rule);
            Assert.Equal("n64", rule.SourceFile);
        }

        [Fact]
        public void Nintendo64_ValidSourceInvalidData_Rule()
        {
            Stream input = new MemoryStream([0xFF, 0xFF, 0xFF, 0xFF]);
            string? skipperName = "n64";

            var detector = new Nintendo64();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Theory]
        [InlineData(new byte[] { 0x80, 0x37, 0x12, 0x40 })]
        [InlineData(new byte[] { 0x37, 0x80, 0x40, 0x12 })]
        [InlineData(new byte[] { 0x40, 0x12, 0x37, 0x80 })]
        public void Nintendo64_ValidSourceValidData_Null(byte[] content)
        {
            Stream input = new MemoryStream(content);
            string? skipperName = "n64";

            var detector = new Nintendo64();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.NotNull(rule);
            Assert.Equal("n64", rule.SourceFile);
        }

        [Fact]
        public void Nintendo64_InvalidName_Null()
        {
            Stream input = new MemoryStream();
            string? skipperName = "INVALID";

            var detector = new Nintendo64();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        #endregion

        #region Nintendo Famicom / Nintendo Entertainment System

        [Fact]
        public void NintendoEntertainmentSystem_EmptyStream_Null()
        {
            Stream input = new MemoryStream();
            string? skipperName = null;

            var detector = new NintendoEntertainmentSystem();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Fact]
        public void NintendoEntertainmentSystem_NoNameInvalidData_Null()
        {
            Stream input = new MemoryStream([0xFF, 0xFF, 0xFF, 0xFF]);
            string? skipperName = null;

            var detector = new NintendoEntertainmentSystem();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Theory]
        [InlineData(new byte[] { 0x4E, 0x45, 0x53, 0x1A })]
        public void NintendoEntertainmentSystem_NoNameValidData_Rule(byte[] content)
        {
            Stream input = new MemoryStream(content);
            string? skipperName = null;

            var detector = new NintendoEntertainmentSystem();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.NotNull(rule);
            Assert.Equal("nes", rule.SourceFile);
        }

        [Fact]
        public void NintendoEntertainmentSystem_ValidNameInvalidData_Null()
        {
            Stream input = new MemoryStream([0xFF, 0xFF, 0xFF, 0xFF]);
            string? skipperName = "Nintendo Famicon/NES";

            var detector = new NintendoEntertainmentSystem();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Theory]
        [InlineData(new byte[] { 0x4E, 0x45, 0x53, 0x1A })]
        public void NintendoEntertainmentSystem_ValidNameValidData_Rule(byte[] content)
        {
            Stream input = new MemoryStream(content);
            string? skipperName = "Nintendo Famicon/NES";

            var detector = new NintendoEntertainmentSystem();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.NotNull(rule);
            Assert.Equal("nes", rule.SourceFile);
        }

        [Fact]
        public void NintendoEntertainmentSystem_ValidSourceInvalidData_Rule()
        {
            Stream input = new MemoryStream([0xFF, 0xFF, 0xFF, 0xFF]);
            string? skipperName = "nes";

            var detector = new NintendoEntertainmentSystem();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Theory]
        [InlineData(new byte[] { 0x4E, 0x45, 0x53, 0x1A })]
        public void NintendoEntertainmentSystem_ValidSourceValidData_Null(byte[] content)
        {
            Stream input = new MemoryStream(content);
            string? skipperName = "nes";

            var detector = new NintendoEntertainmentSystem();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.NotNull(rule);
            Assert.Equal("nes", rule.SourceFile);
        }

        [Fact]
        public void NintendoEntertainmentSystem_InvalidName_Null()
        {
            Stream input = new MemoryStream();
            string? skipperName = "INVALID";

            var detector = new NintendoEntertainmentSystem();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        #endregion

        #region Nintendo Famicom Disk System

        [Fact]
        public void NintendoFamicomDiskSystem_EmptyStream_Null()
        {
            Stream input = new MemoryStream();
            string? skipperName = null;

            var detector = new NintendoFamicomDiskSystem();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Fact]
        public void NintendoFamicomDiskSystem_NoNameInvalidData_Null()
        {
            Stream input = new MemoryStream([0xFF, 0xFF, 0xFF, 0xFF]);
            string? skipperName = null;

            var detector = new NintendoFamicomDiskSystem();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Theory]
        [InlineData(new byte[] { 0x46, 0x44, 0x53, 0x1A, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })]
        [InlineData(new byte[] { 0x46, 0x44, 0x53, 0x1A, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })]
        [InlineData(new byte[] { 0x46, 0x44, 0x53, 0x1A, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })]
        [InlineData(new byte[] { 0x46, 0x44, 0x53, 0x1A, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })]

        public void NintendoFamicomDiskSystem_NoNameValidData_Rule(byte[] content)
        {
            Stream input = new MemoryStream(content);
            string? skipperName = null;

            var detector = new NintendoFamicomDiskSystem();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.NotNull(rule);
            Assert.Equal("fds", rule.SourceFile);
        }

        [Fact]
        public void NintendoFamicomDiskSystem_ValidNameInvalidData_Null()
        {
            Stream input = new MemoryStream([0xFF, 0xFF, 0xFF, 0xFF]);
            string? skipperName = "fds";

            var detector = new NintendoFamicomDiskSystem();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Theory]
        [InlineData(new byte[] { 0x46, 0x44, 0x53, 0x1A, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })]
        [InlineData(new byte[] { 0x46, 0x44, 0x53, 0x1A, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })]
        [InlineData(new byte[] { 0x46, 0x44, 0x53, 0x1A, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })]
        [InlineData(new byte[] { 0x46, 0x44, 0x53, 0x1A, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })]

        public void NintendoFamicomDiskSystem_ValidNameValidData_Rule(byte[] content)
        {
            Stream input = new MemoryStream(content);
            string? skipperName = "fds";

            var detector = new NintendoFamicomDiskSystem();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.NotNull(rule);
            Assert.Equal("fds", rule.SourceFile);
        }

        [Fact]
        public void NintendoFamicomDiskSystem_ValidSourceInvalidData_Rule()
        {
            Stream input = new MemoryStream([0xFF, 0xFF, 0xFF, 0xFF]);
            string? skipperName = "fds";

            var detector = new NintendoFamicomDiskSystem();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Theory]
        [InlineData(new byte[] { 0x46, 0x44, 0x53, 0x1A, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })]
        [InlineData(new byte[] { 0x46, 0x44, 0x53, 0x1A, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })]
        [InlineData(new byte[] { 0x46, 0x44, 0x53, 0x1A, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })]
        [InlineData(new byte[] { 0x46, 0x44, 0x53, 0x1A, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })]

        public void NintendoFamicomDiskSystem_ValidSourceValidData_Null(byte[] content)
        {
            Stream input = new MemoryStream(content);
            string? skipperName = "fds";

            var detector = new NintendoFamicomDiskSystem();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.NotNull(rule);
            Assert.Equal("fds", rule.SourceFile);
        }

        [Fact]
        public void NintendoFamicomDiskSystem_InvalidName_Null()
        {
            Stream input = new MemoryStream();
            string? skipperName = "INVALID";

            var detector = new NintendoFamicomDiskSystem();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        #endregion

        #region Nintendo Super Famicom / Super Nintendo Entertainment System

        [Fact]
        public void SuperNintendoEntertainmentSystem_EmptyStream_Null()
        {
            Stream input = new MemoryStream();
            string? skipperName = null;

            var detector = new SuperNintendoEntertainmentSystem();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Fact]
        public void SuperNintendoEntertainmentSystem_NoNameInvalidData_Null()
        {
            Stream input = new MemoryStream([0xFF, 0xFF, 0xFF, 0xFF]);
            string? skipperName = null;

            var detector = new SuperNintendoEntertainmentSystem();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Theory]
        [InlineData(0x16, new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })]
        [InlineData(0x16, new byte[] { 0xAA, 0xBB, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00 })]
        [InlineData(0x16, new byte[] { 0x53, 0x55, 0x50, 0x45, 0x52, 0x55, 0x46, 0x4F })]
        public void SuperNintendoEntertainmentSystem_NoNameValidData_Rule(int offset, byte[] content)
        {
            byte[] data = [.. Enumerable.Repeat<byte>(0xFF, offset), .. content];
            Stream input = new MemoryStream(data);
            string? skipperName = null;

            var detector = new SuperNintendoEntertainmentSystem();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.NotNull(rule);
            Assert.Equal("snes", rule.SourceFile);
        }

        [Fact]
        public void SuperNintendoEntertainmentSystem_ValidNameInvalidData_Null()
        {
            Stream input = new MemoryStream([0xFF, 0xFF, 0xFF, 0xFF]);
            string? skipperName = "Nintendo Super Famicom/SNES";

            var detector = new SuperNintendoEntertainmentSystem();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Theory]
        [InlineData(0x16, new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })]
        [InlineData(0x16, new byte[] { 0xAA, 0xBB, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00 })]
        [InlineData(0x16, new byte[] { 0x53, 0x55, 0x50, 0x45, 0x52, 0x55, 0x46, 0x4F })]
        public void SuperNintendoEntertainmentSystem_ValidNameValidData_Rule(int offset, byte[] content)
        {
            byte[] data = [.. Enumerable.Repeat<byte>(0xFF, offset), .. content];
            Stream input = new MemoryStream(data);
            string? skipperName = "Nintendo Super Famicom/SNES";

            var detector = new SuperNintendoEntertainmentSystem();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.NotNull(rule);
            Assert.Equal("snes", rule.SourceFile);
        }

        [Fact]
        public void SuperNintendoEntertainmentSystem_ValidSourceInvalidData_Rule()
        {
            Stream input = new MemoryStream([0xFF, 0xFF, 0xFF, 0xFF]);
            string? skipperName = "snes";

            var detector = new SuperNintendoEntertainmentSystem();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Theory]
        [InlineData(0x16, new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })]
        [InlineData(0x16, new byte[] { 0xAA, 0xBB, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00 })]
        [InlineData(0x16, new byte[] { 0x53, 0x55, 0x50, 0x45, 0x52, 0x55, 0x46, 0x4F })]
        public void SuperNintendoEntertainmentSystem_ValidSourceValidData_Null(int offset, byte[] content)
        {
            byte[] data = [.. Enumerable.Repeat<byte>(0xFF, offset), .. content];
            Stream input = new MemoryStream(data);
            string? skipperName = "snes";

            var detector = new SuperNintendoEntertainmentSystem();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.NotNull(rule);
            Assert.Equal("snes", rule.SourceFile);
        }

        [Fact]
        public void SuperNintendoEntertainmentSystem_InvalidName_Null()
        {
            Stream input = new MemoryStream();
            string? skipperName = "INVALID";

            var detector = new SuperNintendoEntertainmentSystem();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        #endregion

        #region Nintendo Super Famicom SPC / Super Nintendo Entertainment System SPC

        [Fact]
        public void SuperFamicomSPC_EmptyStream_Null()
        {
            Stream input = new MemoryStream();
            string? skipperName = null;

            var detector = new SuperFamicomSPC();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Fact]
        public void SuperFamicomSPC_NoNameInvalidData_Null()
        {
            Stream input = new MemoryStream([0xFF, 0xFF, 0xFF, 0xFF]);
            string? skipperName = null;

            var detector = new SuperFamicomSPC();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Theory]
        [InlineData(new byte[] { 0x53, 0x4E, 0x45, 0x53, 0x2D, 0x53, 0x50, 0x43 })]
        public void SuperFamicomSPC_NoNameValidData_Rule(byte[] content)
        {
            Stream input = new MemoryStream(content);
            string? skipperName = null;

            var detector = new SuperFamicomSPC();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.NotNull(rule);
            Assert.Equal("spc", rule.SourceFile);
        }

        [Fact]
        public void SuperFamicomSPC_ValidNameInvalidData_Null()
        {
            Stream input = new MemoryStream([0xFF, 0xFF, 0xFF, 0xFF]);
            string? skipperName = "Nintendo Super Famicon SPC";

            var detector = new SuperFamicomSPC();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Theory]
        [InlineData(new byte[] { 0x53, 0x4E, 0x45, 0x53, 0x2D, 0x53, 0x50, 0x43 })]
        public void SuperFamicomSPC_ValidNameValidData_Rule(byte[] content)
        {
            Stream input = new MemoryStream(content);
            string? skipperName = "Nintendo Super Famicon SPC";

            var detector = new SuperFamicomSPC();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.NotNull(rule);
            Assert.Equal("spc", rule.SourceFile);
        }

        [Fact]
        public void SuperFamicomSPC_ValidSourceInvalidData_Rule()
        {
            Stream input = new MemoryStream([0xFF, 0xFF, 0xFF, 0xFF]);
            string? skipperName = "spc";

            var detector = new SuperFamicomSPC();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        [Theory]
        [InlineData(new byte[] { 0x53, 0x4E, 0x45, 0x53, 0x2D, 0x53, 0x50, 0x43 })]
        public void SuperFamicomSPC_ValidSourceValidData_Null(byte[] content)
        {
            Stream input = new MemoryStream(content);
            string? skipperName = "spc";

            var detector = new SuperFamicomSPC();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.NotNull(rule);
            Assert.Equal("spc", rule.SourceFile);
        }

        [Fact]
        public void SuperFamicomSPC_InvalidName_Null()
        {
            Stream input = new MemoryStream();
            string? skipperName = "INVALID";

            var detector = new SuperFamicomSPC();
            Rule? rule = detector.GetMatchingRule(input, skipperName);
            Assert.Null(rule);
        }

        #endregion
    }
}