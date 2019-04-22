using System;
using System.Collections.Generic;
using System.Reflection;
using UGF.Assemblies.Runtime;
using UGF.Types.Runtime;
using UGF.Utf8Json.Runtime.Resolvers.Unity;
using Utf8Json;
using Utf8Json.Resolvers;

namespace UGF.Utf8Json.Runtime
{
    public static class Utf8JsonUtility
    {
        public static Utf8JsonFormatterResolver CreateDefaultResolver()
        {
            var resolver = new Utf8JsonFormatterResolver();

            CreateFormatters(resolver.Formatters);

            resolver.Resolvers.Add(UnityResolver.Instance);
            resolver.Resolvers.Add(BuiltinResolver.Instance);

            return resolver;
        }

        public static void CreateFormatters(IDictionary<Type, IJsonFormatter> formatters, Assembly assembly = null)
        {
            foreach (Type type in AssemblyUtility.GetBrowsableTypes<Utf8JsonFormatterAttribute>(assembly))
            {
                var attribute = type.GetCustomAttribute<Utf8JsonFormatterAttribute>();

                if (attribute != null && TypesUtility.TryCreateType(type, out IJsonFormatter formatter))
                {
                    formatters.Add(attribute.TargetType, formatter);
                }
            }
        }

        public static void SetFormatterCache(IJsonFormatter formatter, Type targetType)
        {
            Type type = typeof(Utf8JsonFormatterCache<>).MakeGenericType(targetType);
            FieldInfo field = type.GetField("Formatter");

            field.SetValue(null, formatter);
        }

        public static bool TryGetFormatterCache(Type targetType, out IJsonFormatter formatter)
        {
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
