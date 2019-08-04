using NUnit.Framework;
using UnityEngine;
using Utf8Json;
using Utf8Json.Formatters;

namespace UGF.Utf8Json.Runtime.Tests
{
    public class TestUtf8JsonFormatterResolver
    {
        private Utf8JsonFormatterResolver m_resolver;

        [SetUp]
        public void Setup()
        {
            m_resolver = Utf8JsonUtility.CreateDefaultResolver();
        }

        [Test]
        public void GetEnum()
        {
            IJsonFormatter<HideFlags> formatter = m_resolver.GetFormatter<HideFlags>();

            Assert.NotNull(formatter);
            Assert.True(formatter is EnumFormatter<HideFlags>);
        }
    }
}
