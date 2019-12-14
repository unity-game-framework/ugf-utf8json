using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UGF.Utf8Json.Runtime.Tests.Formatter.Typed.Resolvers;
using Unity.Profiling;
using Utf8Json;

namespace UGF.Utf8Json.Runtime.Tests.Formatters.Typed
{
    public class TestTypedFormatter
    {
        private readonly string m_target1Data = "{\"type\":\"UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter+Target1, UGF.Utf8Json.Runtime.Tests\",\"boolValue\":true}";
        private readonly string m_target1Data2 = "{\"type\":\"UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter+Target1, UGF.Utf8Json.Runtime.Tests\",\"boolValue\":false}";
        private readonly string m_target2Data = "{\"type\":\"UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter+Target2, UGF.Utf8Json.Runtime.Tests\",\"intValue\":10}";
        private readonly string m_target2Data2 = "{\"type\":\"UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter+Target2, UGF.Utf8Json.Runtime.Tests\",\"intValue\":100}";
        private readonly string m_target3Data = "{\"type\":\"UGF.Utf8Json.Runtime.Tests.Formatters.Typed.TestTypedFormatter+Target3, UGF.Utf8Json.Runtime.Tests\"}";
        private readonly string m_collectionPath = "Assets/UGF.Utf8Json.Runtime.Tests/Formatters.Typed/collection.json";
        private ProfilerMarker m_serializeMethodMarker = new ProfilerMarker("TestTypedFormatter.SerializeProfiler()");

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

        [Serializable]
        public class Target3 : ITarget
        {
        }

        [Serializable]
        public class TargetCollection
        {
            public List<ITarget> Targets { get; set; } = new List<ITarget>();
        }

        [Test]
        public void Serialize()
        {
            var resolver = new Utf8JsonFormatterResolver();

            resolver.AddResolver(TestTypedFormatterResolver.Instance);

            var target1 = new Target1();
            var target2 = new Target2();
            var target3 = new Target3();

            string data1 = JsonSerializer.ToJsonString<ITarget>(target1, resolver);
            string data2 = JsonSerializer.ToJsonString<ITarget>(target2, resolver);
            string data3 = JsonSerializer.ToJsonString<ITarget>(target3, resolver);

            Assert.AreEqual(m_target1Data, data1);
            Assert.AreEqual(m_target2Data, data2);
            Assert.AreEqual(m_target3Data, data3);
            Assert.Pass($"{data1}\n{data2}\n{data3}");
        }

        [Test]
        public void SerializeCollection()
        {
            var resolver = new Utf8JsonFormatterResolver();

            resolver.AddResolver(TestTypedFormatterResolver.Instance);

            var collection = new TargetCollection
            {
                Targets =
                {
                    new Target1(),
                    new Target2(),
                    new Target3(),
                    new Target3(),
                    new Target2(),
                    new Target1()
                }
            };

            string data = JsonSerializer.ToJsonString(collection, resolver);

            data = JsonSerializer.PrettyPrint(data);

            File.WriteAllText(m_collectionPath, data);
            Assert.Pass(data);
        }

        [Test]
        public void SerializeProfiler()
        {
            var resolver = new Utf8JsonFormatterResolver();

            resolver.AddResolver(TestTypedFormatterResolver.Instance);

            var target = new Target1();
            var writer = new JsonWriter(new byte[100]);

            resolver.GetFormatter<ITarget>().Serialize(ref writer, target, resolver);

            m_serializeMethodMarker.Begin();

            resolver.GetFormatter<ITarget>().Serialize(ref writer, target, resolver);

            m_serializeMethodMarker.End();

            Assert.Pass();
        }

        [Test]
        public void Deserialize()
        {
            var resolver = new Utf8JsonFormatterResolver();

            resolver.AddResolver(TestTypedFormatterResolver.Instance);

            var target1 = JsonSerializer.Deserialize<ITarget>(m_target1Data2, resolver);
            var target2 = JsonSerializer.Deserialize<ITarget>(m_target2Data2, resolver);

            Assert.AreEqual(typeof(Target1), target1.GetType());
            Assert.AreEqual(typeof(Target2), target2.GetType());

            var target12 = (Target1)target1;
            var target22 = (Target2)target2;

            Assert.AreEqual(false, target12.BoolValue);
            Assert.AreEqual(100, target22.IntValue);
        }
    }
}
