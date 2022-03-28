namespace Cygnus.ConsoleApp
{
    internal class CommandLineOptions
    {
        [Value(0, MetaValue = "Action", Required = true, HelpText = "Action (possible values: \"check\", \"run\").")]
        public string Action { get; set; } = string.Empty;

        [Option('c', "config", Required = false, HelpText = "Configuration file.")]
        public string ConfigurationFile { get; set; } = string.Empty;

        [Option('f', "file", Required = false, HelpText = "Synchronization definition file.")]
        public string DefinitionFile { get; set; } = string.Empty;

        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool IsVerbose { get; set; }
    }
}
