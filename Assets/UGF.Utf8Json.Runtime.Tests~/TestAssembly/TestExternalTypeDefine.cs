using System;
using System.Collections.Generic;
using UGF.Utf8Json.Runtime.ExternalType;
using Utf8Json;
using Utf8Json.Formatters;

namespace UGF.Utf8Json.Runtime.Tests.TestAssembly
{
    [Utf8JsonExternalTypeDefine]
    public class TestExternalTypeDefine : IUtf8JsonExternalTypeDefine
    {
        public void GetFormatters(IDictionary<Type, IJsonFormatter> formatters)
        {
            formatters.Add(typeof(TestTarget[]), new ArrayFormatter<TestTarget>());
        }
    }
}
