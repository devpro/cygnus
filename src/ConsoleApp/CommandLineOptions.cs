namespace Cygnus.ConsoleApp
{
    internal class CommandLineOptions
    {
        [Option('f', "file", Required = true, HelpText = "Synchronization definition file.")]
        public string DefinitionFile { get; set; } = string.Empty;

        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool IsVerbose { get; set; }
    }
}
