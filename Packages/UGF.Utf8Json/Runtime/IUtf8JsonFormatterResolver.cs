using System;
using System.Collections.Generic;
using Utf8Json;

namespace UGF.Utf8Json.Runtime
{
    public interface IUtf8JsonFormatterResolver : IJsonFormatterResolver
    {
        IReadOnlyDictionary<Type, IJsonFormatter> Formatters { get; }
        IReadOnlyList<IJsonFormatterResolver> Resolvers { get; }
    }
}
