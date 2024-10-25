namespace Headerer
{
    public class Program
    {
        /// <summary>
        /// Entry point for the SabreTools application
        /// </summary>
        /// <param name="args">String array representing command line parameters</param>
        public static void Main(string[] args)
        {
            // Validate the arguments
            if (args == null || args.Length == 0)
            {
                Options.DisplayHelp("One input file path required");
                return;
            }

            // Get the options from the arguments
            var options = Options.ParseOptions(args);

            // If we have an invalid state
            if (options == null)
            {
                Options.DisplayHelp();
                return;
            }

            // Loop through the input paths
            foreach (string inputPath in options.InputPaths)
            {
                // TODO: Do something with the output success flags
                switch (options.Feature)
                {
                    case Feature.Extract:
                        _ = Extract.DetectTransformStore(inputPath, options.OutputDir, options.NoStoreHeader);
                        break;

                    case Feature.Restore:
                        _ = Restore.RestoreHeader(inputPath, options.OutputDir);
                        break;
                }
            }
        }
    }
}
