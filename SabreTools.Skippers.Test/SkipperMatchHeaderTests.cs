using System.IO;

using SabreTools.Skippers;
using Xunit;

namespace SabreTools.Test.Skippers
{
    [Collection("SkipperMatch")]
    public class SkipperMatchHeaderTests
    {
        public SkipperMatchHeaderTests()
        {
            SkipperMatch.Init();
        }

        [Theory]
        [InlineData(0x01, new byte[] { 0x41, 0x54, 0x41, 0x52, 0x49, 0x37, 0x38, 0x30, 0x30})]
        [InlineData(0x64, new byte[] { 0x41, 0x43, 0x54, 0x55, 0x41, 0x4C, 0x20, 0x43, 0x41, 0x52, 0x54, 0x20, 0x44, 0x41, 0x54, 0x41, 0x20, 0x53, 0x54, 0x41, 0x52, 0x54, 0x53, 0x20, 0x48, 0x45, 0x52, 0x45 })]
        public void A7800Test(int offset, byte[] content)
        {
            // Create the stream with the required input
            var ms = new MemoryStream();
            for (int i = 0; i < offset; i++)
            {
                ms.WriteByte(0xFF);
            }

            ms.Write(content);
            ms.Seek(0, SeekOrigin.Begin);
            
            // Check that we get a match
            var rule = SkipperMatch.GetMatchingRule(ms, string.Empty);
            Assert.NotNull(rule);
            Assert.Equal("a7800", rule.SourceFile);
        }

        [Theory]
        [InlineData(new byte[] { 0x46, 0x44, 0x53, 0x1A, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })]
        [InlineData(new byte[] { 0x46, 0x44, 0x53, 0x1A, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })]
        [InlineData(new byte[] { 0x46, 0x44, 0x53, 0x1A, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })]
        [InlineData(new byte[] { 0x46, 0x44, 0x53, 0x1A, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })]
        
        public void FDSTest(byte[] content)
        {
            // Create the stream with the required input
            var ms = new MemoryStream();
            ms.Write(content);
            ms.Seek(0, SeekOrigin.Begin);
            
            // Check that we get a match
            var rule = SkipperMatch.GetMatchingRule(ms, string.Empty);
            Assert.NotNull(rule);
            Assert.Equal("fds", rule.SourceFile);
        }

        [Theory]
        [InlineData(0x00, new byte[] { 0x4C, 0x59, 0x4E, 0x58 })]
        [InlineData(0x06, new byte[] { 0x42, 0x53, 0x39 })]
        public void LynxTest(int offset, byte[] content)
        {
            // Create the stream with the required input
            var ms = new MemoryStream();
            for (int i = 0; i < offset; i++)
            {
                ms.WriteByte(0xFF);
            }

            ms.Write(content);
            ms.Seek(0, SeekOrigin.Begin);
            
            // Check that we get a match
            var rule = SkipperMatch.GetMatchingRule(ms, string.Empty);
            Assert.NotNull(rule);
            Assert.Equal("lynx", rule.SourceFile);
        }

        [Theory]
        [InlineData(new byte[] { 0x80, 0x37, 0x12, 0x40 })]
        [InlineData(new byte[] { 0x37, 0x80, 0x40, 0x12 })]
        [InlineData(new byte[] { 0x40, 0x12, 0x37, 0x80 })]
        public void N64Test(byte[] content)
        {
            // Create the stream with the required input
            var ms = new MemoryStream();
            ms.Write(content);
            ms.Seek(0, SeekOrigin.Begin);
            
            // Check that we get a match
            var rule = SkipperMatch.GetMatchingRule(ms, string.Empty);
            Assert.NotNull(rule);
            Assert.Equal("n64", rule.SourceFile);
        }

        [Fact]
        public void NESTest()
        {
            // Create the stream with the required input
            var ms = new MemoryStream();
            ms.Write(new byte[] { 0x4E, 0x45, 0x53, 0x1A });
            ms.Seek(0, SeekOrigin.Begin);
            
            // Check that we get a match
            var rule = SkipperMatch.GetMatchingRule(ms, string.Empty);
            Assert.NotNull(rule);
            Assert.Equal("nes", rule.SourceFile);
        }

        [Fact]
        public void PCETest()
        {
            // Create the stream with the required input
            var ms = new MemoryStream();
            ms.Write(new byte[] { 0x40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xAA, 0xBB, 0x02 });
            ms.Seek(0, SeekOrigin.Begin);
            
            // Check that we get a match
            var rule = SkipperMatch.GetMatchingRule(ms, string.Empty);
            Assert.NotNull(rule);
            Assert.Equal("pce", rule.SourceFile);
        }

        [Theory]
        [InlineData(new byte[] { 0x50, 0x53, 0x49, 0x44, 0x00, 0x01, 0x00, 0x76 })]
        [InlineData(new byte[] { 0x50, 0x53, 0x49, 0x44, 0x00, 0x03, 0x00, 0x7c })]
        [InlineData(new byte[] { 0x50, 0x53, 0x49, 0x44, 0x00, 0x02, 0x00, 0x7c })]
        [InlineData(new byte[] { 0x50, 0x53, 0x49, 0x44, 0x00, 0x01, 0x00, 0x7c })]
        [InlineData(new byte[] { 0x52, 0x53, 0x49, 0x44, 0x00, 0x02, 0x00, 0x7c })]
        public void PSIDTest(byte[] content)
        {
            // Create the stream with the required input
            var ms = new MemoryStream();
            ms.Write(content);
            ms.Seek(0, SeekOrigin.Begin);
            
            // Check that we get a match
            var rule = SkipperMatch.GetMatchingRule(ms, string.Empty);
            Assert.NotNull(rule);
            Assert.Equal("psid", rule.SourceFile);
        }

        [Theory]
        [InlineData(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 })]
        [InlineData(new byte[] { 0xAA, 0xBB, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00 })]
        [InlineData(new byte[] { 0x53, 0x55, 0x50, 0x45, 0x52, 0x55, 0x46, 0x4F })]
        public void SNESTest(byte[] content)
        {
            // Create the stream with the required input
            var ms = new MemoryStream();
            for (int i = 0; i < 0x16; i++)
            {
                ms.WriteByte(0xFF);
            }

            ms.Write(content);
            ms.Seek(0, SeekOrigin.Begin);
            
            // Check that we get a match
            var rule = SkipperMatch.GetMatchingRule(ms, string.Empty);
            Assert.NotNull(rule);
            Assert.Equal("snes", rule.SourceFile);
        }

        [Fact]
        public void SPCTest()
        {
            // Create the stream with the required input
            var ms = new MemoryStream();
            ms.Write(new byte[] { 0x53, 0x4E, 0x45, 0x53, 0x2D, 0x53, 0x50, 0x43 });
            ms.Seek(0, SeekOrigin.Begin);
            
            // Check that we get a match
            var rule = SkipperMatch.GetMatchingRule(ms, string.Empty);
            Assert.NotNull(rule);
            Assert.Equal("spc", rule.SourceFile);
        }
    }
}