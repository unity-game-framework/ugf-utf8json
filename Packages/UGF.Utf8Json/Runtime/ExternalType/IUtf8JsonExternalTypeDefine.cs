using System;
using System.Collections.Generic;

namespace UGF.Utf8Json.Runtime.ExternalType
{
    public interface IUtf8JsonExternalTypeDefine
    {
        IReadOnlyList<Type> Types { get; }
    }
}
