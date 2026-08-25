using System;
using System.IO;

namespace Headerer
{
    public class Program
    {
        /// <summary>
        /// Entry point for the Headerer application
        /// </summary>
        /// <param name="args">String array representing command line parameters</param>
        public static void Main(string[] args)
        {
            // Validate the arguments
            if (args.Length == 0)
            {
                Options.DisplayHelp("One input file path required");
                return;
            }

            // Get the options from the arguments
            var options = Options.ParseOptions(args);

            // If we have an invalid state
            if (options is null)
            {
                Options.DisplayHelp();
                return;
            }

            // Loop through the input paths
            foreach (string inputPath in options.InputPaths)
            {
                if (File.Exists(inputPath))
                {
                    ProcessFilePath(inputPath, options);
                }
                else if (Directory.Exists(inputPath))
                {
                    foreach (var file in Directory.GetFiles(inputPath, "*", SearchOption.AllDirectories))
                    {
                        ProcessFilePath(file, options);
                    }
                }
                else
                {
                    Console.WriteLine($"{inputPath} is not a file or directory!");
                }
            }
        }

        /// <summary>
        /// Process a single file path using the provided options
        /// </summary>
        private static void ProcessFilePath(string path, Options options)
        {
            // TODO: Do something with the output success flags
            switch (options.Feature)
            {
                case Feature.Extract:
                    _ = Extract.DetectTransformStore(path, options.OutputDir, options.NoStoreHeader);
                    break;

                case Feature.Restore:
                    _ = Restore.RestoreHeader(path, options.OutputDir);
                    break;

                case Feature.NONE:
                default:
                    break;
            }
        }
    }
}
