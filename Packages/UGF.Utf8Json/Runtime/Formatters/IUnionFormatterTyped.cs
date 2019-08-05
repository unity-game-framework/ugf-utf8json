using System;

namespace UGF.Utf8Json.Runtime.Formatters
{
    public interface IUnionFormatterTyped
    {
        Type TargetType { get; }
        Type BaseType { get; }
    }
}
