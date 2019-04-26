using System;
using System.Collections.Generic;
using System.Reflection;
using UGF.Assemblies.Runtime;
using UGF.Types.Runtime;
using UGF.Utf8Json.Runtime.ExternalType;
using UGF.Utf8Json.Runtime.Resolvers.Unity;
using Utf8Json;
using Utf8Json.Resolvers;

namespace UGF.Utf8Json.Runtime
{
    /// <summary>
    /// Provides utilities to work with Utf8 Json serialization.
    /// </summary>
    public static class Utf8JsonUtility
    {
        public static Utf8JsonFormatterResolver CreateDefaultResolver(bool includeExternal = true, bool includeExternalDefines = true, Assembly assembly = null)
        {
            var resolver = new Utf8JsonFormatterResolver();

            if (includeExternal)
            {
                GetFormatters(resolver.Formatters, assembly);
            }

            if (includeExternalDefines)
            {
                Utf8JsonExternalTypeUtility.GetFormatters(resolver.Formatters, assembly);
            }

            resolver.Resolvers.Add(UnityResolver.Instance);
            resolver.Resolvers.Add(BuiltinResolver.Instance);

            return resolver;
        }

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

        public static void SetFormatterCache(Type targetType, IJsonFormatter formatter)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));

            Type type = typeof(Utf8JsonFormatterCache<>).MakeGenericType(targetType);
            FieldInfo field = type.GetField("Formatter");

            field.SetValue(null, formatter);
        }

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
