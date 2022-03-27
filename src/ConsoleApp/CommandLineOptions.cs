namespace Cygnus.ConsoleApp
{
    internal class CommandLineOptions
    {
        [Value(0, MetaValue = "Action", Required = true, HelpText = "Action (possible values: \"check\", \"run\").")]
        public string Action { get; set; } = string.Empty;

        [Option('f', "file", Required = false, HelpText = "Synchronization definition file.")]
        public string DefinitionFile { get; set; } = string.Empty;

        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool IsVerbose { get; set; }

        [Option("help", Required = false, HelpText = "Display help message.")]
        public bool IsHelp { get; set; }

        [Option("version", Required = false, HelpText = "Display version.")]
        public bool IsVersion { get; set; }
    }
}
