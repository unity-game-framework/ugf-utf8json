using System;
using Utf8Json;

namespace UGF.Utf8Json.Runtime.Formatters.Union
{
    public interface IUnionFormatter
    {
        void AddFormatter(Type targetType, string typeIdentifier, IJsonFormatter formatter);
        void RemoveFormatter(Type targetType);
        void Clear();
        T GetFormatter<T>(Type targetType) where T : IJsonFormatter;
        bool TryGetFormatter<T>(Type targetType, out T formatter) where T : IJsonFormatter;
    }
}
