using System;
using System.IO;
using System.Text;
using NUnit.Framework;
using Utf8Json;

namespace UGF.Utf8Json.Runtime.Tests
{
    public class TestEncoding
    {
        private Utf8JsonFormatterResolver m_resolver;

        [Serializable]
        public class Target
        {
            public string Value { get; set; }
        }

        [OneTimeSetUp]
        public void Setup()
        {
            m_resolver = Utf8JsonUtility.CreateDefaultResolver();
            m_resolver.AddResolver(Generated.Resolvers.Resolver.Instance);
        }

        [Test]
        public void Test()
        {
            var target = new Target { Value = "汉字" };
            string text = JsonSerializer.ToJsonString(target, m_resolver);
            byte[] bytes = JsonSerializer.Serialize(target, m_resolver);

            File.WriteAllBytes("Assets/UGF.Utf8Json.Runtime.Tests/TestEncodingBytes.bytes", bytes);

            var target0 = JsonSerializer.Deserialize<Target>(text, m_resolver);

            Assert.NotNull(target0);
            Assert.AreEqual("汉字", target0.Value);
            Assert.Pass(text);
        }
    }
}
