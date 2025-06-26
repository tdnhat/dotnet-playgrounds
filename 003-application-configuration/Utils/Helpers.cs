namespace _003_application_configuration.Utils
{
    public static class Helpers
    {
        public static string MaskConnectionString(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                return connectionString;

            var sensitiveKeys = new[] { "password", "pwd", "user id", "uid", "username" };
            var result = connectionString;

            foreach (var key in sensitiveKeys)
            {
                // Pattern to match key=value; or key=value (end of string)
                var pattern = $@"({key}\s*=\s*)[^;]*";
                result = System.Text.RegularExpressions.Regex.Replace(
                    result,
                    pattern,
                    $"$1***",
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase
                );
            }

            return result;
        }
    }
}