using Utf8Json;

namespace UGF.Utf8Json.Runtime
{
    public static class Utf8JsonFormatterResolverCache<T>
    {
        private static IJsonFormatter<T> m_formatter;

        public static IJsonFormatter<T> GetFormatter(IUtf8JsonFormatterResolver resolver)
        {
            if (m_formatter == null && resolver.Formatters.TryGetValue(typeof(T), out IJsonFormatter formatter) && formatter is IJsonFormatter<T> formatterGeneric)
            {
                m_formatter = formatterGeneric;
            }

            return m_formatter;
        }
    }
}
