using System;
using System.Collections.Generic;

namespace UGF.Utf8Json.Runtime.ExternalType
{
    public class Utf8JsonExternalTypeDefine : IUtf8JsonExternalTypeDefine
    {
        public List<Type> Types { get; } = new List<Type>();

        IReadOnlyList<Type> IUtf8JsonExternalTypeDefine.Types { get { return Types; } }
    }
}
