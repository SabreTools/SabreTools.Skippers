using System;
using System.IO;
using SabreTools.Hashing;
using SabreTools.IO.Extensions;
using SabreTools.Skippers;

namespace Headerer
{
    internal static class Extract
    {
        /// <summary>
        /// Detect header skipper compliance and create an output file
        /// </summary>
        /// <param name="file">Name of the file to be parsed</param>
        /// <param name="outDir">Output directory to write the file to, empty means the same directory as the input file</param>
        /// <param name="nostore">True if headers should not be stored in the database, false otherwise</param>
        /// <param name="debug">Enable additional log statements for debugging</param>
        /// <returns>True if the output file was created, false otherwise</returns>
        public static bool DetectTransformStore(string file, string? outDir, bool nostore, bool debug = false)
        {
            // Create the output directory if it doesn't exist
            if (!string.IsNullOrWhiteSpace(outDir) && !Directory.Exists(outDir))
                Directory.CreateDirectory(outDir);

            Console.WriteLine($"\nGetting skipper information for '{file}'");

            // Get the skipper rule that matches the file, if any
            SkipperMatch.Init();
            Rule rule = SkipperMatch.GetMatchingRule(file, string.Empty);

            // If we have an empty rule, return false
            if (rule.Tests == null || rule.Tests.Length == 0 || rule.Operation != HeaderSkipOperation.None)
                return false;

            Console.WriteLine("File has a valid copier header");

            // Get the header bytes from the file first
            string hstr;
            try
            {
                // Extract the header as a string for the database
                using var fs = File.OpenRead(file);
                int startOffset = int.Parse(rule.StartOffset ?? "0");
                byte[] hbin = new byte[startOffset];
                fs.Read(hbin, 0, startOffset);
                hstr = ByteArrayExtensions.ByteArrayToString(hbin)!;
            }
            catch
            {
                return false;
            }

            // Apply the rule to the file
            string newfile = string.IsNullOrWhiteSpace(outDir) ? Path.GetFullPath(file) + ".new" : Path.Combine(outDir, Path.GetFileName(file));
            rule.TransformFile(file, newfile);

            // If the output file doesn't exist, return false
            if (!File.Exists(newfile))
                return false;

            // Now add the information to the database if it's not already there
            if (!nostore)
            {
                string sha1 = HashTool.GetFileHash(newfile, HashType.SHA1) ?? string.Empty;
                Database.AddHeader(hstr, sha1, rule.SourceFile!, debug);
            }

            return true;
        }
    }
}
