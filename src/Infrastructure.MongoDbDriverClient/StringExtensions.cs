namespace Cygnus.Infrastructure.MongoDbDriverClient
{
    public static class StringExtensions
    {
        public static string ToBooleanString(this string input)
        {
            if (input == null || !bool.TryParse(input, out var result))
            {
                return string.Empty;
            }

            return result ? "true" : "false";
        }
    }
}
