using System;
using System.Collections.Generic;
using NUnit.Framework;
using UGF.Utf8Json.Runtime.ExternalType;
using UGF.Utf8Json.Runtime.Tests.TestAssembly;
using Utf8Json;

namespace UGF.Utf8Json.Runtime.Tests.ExternalType
{
    public class TestUtf8JsonExternalTypeUtility
    {
        [Test]
        public void GetFormatters()
        {
            var formatters = new Dictionary<Type, IJsonFormatter>();

            Utf8JsonExternalTypeUtility.GetFormatters(formatters, typeof(TestTarget).Assembly);

            Assert.AreEqual(1, formatters.Count);
        }

        [Test]
        public void GetDefines()
        {
            var defines = new List<IUtf8JsonExternalTypeDefine>();

            Utf8JsonExternalTypeUtility.GetDefines(defines, typeof(TestTarget).Assembly);

            Assert.AreEqual(1, defines.Count);
        }
    }
}
