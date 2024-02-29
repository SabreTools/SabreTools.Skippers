using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace SabreTools.Skippers.Tests
{
    /// <summary>
    /// Test that uses a byte mask XOR against data
    /// </summary>
    [XmlType("xor")]
    public class XorTest : Test
    {
        #region Fields

        /// <summary>
        /// File offset to run the test
        /// </summary>
        /// <remarks>Either numeric or the literal "EOF"</remarks>
        [XmlAttribute("offset")]
        public string? Offset
        {
            get => _offset == null ? "EOF" : _offset.Value.ToString();
            set
            {
                if (value == null || value.ToLowerInvariant() == "eof")
                    _offset = null;
                else
                    _offset = Convert.ToInt64(value, fromBase: 16);
            }
        }

        /// <summary>
        /// Static value to be checked at the offset
        /// </summary>
        /// <remarks>Hex string representation of a byte array</remarks>
        [XmlAttribute("value")]
        public string? Value
        {
            get => _value == null ? string.Empty : BitConverter.ToString(_value).Replace("-", string.Empty);
            set => _value = ParseByteArrayFromHex(value);
        }

        /// <summary>
        /// Determines whether a pass or failure is expected
        /// </summary>
        [XmlAttribute("result")]
        public bool Result { get; set; } = true;

        /// <summary>
        /// Byte mask to be applied to the tested bytes
        /// </summary>
        /// <remarks>Hex string representation of a byte array</remarks>
        [XmlAttribute("mask")]
        public string? Mask
        {
            get => _mask == null ? string.Empty : BitConverter.ToString(_mask).Replace("-", string.Empty);
            set => _mask = ParseByteArrayFromHex(value);
        }

        #endregion

        #region Private instance variables

        /// <summary>
        /// File offset to run the test
        /// </summary>
        /// <remarks>null is EOF</remarks>
        private long? _offset;

        /// <summary>
        /// Static value to be checked at the offset
        /// </summary>
        private byte[]? _value;

        /// <summary>
        /// Byte mask to be applied to the tested bytes
        /// </summary>
        private byte[]? _mask;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public XorTest(string? offset, string? value, bool result, string? mask)
        {
            Offset = offset;
            Value = value;
            Result = result;
            Mask = mask;
        }

        /// <inheritdoc/>
        public override bool Passes(Stream input)
        {
            // If we have an invalid mask
            if (_mask == null || _mask.Length == 0)
                return false;

            // If we have an invalid value
            if (_value == null || _value.Length == 0)
                return false;

            // Seek to the correct position, if possible
            if (!Seek(input, _offset))
                return false;

            bool result = true;
            try
            {
                // Then apply the mask if it exists
                byte[] read = new byte[_mask.Length];
                input.Read(read, 0, _mask.Length);

                byte[] masked = new byte[_mask.Length];
                for (int i = 0; i < read.Length; i++)
                {
                    masked[i] = (byte)(read[i] ^ _mask[i]);
                }

                // Finally, compare it against the value
                for (int i = 0; i < _value.Length; i++)
                {
                    if (masked[i] != _value[i])
                    {
                        result = false;
                        break;
                    }
                }
            }
            catch
            {
                result = false;
            }

            // Return if the expected and actual results match
            return result == Result;
        }
    }
}