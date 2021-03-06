using System;
using System.Collections.Generic;
using UGF.Utf8Json.Runtime.Formatters.Typed;
using Utf8Json;

namespace UGF.Utf8Json.Runtime
{
    /// <summary>
    /// Represents default resolver with the formatters and nested resolvers.
    /// </summary>
    public class Utf8JsonFormatterResolver : IUtf8JsonFormatterResolver
    {
        public ITypedFormatterTypeProvider TypedFormatterTypeProvider { get; }

        /// <summary>
        /// Gets collection of the formatters stored by the target type.
        /// </summary>
        public IReadOnlyDictionary<Type, IJsonFormatter> Formatters { get { return m_formatters; } }

        /// <summary>
        /// Gets collection of the nested resolvers.
        /// </summary>
        public IReadOnlyList<IJsonFormatterResolver> Resolvers { get { return m_resolvers; } }

        private readonly Dictionary<Type, IJsonFormatter> m_formatters = new Dictionary<Type, IJsonFormatter>();
        private readonly List<IJsonFormatterResolver> m_resolvers = new List<IJsonFormatterResolver>();

        public Utf8JsonFormatterResolver(ITypedFormatterTypeProvider typedFormatterTypeProvider = null)
        {
            TypedFormatterTypeProvider = typedFormatterTypeProvider ?? new TypedFormatterTypeProvider();
        }

        public void AddFormatter<T>(IJsonFormatter<T> formatter)
        {
            AddFormatter(typeof(T), formatter);
        }

        public void AddFormatter(Type type, IJsonFormatter formatter)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (formatter == null) throw new ArgumentNullException(nameof(formatter));
            if (m_formatters.ContainsKey(type)) throw new ArgumentException($"The formatter by the specified type already has been added: '{type}', '{formatter}'.", nameof(type));

            m_formatters.Add(type, formatter);
        }

        public void RemoveFormatter(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            m_formatters.Remove(type);
        }

        public void AddResolver(IJsonFormatterResolver resolver)
        {
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));

            m_resolvers.Add(resolver);
        }

        public void RemoveResolver(IJsonFormatterResolver resolver)
        {
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));

            m_resolvers.Remove(resolver);
        }

        public bool TryGetFormatter<T>(out IJsonFormatter<T> formatter)
        {
            if (m_formatters.TryGetValue(typeof(T), out IJsonFormatter value) && value is IJsonFormatter<T> cast)
            {
                formatter = cast;
                return true;
            }

            if (TryCreateTypeFormatter(typeof(T), out TypedFormatter<T> typedFormatter))
            {
                formatter = typedFormatter;
                return true;
            }

            for (int i = 0; i < m_resolvers.Count; i++)
            {
                IJsonFormatterResolver resolver = m_resolvers[i];

                formatter = resolver.GetFormatter<T>();

                if (formatter != null)
                {
                    return true;
                }
            }

            formatter = null;
            return false;
        }

        public bool TryGetFormatter(Type type, out IJsonFormatter formatter)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (m_formatters.TryGetValue(type, out formatter))
            {
                return true;
            }

            if (TryCreateTypeFormatter(type, out TypedFormatter<object> typedFormatter))
            {
                formatter = typedFormatter;
                return true;
            }

            for (int i = 0; i < m_resolvers.Count; i++)
            {
                formatter = m_resolvers[i].GetFormatter(type);

                if (formatter != null)
                {
                    return true;
                }
            }

            return false;
        }

        public IJsonFormatter<T> GetFormatter<T>()
        {
            if (!TryGetFormatter(out IJsonFormatter<T> formatter))
            {
                throw new ArgumentException($"Formatter for specified type not found: '{typeof(T)}'.");
            }

            return formatter;
        }

        public IJsonFormatter GetFormatter(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!TryGetFormatter(type, out IJsonFormatter formatter))
            {
                throw new ArgumentException($"Formatter for specified type not found: '{type}'.");
            }

            return formatter;
        }

        private bool TryCreateTypeFormatter<T>(Type type, out TypedFormatter<T> formatter)
        {
            if (type.IsAbstract || type.IsInterface)
            {
                formatter = new TypedFormatter<T>(this, TypedFormatterTypeProvider);

                m_formatters.Add(type, formatter);

                return true;
            }

            formatter = null;
            return false;
        }
    }
}
