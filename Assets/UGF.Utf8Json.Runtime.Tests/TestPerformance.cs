using NUnit.Framework;
using UGF.Utf8Json.Runtime.Tests.TestAssembly;
using Unity.PerformanceTesting;
using Unity.Profiling;
using UnityEngine;
using Utf8Json;

namespace UGF.Utf8Json.Runtime.Tests
{
    public class TestPerformance
    {
        private Utf8JsonFormatterResolver m_resolver;
        private TestTarget m_target;
        private string m_targetJson;
        private byte[] m_targetBytes;
        private ProfilerMarker m_serializeMethodMarker = new ProfilerMarker("Test.Serialize");
        private ProfilerMarker m_serializeMethodStringMarker = new ProfilerMarker("Test.Serialize.String");

        [SetUp]
        public void Setup()
        {
            m_resolver = Utf8JsonUtility.CreateDefaultResolver();
            m_resolver.CacheFormatters();
            m_target = new TestTarget();
            m_targetJson = Resources.Load<TextAsset>("TestTargetJson").text;
            m_targetBytes = Resources.Load<TextAsset>("TestTargetBytes").bytes;
        }

        [Test, PerformanceTest]
        public void Serialize()
        {
            Measure.Method(SerializeMethod).WarmupCount(1).GC().Run();
        }

        [Test, PerformanceTest]
        public void SerializeUnsafe()
        {
            Measure.Method(SerializeMethodUnsafe).WarmupCount(1).GC().Run();
        }

        [Test, PerformanceTest]
        public void SerializeString()
        {
            Measure.Method(SerializeMethodString).WarmupCount(1).GC().Run();
        }

        [Test]
        public void SerializeProfiler()
        {
            SerializeMethod();

            m_serializeMethodMarker.Begin();

            SerializeMethod();

            m_serializeMethodMarker.End();
        }

        [Test]
        public void SerializeStringProfiler()
        {
            SerializeMethodString();

            m_serializeMethodStringMarker.Begin();

            SerializeMethodString();

            m_serializeMethodStringMarker.End();
        }

        [Test, PerformanceTest]
        public void Deserialize()
        {
            Measure.Method(DeserializeMethod).WarmupCount(1).GC().Run();
        }

        [Test, PerformanceTest]
        public void DeserializeString()
        {
            Measure.Method(DeserializeMethodString).WarmupCount(1).GC().Run();
        }

        private void SerializeMethod()
        {
            JsonSerializer.Serialize(m_target, m_resolver);
        }

        private void SerializeMethodUnsafe()
        {
            JsonSerializer.SerializeUnsafe(m_target, m_resolver);
        }

        private void SerializeMethodString()
        {
            JsonSerializer.ToJsonString(m_target, m_resolver);
        }

        private void DeserializeMethod()
        {
            JsonSerializer.Deserialize<TestTarget>(m_targetBytes, m_resolver);
        }

        private void DeserializeMethodString()
        {
            JsonSerializer.Deserialize<TestTarget>(m_targetJson, m_resolver);
        }
    }
}
