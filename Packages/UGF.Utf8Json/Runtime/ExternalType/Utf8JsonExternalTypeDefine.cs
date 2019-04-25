using System;
using System.Collections.Generic;
using Utf8Json;

namespace UGF.Utf8Json.Runtime.ExternalType
{
    public class Utf8JsonExternalTypeDefine : IUtf8JsonExternalTypeDefine
    {
        public Dictionary<Type, IJsonFormatter> Formatters { get; } = new Dictionary<Type, IJsonFormatter>();

        public void GetFormatters(IDictionary<Type, IJsonFormatter> formatters)
        {
            foreach (KeyValuePair<Type, IJsonFormatter> pair in Formatters)
            {
                formatters.Add(pair.Key, pair.Value);
            }
        }
    }
}
