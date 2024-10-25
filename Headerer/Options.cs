using System;
using System.Collections.Generic;

namespace Headerer
{
    internal sealed class Options
    {
        #region Properties

        /// <summary>
        /// Set of input paths to use for operations
        /// </summary>
        public List<string> InputPaths { get; private set; } = [];

        /// <summary>
        /// Represents the feature being called
        /// </summary>
        public Feature Feature { get; private set; } = Feature.NONE;

        /// <summary>
        /// Output debug statements to console
        /// </summary>
        public bool Debug { get; private set; } = false;

        /// <summary>
        /// Optional output directory
        /// </summary>
        public string? OutputDir { get; private set; }

        #region Extraction

        /// <summary>
        /// Disable storing copier headers on extract
        /// </summary>
        public bool NoStoreHeader { get; private set; }

        #endregion

        #endregion

        /// <summary>
        /// Parse commandline arguments into an Options object
        /// </summary>
        public static Options? ParseOptions(string[] args)
        {
            // If we have invalid arguments
            if (args == null || args.Length == 0)
                return null;

            // Create an Options object
            var options = new Options();

            // Get the first argument as a feature flag
            string featureName = args[0];
            switch (featureName)
            {
                case "ex":
                case "extract":
                    options.Feature = Feature.Extract;
                    break;

                case "re":
                case "restore":
                    options.Feature = Feature.Restore;
                    break;

                default:
                    Console.WriteLine($"{featureName} is not a recognized feature");
                    return null;
            }

            // Parse the options and paths
            int index = 1;
            for (; index < args.Length; index++)
            {
                string arg = args[index];
                switch (arg)
                {
                    case "-dbg":
                    case "--debug":
                        options.Debug = true;
                        break;

                    case "-o":
                    case "--outdir":
                        options.OutputDir = index + 1 < args.Length ? args[++index] : string.Empty;
                        break;

                    #region Extraction

                    case "-nsh":
                    case "--no-store-header":
                        options.NoStoreHeader = true;
                        break;

                    #endregion

                    default:
                        options.InputPaths.Add(arg);
                        break;
                }
            }

            // Validate we have any input paths to work on
            if (options.InputPaths.Count == 0)
            {
                Console.WriteLine("At least one path is required!");
                return null;
            }

            return options;
        }

        /// <summary>
        /// Display help text
        /// </summary>
        /// <param name="err">Additional error text to display, can be null to ignore</param>
        public static void DisplayHelp(string? err = null)
        {
            if (!string.IsNullOrEmpty(err))
                Console.WriteLine($"Error: {err}");

            Console.WriteLine("Headerer - Remove, store, and restore copier headers");
            Console.WriteLine();
            Console.WriteLine("Headerer.exe <features> <options> file|directory ...");
            Console.WriteLine();
            Console.WriteLine("Features:");
            Console.WriteLine("ex, extract              Extract and remove copier headers");
            Console.WriteLine("re, restore              Restore header to file based on SHA-1");
            Console.WriteLine();
            Console.WriteLine("Common options:");
            Console.WriteLine("-?, -h, --help           Display this help text and quit");
            Console.WriteLine("-dbg, --debug            Enable debug logging statements");
            Console.WriteLine("-o, --outdir [PATH]      Set output directory");
            Console.WriteLine();
            Console.WriteLine("Extraction options:");
            Console.WriteLine("-nsh, --no-store-header  Don't store the extracted header");
        }
    }
}