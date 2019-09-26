using System;
using System.Runtime.Serialization;
using NUnit.Framework;
using UGF.Utf8Json.Runtime.Formatters;
using UGF.Utf8Json.Runtime.Tests.Resolvers;

namespace UGF.Utf8Json.Runtime.Tests.Formatters
{
    public class TestUnionFormatter
    {
        private readonly string m_target1Data = "{\"type\":\"one\",\"boolValue\":true}";
        private readonly string m_target1Data2 = "{\"type\":\"one\",\"boolValue\":false}";
        private readonly string m_target2Data = "{\"type\":\"two\",\"intValue\":10}";
        private readonly string m_target2Data2 = "{\"type\":\"two\",\"intValue\":100}";
        private Utf8JsonFormatterResolver m_resolver;

        private class Formatter : UnionFormatter<ITarget>
        {
            public Formatter()
            {
                AddFormatter<Target1>("one");
                AddFormatter<Target2>("two");
            }
        }

        public interface ITarget
        {
        }

        [Serializable]
        public class Target1 : ITarget
        {
            public bool BoolValue { get; set; } = true;
        }

        [Serializable]
        public class Target2 : ITarget
        {
            public int IntValue { get; set; } = 10;
        }

        [OneTimeSetUp]
        public void Setup()
        {
            m_resolver = Utf8JsonUtility.CreateDefaultResolver();
            m_resolver.AddFormatter<ITarget>(new Formatter());
            m_resolver.AddResolver(UGFUtf8JsonRuntimeTestsResolver.Instance);
        }

        public void AddFormatter()
        {
        }

        public void RemoveFormatter()
        {
        }

        public void Clear()
        {
        }

        public void GetFormatterIdentifier()
        {
        }

        public void TryGetFormatterIdentifier()
        {
        }

        public void GetFormatter()
        {
        }

        public void TryGetFormatterByTargetType()
        {
        }

        public void TryGetFormatterByIdentifier()
        {
        }

        public void Serialize()
        {
        }

        public void Deserialize()
        {
        }

        public void WriteTypeIdentifierSpace()
        {
        }

        public void WriteTypeIdentifier()
        {
        }

        public void ReadTypeIdentifier()
        {
        }
    }
}
