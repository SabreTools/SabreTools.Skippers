using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace SabreTools.Skippers.Tests
{
    /// <summary>
    /// Test that checks data matches
    /// </summary>
    [XmlType("data")]
    public class DataTest : Test
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

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public DataTest(string? offset, string? value, bool result)
        {
            Offset = offset;
            Value = value;
            Result = result;
        }

        /// <inheritdoc/>
        public override bool Passes(Stream input)
        {
            // If we have an invalid value
            if (_value == null || _value.Length == 0)
                return false;

            // Seek to the correct position, if possible
            if (!Seek(input, _offset))
                return false;

            // Then read and compare bytewise
            bool result = true;
            for (int i = 0; i < _value.Length; i++)
            {
                try
                {
                    if (input.ReadByte() != _value[i])
                    {
                        result = false;
                        break;
                    }
                }
                catch
                {
                    result = false;
                    break;
                }
            }

            // Return if the expected and actual results match
            return result == Result;
        }
    }
}