using System;
using System.Collections.Generic;
using Utf8Json;

namespace UGF.Utf8Json.Runtime.ExternalType
{
    public interface IUtf8JsonExternalTypeDefine
    {
        void GetFormatters(IDictionary<Type, IJsonFormatter> formatters);
    }
}
