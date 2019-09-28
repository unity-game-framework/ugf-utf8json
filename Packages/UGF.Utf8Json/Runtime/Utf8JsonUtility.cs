using Utf8Json.Resolvers;
using Utf8Json.Unity;

namespace UGF.Utf8Json.Runtime
{
    /// <summary>
    /// Provides utilities to work with Utf8 Json serialization.
    /// </summary>
    public static class Utf8JsonUtility
    {
        /// <summary>
        /// Creates default resolver with Unity and Builtin resolvers.
        /// </summary>
        public static Utf8JsonFormatterResolver CreateDefaultResolver()
        {
            var resolver = new Utf8JsonFormatterResolver();

            resolver.AddResolver(UnityResolver.Instance);
            resolver.AddResolver(BuiltinResolver.Instance);

            return resolver;
        }
    }
}
