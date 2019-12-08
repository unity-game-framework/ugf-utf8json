using System;

namespace UGF.Utf8Json.Runtime.Formatters.Typed
{
    public interface ITypedFormatterTypeProvider
    {
        bool TryGetTypeName(Type type, out byte[] typeName);
        bool TryGetType(ArraySegment<byte> typeName, out Type type);
    }
}
