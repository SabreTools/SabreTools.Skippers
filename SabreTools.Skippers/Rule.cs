using System;
using System.IO;
using System.Xml.Serialization;
using SabreTools.Skippers.Tests;

namespace SabreTools.Skippers
{
    [XmlType("rule")]
    public class Rule
    {
        #region Fields

        /// <summary>
        /// Starting offset for applying rule
        /// </summary>
        /// <remarks>Either numeric or the literal "EOF"</remarks>
        [XmlAttribute("start_offset")]
        public string? StartOffset
        {
            get => _startOffset == null ? "EOF" : _startOffset.Value.ToString();
            set
            {
                if (value == null || value.Equals("eof", StringComparison.InvariantCultureIgnoreCase))
                    _startOffset = null;
                else
                    _startOffset = Convert.ToInt64(value, fromBase: 16);
            }
        }

        /// <summary>
        /// Ending offset for applying rule
        /// </summary>
        /// <remarks>Either numeric or the literal "EOF"</remarks>
        [XmlAttribute("end_offset")]
        public string? EndOffset
        {
            get => _endOffset == null ? "EOF" : _endOffset.Value.ToString();
            set
            {
                if (value == null || value.Equals("eof", StringComparison.InvariantCultureIgnoreCase))
                    _endOffset = null;
                else
                    _endOffset = Convert.ToInt64(value, fromBase: 16);
            }
        }

        /// <summary>
        /// Byte manipulation operation
        /// </summary>
        [XmlAttribute("operation")]
        public HeaderSkipOperation Operation { get; set; }

        /// <summary>
        /// List of matching tests in a rule
        /// </summary>
        [XmlElement("and", typeof(AndTest))]
        [XmlElement("data", typeof(DataTest))]
        [XmlElement("file", typeof(FileTest))]
        [XmlElement("or", typeof(OrTest))]
        [XmlElement("xor", typeof(XorTest))]
        public Test[]? Tests { get; set; }

        /// <summary>
        /// Filename the skipper rule lives in
        /// </summary>
        [XmlIgnore]
        public string? SourceFile { get; set; }

        #endregion

        #region Private instance variables

        /// <summary>
        /// Starting offset for applying rule
        /// </summary>
        /// <remarks>null is EOF</remarks>
        private long? _startOffset = null;

        /// <summary>
        /// Ending offset for applying rule
        /// </summary>
        /// <remarks>null is EOF</remarks>
        private long? _endOffset = null;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public Rule(string? startOffset, string? endOffset, HeaderSkipOperation operation, Test[]? tests, string? sourceFile)
        {
            StartOffset = startOffset;
            EndOffset = endOffset;
            Operation = operation;
            Tests = tests;
            SourceFile = sourceFile;
        }

        /// <summary>
        /// Check if a Stream passes all tests in the Rule
        /// </summary>
        /// <param name="input">Stream to check</param>
        /// <returns>True if all tests passed, false otherwise</returns>
        public bool PassesAllTests(Stream input)
        {
            bool success = true;

            // If there are no tests
            if (Tests == null || Tests.Length == 0)
                return success;

            foreach (Test test in Tests)
            {
                bool result = test.Passes(input);
                success &= result;
            }

            return success;
        }

        /// <summary>
        /// Transform an input file using the given rule
        /// </summary>
        /// <param name="input">Input file name</param>
        /// <param name="output">Output file name</param>
        /// <returns>True if the file was transformed properly, false otherwise</returns>
        public bool TransformFile(string input, string output)
        {
            // If the input file doesn't exist
            if (string.IsNullOrEmpty(input) || !File.Exists(input))
                return false;

            // If we have an invalid output directory name
            if (string.IsNullOrEmpty(output))
                return false;

            // Create the output directory if it doesn't already
            string parentDirectory = Path.GetDirectoryName(output) ?? string.Empty;
            Directory.CreateDirectory(parentDirectory);

            //logger.User($"Attempting to apply rule to '{input}'");
            bool success = TransformStream(File.OpenRead(input), File.Create(output));

            // If the output file has size 0, delete it
            if (new FileInfo(output).Length == 0)
            {
                File.Delete(output);
                success = false;
            }

            return success;
        }

