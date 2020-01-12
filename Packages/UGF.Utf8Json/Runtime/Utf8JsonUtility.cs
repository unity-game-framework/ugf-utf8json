using System;
using Utf8Json;

namespace UGF.Utf8Json.Runtime
{
    /// <summary>
    /// Provides utilities to work with Utf8 Json serialization.
    /// </summary>
    public static partial class Utf8JsonUtility
    {
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
