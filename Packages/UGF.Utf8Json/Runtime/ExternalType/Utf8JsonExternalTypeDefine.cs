using System;
using System.Collections.Generic;
using Utf8Json;

namespace UGF.Utf8Json.Runtime.ExternalType
{
    /// <summary>
    /// Represents define that stores formatters for the external types.
    /// </summary>
    public class Utf8JsonExternalTypeDefine : IUtf8JsonExternalTypeDefine
    {
        /// <summary>
        /// Gets collection of the formatters stored by the target type.
        /// </summary>
        public Dictionary<Type, IJsonFormatter> Formatters { get; } = new Dictionary<Type, IJsonFormatter>();

        public void GetFormatters(IDictionary<Type, IJsonFormatter> formatters)
        {
            if (formatters == null) throw new ArgumentNullException(nameof(formatters));

            foreach (KeyValuePair<Type, IJsonFormatter> pair in Formatters)
            {
                formatters.Add(pair.Key, pair.Value);
            }
        }
    }
}
