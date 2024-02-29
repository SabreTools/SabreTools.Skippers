using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace SabreTools.Skippers.Tests
{
    /// <summary>
    /// Test that tests file size
    /// </summary>
    [XmlType("file")]
    public class FileTest : Test
    {
        #region Fields

        /// <summary>
        /// Determines whether a pass or failure is expected
        /// </summary>
        [XmlAttribute("result")]
        public bool Result { get; set; } = true;

        /// <summary>
        /// Expected size of the input byte array, used with the Operator
        /// </summary>
        /// <remarks>Either numeric or the literal "po2"</remarks>
        [XmlAttribute("size")]
        public string? Size
        {
            get => _size == null ? "po2" : _size.Value.ToString();
            set
            {
                if (value == null || value.ToLowerInvariant() == "po2")
                    _size = null;
                else
                    _size = Convert.ToInt64(value, fromBase: 16);
            }
        }

        /// <summary>
        /// Expected range value for the input byte array size, used with Size
        /// </summary>
        [XmlAttribute("operator")]
        public HeaderSkipTestFileOperator Operator { get; set; }

        #endregion

        #region Private instance variables

        /// <summary>
        /// File offset to run the test
        /// </summary>
        /// <remarks>null is PO2 ("power of 2" filesize)</remarks>
        private long? _size;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public FileTest(bool result, string? size, HeaderSkipTestFileOperator opr)
        {
            Result = result;
            Size = size;
            Operator = opr;
        }

        /// <inheritdoc/>
        public override bool Passes(Stream input)
        {
            // First get the file size from stream
            long size = input.Length;

            // If we have a null size, check that the size is a power of 2
            bool result = true;
            if (_size == null)
            {
                // http://stackoverflow.com/questions/600293/how-to-check-if-a-number-is-a-power-of-2
                result = (((ulong)size & ((ulong)size - 1)) == 0);
            }
            else if (Operator == HeaderSkipTestFileOperator.Less)
            {
                result = (size < _size);
            }
            else if (Operator == HeaderSkipTestFileOperator.Greater)
            {
                result = (size > _size);
            }
            else if (Operator == HeaderSkipTestFileOperator.Equal)
            {
                result = (size == _size);
            }

            // Return if the expected and actual results match
            return result == Result;
        }
    }
}