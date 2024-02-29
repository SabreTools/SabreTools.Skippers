using System.IO;

using SabreTools.Skippers;
using Xunit;

namespace SabreTools.Test.Skippers
{
    [Collection("SkipperMatch")]
    public class RuleTransformTests
    {
        public RuleTransformTests()
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
            PadAndReset(ms);
            
            // Get a match
            var rule = SkipperMatch.GetMatchingRule(ms, string.Empty, true);
            ms.Seek(0, SeekOrigin.Begin);

            // Transform the stream
            var actual = new MemoryStream();
            rule.TransformStream(ms, actual, false, true);

            // Validate expected length
            Assert.Equal(1024 - 0x80, actual.Length);
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
            PadAndReset(ms);
            
            // Get a match
            var rule = SkipperMatch.GetMatchingRule(ms, string.Empty, true);
            ms.Seek(0, SeekOrigin.Begin);

            // Transform the stream
            var actual = new MemoryStream();
            rule.TransformStream(ms, actual, false, true);

            // Validate expected length
            Assert.Equal(1024 - 0x10, actual.Length);
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
            PadAndReset(ms);
            
            // Get a match
            var rule = SkipperMatch.GetMatchingRule(ms, string.Empty, true);
            ms.Seek(0, SeekOrigin.Begin);

            // Transform the stream
            var actual = new MemoryStream();
            rule.TransformStream(ms, actual, false, true);

            // Validate expected length
            Assert.Equal(1024 - 0x40, actual.Length);
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
            PadAndReset(ms);
            
            // Get a match
            var rule = SkipperMatch.GetMatchingRule(ms, string.Empty, true);
            ms.Seek(0, SeekOrigin.Begin);

            // Transform the stream
            var actual = new MemoryStream();
            rule.TransformStream(ms, actual, false, true);

            // Validate expected length
            Assert.Equal(1024, actual.Length);
        }

        [Fact]
        public void NESTest()
        {
            // Create the stream with the required input
            var ms = new MemoryStream();
            ms.Write(new byte[] { 0x4E, 0x45, 0x53, 0x1A });
            PadAndReset(ms);
            
            // Get a match
            var rule = SkipperMatch.GetMatchingRule(ms, string.Empty, true);
            ms.Seek(0, SeekOrigin.Begin);

            // Transform the stream
            var actual = new MemoryStream();
            rule.TransformStream(ms, actual, false, true);

            // Validate expected length
            Assert.Equal(1024 - 0x10, actual.Length);
        }

        [Fact]
        public void PCETest()
        {
            // Create the stream with the required input
            var ms = new MemoryStream();
            ms.Write(new byte[] { 0x40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xAA, 0xBB, 0x02 });
            PadAndReset(ms);
            
            // Get a match
            var rule = SkipperMatch.GetMatchingRule(ms, string.Empty, true);
            ms.Seek(0, SeekOrigin.Begin);

            // Transform the stream
            var actual = new MemoryStream();
            rule.TransformStream(ms, actual, false, true);

            // Validate expected length
            Assert.Equal(1024 - 0x200, actual.Length);
        }

        [Theory]
        [InlineData(new byte[] { 0x50, 0x53, 0x49, 0x44, 0x00, 0x01, 0x00, 0x76 }, 0x76)]
        [InlineData(new byte[] { 0x50, 0x53, 0x49, 0x44, 0x00, 0x03, 0x00, 0x7c }, 0x76)]
        [InlineData(new byte[] { 0x50, 0x53, 0x49, 0x44, 0x00, 0x02, 0x00, 0x7c }, 0x7c)]
        [InlineData(new byte[] { 0x50, 0x53, 0x49, 0x44, 0x00, 0x01, 0x00, 0x7c }, 0x7c)]
        [InlineData(new byte[] { 0x52, 0x53, 0x49, 0x44, 0x00, 0x02, 0x00, 0x7c }, 0x7c)]
        public void PSIDTest(byte[] content, int startOffset)
        {
            // Create the stream with the required input
            var ms = new MemoryStream();
            ms.Write(content);
            PadAndReset(ms);
            
            // Get a match
            var rule = SkipperMatch.GetMatchingRule(ms, string.Empty, true);
            ms.Seek(0, SeekOrigin.Begin);

            // Transform the stream
            var actual = new MemoryStream();
            rule.TransformStream(ms, actual, false, true);

            // Validate expected length
            Assert.Equal(1024 - startOffset, actual.Length);
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
            PadAndReset(ms);
            
            // Get a match
            var rule = SkipperMatch.GetMatchingRule(ms, string.Empty, true);
            ms.Seek(0, SeekOrigin.Begin);

            // Transform the stream
            var actual = new MemoryStream();
            rule.TransformStream(ms, actual, false, true);

            // Validate expected length
            Assert.Equal(1024 - 0x200, actual.Length);
        }

        [Fact]
        public void SPCTest()
        {
            // Create the stream with the required input
            var ms = new MemoryStream();
            ms.Write(new byte[] { 0x53, 0x4E, 0x45, 0x53, 0x2D, 0x53, 0x50, 0x43 });
            PadAndReset(ms);
            
            // Get a match
            var rule = SkipperMatch.GetMatchingRule(ms, string.Empty, true);
            ms.Seek(0, SeekOrigin.Begin);

            // Transform the stream
            var actual = new MemoryStream();
            rule.TransformStream(ms, actual, false, true);

            // Validate expected length
            Assert.Equal(1024 - 0x100, actual.Length);
        }

        /// <summary>
        /// Pad the stream to 1KiB then seek to beginning
        /// </summary>
        private static void PadAndReset(MemoryStream ms)
        {
            for (long i = ms.Length; i < 1024; i++)
            {
                ms.WriteByte(0xFF);
            }

            ms.Seek(0, SeekOrigin.Begin);
        }
    }
}