        /// <summary>
        /// Transform an input stream using the given rule
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="output">Output stream</param>
        /// <param name="keepReadOpen">True if the underlying read stream should be kept open, false otherwise</param>
        /// <param name="keepWriteOpen">True if the underlying write stream should be kept open, false otherwise</param>
        /// <returns>True if the file was transformed properly, false otherwise</returns>
        public bool TransformStream(Stream? input, Stream output, bool keepReadOpen = false, bool keepWriteOpen = false)
        {
            bool success = true;

            // If the input stream isn't valid
            if (input == null || !input.CanRead)
                return false;

            // If the sizes are wrong for the values, fail
            long extsize = input.Length;
            if ((Operation > HeaderSkipOperation.Bitswap && (extsize % 2) != 0)
                || (Operation > HeaderSkipOperation.Byteswap && (extsize % 4) != 0)
                || (Operation > HeaderSkipOperation.Bitswap && (_startOffset == null || _startOffset % 2 != 0)))
            {
                return false;
            }

            // Now read the proper part of the file and apply the rule
            BinaryWriter? bw = null;
            BinaryReader? br = null;
            try
            {
                bw = new BinaryWriter(output);
                br = new BinaryReader(input);

                // Seek to the beginning offset
                if (_startOffset == null)
                    success = false;

                else if (Math.Abs((long)_startOffset) > input.Length)
                    success = false;

                else if (_startOffset > 0)
                    input.Seek((long)_startOffset, SeekOrigin.Begin);

                else if (_startOffset < 0)
                    input.Seek((long)_startOffset, SeekOrigin.End);

                // Then read and apply the operation as you go
                if (success)
                {
                    byte[] buffer = new byte[4];
                    int pos = 0;
                    while (input.Position < (_endOffset ?? input.Length)
                        && input.Position < input.Length)
                    {
                        byte b = br.ReadByte();
                        switch (Operation)
                        {
                            case HeaderSkipOperation.Bitswap:
                                // http://stackoverflow.com/questions/3587826/is-there-a-built-in-function-to-reverse-bit-order
                                uint r = b;
                                int s = 7;
                                for (b >>= 1; b != 0; b >>= 1)
                                {
                                    r <<= 1;
                                    r |= (byte)(b & 1);
                                    s--;
                                }
                                r <<= s;
                                buffer[pos] = (byte)r;
                                break;

                            case HeaderSkipOperation.Byteswap:
                                if (pos % 2 == 1)
                                    buffer[pos - 1] = b;
                                else
                                    buffer[pos + 1] = b;

                                break;

                            case HeaderSkipOperation.Wordswap:
                                buffer[3 - pos] = b;
                                break;

                            case HeaderSkipOperation.WordByteswap:
                                buffer[(pos + 2) % 4] = b;
                                break;

                            case HeaderSkipOperation.None:
                            default:
                                buffer[pos] = b;
                                break;
                        }

                        // Set the buffer position to default write to
                        pos = (pos + 1) % 4;

                        // If we filled a buffer, flush to the stream
                        if (pos == 0)
                        {
                            bw.Write(buffer);
                            bw.Flush();
                            buffer = new byte[4];
                        }
                    }

                    // If there's anything more in the buffer, write only the left bits
                    for (int i = 0; i < pos; i++)
                    {
                        bw.Write(buffer[i]);
                    }
                }
            }
            catch
            {
                return false;
            }
            finally
            {
#if NET40_OR_GREATER
                // If we're not keeping the read stream open, dispose of the binary reader
                if (!keepReadOpen)
                    br?.Dispose();

                // If we're not keeping the write stream open, dispose of the binary reader
                if (!keepWriteOpen)
                    bw?.Dispose();
#endif
            }

            return success;
        }
    }
}
