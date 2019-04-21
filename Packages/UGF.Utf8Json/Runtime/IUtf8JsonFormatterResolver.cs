using System;
using System.Collections.Generic;
using Utf8Json;

namespace UGF.Utf8Json.Runtime
{
    public interface IUtf8JsonFormatterResolver
    {
        IReadOnlyDictionary<Type, IJsonFormatter> Formatters { get; }

        void Add(Type type, IJsonFormatter formatter);
        void Remove(Type type);
        void Clear();
    }
}
