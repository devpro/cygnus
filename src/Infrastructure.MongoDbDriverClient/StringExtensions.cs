namespace Cygnus.Infrastructure.MongoDbDriverClient
{
    internal static class StringExtensions
    {
        internal static string ToBooleanString(this string input)
        {
            if (input == null || !bool.TryParse(input, out var result))
            {
                return string.Empty;
            }

            return result ? "true" : "false";
        }
    }
}
