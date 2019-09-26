using System;
using Utf8Json;

namespace UGF.Utf8Json.Runtime.Formatters.Union
{
    public interface IUnionFormatter
    {
        void AddFormatter(Type targetType, string typeIdentifier, IJsonFormatter formatter);
        void RemoveFormatter(Type targetType);
        void Clear();
        IJsonFormatter<T> GetFormatter<T>(Type targetType);
        bool TryGetFormatter<T>(Type targetType, out IJsonFormatter<T> formatter);
    }
}
