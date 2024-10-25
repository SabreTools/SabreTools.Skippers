using System;
using System.Collections.Generic;
using System.IO;
using SabreTools.Hashing;
using SabreTools.IO.Extensions;

namespace Headerer
{
    internal static class Restore
    {
        /// <summary>
        /// Detect and replace header(s) to the given file
        /// </summary>
        /// <param name="file">Name of the file to be parsed</param>
        /// <param name="outDir">Output directory to write the file to, empty means the same directory as the input file</param>
        /// <param name="debug">Enable additional log statements for debugging</param>
        /// <returns>True if a header was found and appended, false otherwise</returns>
        public static bool RestoreHeader(string file, string? outDir, bool debug = false)
        {
            // Create the output directory if it doesn't exist
            if (!string.IsNullOrWhiteSpace(outDir) && !Directory.Exists(outDir))
                Directory.CreateDirectory(outDir);

            // First, get the SHA-1 hash of the file
            string sha1 = HashTool.GetFileHash(file, HashType.SHA1) ?? string.Empty;

            // Retrieve a list of all related headers from the database
            List<string> headers = Database.RetrieveHeaders(sha1, debug);

            // If we have nothing retrieved, we return false
            if (headers.Count == 0)
                return false;

            // Now loop through and create the reheadered files, if possible
            for (int i = 0; i < headers.Count; i++)
            {
                string outputFile = (string.IsNullOrWhiteSpace(outDir) ? $"{Path.GetFullPath(file)}.new" : Path.Combine(outDir, Path.GetFileName(file))) + i;
                Console.WriteLine($"Creating reheadered file: {outputFile}");
                AppendBytes(file, outputFile, ByteArrayExtensions.StringToByteArray(headers[i]), null);
                Console.WriteLine("Reheadered file created!");
            }

            return true;
        }

        /// <summary>
        /// Add an aribtrary number of bytes to the inputted file
        /// </summary>
        /// <param name="input">File to be appended to</param>
        /// <param name="output">Outputted file</param>
        /// <param name="bytesToAddToHead">Bytes to be added to head of file</param>
        /// <param name="bytesToAddToTail">Bytes to be added to tail of file</param>
        private static void AppendBytes(string input, string output, byte[]? bytesToAddToHead, byte[]? bytesToAddToTail)
        {
            // If any of the inputs are invalid, skip
            if (!File.Exists(input))
                return;

            using FileStream fsr = File.OpenRead(input);
            using FileStream fsw = File.OpenWrite(output);

            AppendBytes(fsr, fsw, bytesToAddToHead, bytesToAddToTail);
        }

        /// <summary>
        /// Add an aribtrary number of bytes to the inputted stream
        /// </summary>
        /// <param name="input">Stream to be appended to</param>
        /// <param name="output">Outputted stream</param>
        /// <param name="bytesToAddToHead">Bytes to be added to head of stream</param>
        /// <param name="bytesToAddToTail">Bytes to be added to tail of stream</param>
        private static void AppendBytes(Stream input, Stream output, byte[]? bytesToAddToHead, byte[]? bytesToAddToTail)
        {
            // Write out prepended bytes
            if (bytesToAddToHead != null && bytesToAddToHead.Length > 0)
                output.Write(bytesToAddToHead, 0, bytesToAddToHead.Length);

            // Now copy the existing file over
            input.CopyTo(output);

            // Write out appended bytes
            if (bytesToAddToTail != null && bytesToAddToTail.Length > 0)
                output.Write(bytesToAddToTail, 0, bytesToAddToTail.Length);
        }
    }
}
