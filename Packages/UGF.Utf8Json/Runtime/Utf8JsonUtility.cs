using System;
using Utf8Json;
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

        public static string ToJson<T>(T target, IUtf8JsonFormatterResolver resolver, bool readable = false)
        {
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));

            if (readable)
            {
                byte[] bytes = JsonSerializer.Serialize(target, resolver);

                return JsonSerializer.PrettyPrint(bytes);
            }

            return JsonSerializer.ToJsonString(target, resolver);
        }

        public static T FromJson<T>(string text, IUtf8JsonFormatterResolver resolver)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentException("Value cannot be null or empty.", nameof(text));
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));

            return JsonSerializer.Deserialize<T>(text, resolver);
        }
    }
}
