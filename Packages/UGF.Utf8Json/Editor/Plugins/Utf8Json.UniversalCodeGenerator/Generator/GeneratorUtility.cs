namespace Utf8Json.CodeGenerator.Generator
{
    internal static class GeneratorUtility
    {
        public static string ToCamelCase(string s)
        {
            if (string.IsNullOrEmpty(s) || char.IsLower(s, 0))
            {
                return s;
            }

            char[] array = s.ToCharArray();

            array[0] = char.ToLowerInvariant(array[0]);

            return new string(array);
        }
    }
}
