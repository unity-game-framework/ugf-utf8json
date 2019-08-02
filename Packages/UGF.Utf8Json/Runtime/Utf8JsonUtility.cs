using System;
using System.Collections.Generic;
using System.Reflection;
using UGF.Assemblies.Runtime;
using UGF.Types.Runtime;
using UGF.Utf8Json.Runtime.ExternalType;
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
        /// Creates default resolver with Unity, Enum and Builtin resolvers, also with found formatters.
        /// </summary>
        /// <param name="includeFormatters">The value determines whether to include formatters marked with <see cref="Utf8JsonFormatterAttribute"/>.</param>
        /// <param name="includeExternalDefines">The value determines whether to include formatters from the external type defines.</param>
        /// <param name="assembly">The assembly to search for formatters.</param>
        public static Utf8JsonFormatterResolver CreateDefaultResolver(bool includeFormatters = true, bool includeExternalDefines = true, Assembly assembly = null)
        {
            var resolver = new Utf8JsonFormatterResolver();

            if (includeFormatters)
            {
                GetFormatters(resolver.Formatters, assembly);
            }

            if (includeExternalDefines)
            {
                Utf8JsonExternalTypeUtility.GetFormatters(resolver.Formatters, assembly);
            }

            resolver.Resolvers.Add(UnityResolver.Instance);
            resolver.Resolvers.Add(EnumResolver.UnderlyingValue);
            resolver.Resolvers.Add(BuiltinResolver.Instance);

            return resolver;
        }

        /// <summary>
        /// Gets collection of the formatters that marked with <see cref="Utf8JsonFormatterAttribute"/>.
        /// </summary>
        /// <param name="formatters">The collection to add found formatters to.</param>
        /// <param name="assembly">The assembly to search.</param>
        public static void GetFormatters(IDictionary<Type, IJsonFormatter> formatters, Assembly assembly = null)
        {
            if (formatters == null) throw new ArgumentNullException(nameof(formatters));

            foreach (Type type in AssemblyUtility.GetBrowsableTypes<Utf8JsonFormatterAttribute>(assembly))
            {
                var attribute = type.GetCustomAttribute<Utf8JsonFormatterAttribute>();

                if (attribute != null && TypesUtility.TryCreateType(type, out IJsonFormatter formatter))
                {
                    formatters.Add(attribute.TargetType, formatter);
                }
            }
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
