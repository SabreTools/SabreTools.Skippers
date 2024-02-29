using System;
using System.Globalization;
using System.IO;

namespace SabreTools.Skippers
{
    /// <summary>
    /// Individual test that applies to a Rule
    /// </summary>
    public abstract class Test
    {
        /// <summary>
        /// Check if a stream passes the test
        /// </summary>
        /// <param name="input">Stream to check rule against</param>
        /// <remarks>The Stream is assumed to be in the proper position for a given test</remarks>
        public abstract bool Passes(Stream input);

        #region Helpers

        /// <summary>
        /// Prase a hex string into a byte array
        /// </summary>
        /// <see href="http://stackoverflow.com/questions/321370/how-can-i-convert-a-hex-string-to-a-byte-array"/>
        protected static byte[]? ParseByteArrayFromHex(string? hex)
        {
            // If we have an invalid string
            if (string.IsNullOrEmpty(hex))
                return null;

            var ret = new byte[hex!.Length / 2];
            for (int index = 0; index < ret.Length; index++)
            {
                string byteValue = hex.Substring(index * 2, 2);
                ret[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return ret;
        }

        /// <summary>
        /// Seek an input stream based on the test value
        /// </summary>
        /// <param name="input">Stream to seek</param>
        /// <param name="offset">Offset to seek to</param>
        /// <returns>True if the stream could seek, false on error</returns>
        protected static bool Seek(Stream input, long? offset)
        {
            try
            {
                // Null offset means EOF
                if (offset == null)
                    input.Seek(0, SeekOrigin.End);

                // Positive offset means from beginning
                else if (offset >= 0 && offset <= input.Length)
                    input.Seek(offset.Value, SeekOrigin.Begin);

                // Negative offset means from end
                else if (offset < 0 && Math.Abs(offset.Value) <= input.Length)
                    input.Seek(offset.Value, SeekOrigin.End);

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}