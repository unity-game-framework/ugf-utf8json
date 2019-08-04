using System;
using System.Reflection;
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

            resolver.Resolvers.Add(UnityResolver.Instance);
            resolver.Resolvers.Add(BuiltinResolver.Instance);

            return resolver;
        }

        /// <summary>
        /// Sets the specified formatter to the static formatter cache by the specified target type.
        /// </summary>
        /// <param name="targetType">The target of the formatter.</param>
        /// <param name="formatter">The formatter to set. (Can be Null)</param>
        public static void SetFormatterCache(Type targetType, IJsonFormatter formatter)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            Type type = typeof(Utf8JsonFormatterCache<>).MakeGenericType(targetType);
            FieldInfo field = type.GetField("Formatter");

            field.SetValue(null, formatter);
        }

        /// <summary>
        /// Tries to get formatter for the specified target type from the static formatter cache.
        /// </summary>
        /// <param name="targetType">The target of the formatter.</param>
        /// <param name="formatter">The cached formatter.</param>
        public static bool TryGetFormatterCache(Type targetType, out IJsonFormatter formatter)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            Type type = typeof(Utf8JsonFormatterCache<>).MakeGenericType(targetType);
            FieldInfo field = type.GetField("Formatter");
            object value = field.GetValue(null);

            if (value != null && value is IJsonFormatter valueFormatter)
            {
                formatter = valueFormatter;
                return true;
            }

            formatter = null;
            return false;
        }
    }
}
