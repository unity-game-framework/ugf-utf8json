using System;
using Generated2.Resolvers;
using NUnit.Framework;
using UnityEngine;
using Utf8Json;
using Utf8Json.Resolvers;
using Utf8Json.Unity;

namespace UGF.Utf8Json.Runtime.Tests
{
    public class TestSerialization
    {
        private readonly string m_targetSerialized = "{\"name\":\"Target\",\"boolValue\":true,\"floatValue\":50.5,\"intValue\":50,\"flags\":52}";
        private readonly string m_target2Serialized = "{\"vector2\":{\"x\":1,\"y\":1},\"bounds\":{\"center\":{\"x\":1,\"y\":1,\"z\":1},\"size\":{\"x\":1,\"y\":1,\"z\":1}}}";
        private Utf8JsonFormatterResolver m_resolver;

        [Serializable]
        public class Target
        {
            public string Name { get; set; } = "Target";
            public bool BoolValue { get; set; } = true;
            public float FloatValue { get; set; } = 50.5F;
            public int IntValue { get; set; } = 50;
            public HideFlags Flags { get; set; } = HideFlags.DontSave;
        }

        [Serializable]
        public class Target2
        {
            public Vector2 Vector2 { get; set; } = Vector2.one;
            public Bounds Bounds { get; set; } = new Bounds(Vector3.one, Vector3.one);
        }

        public class Target3
        {
            public string Name { get; set; } = "Target";
            public bool BoolValue { get; set; } = true;
        }

        [Serializable]
        public class Target4
        {
        }

        [OneTimeSetUp]
        public void Setup()
        {
            m_resolver = new Utf8JsonFormatterResolver();
            m_resolver.AddResolver(BuiltinResolver.Instance);
            m_resolver.AddResolver(UnityResolver.Instance);
            m_resolver.AddResolver(Resolver2.Instance);
        }

        [Test]
        public void Serialize()
        {
            var target = new Target();

            string data = JsonSerializer.ToJsonString(target, m_resolver);

            Assert.AreEqual(m_targetSerialized, data);
        }

        [Test]
        public void Serialize2()
        {
            var target = new Target2();

            string data = JsonSerializer.ToJsonString(target, m_resolver);

            Assert.AreEqual(m_target2Serialized, data);
        }

        [Test]
        public void Deserialize()
        {
            var target = JsonSerializer.Deserialize<Target>(m_targetSerialized, m_resolver);

            Assert.NotNull(target);
            Assert.AreEqual("Target", target.Name);
            Assert.AreEqual(true, target.BoolValue);
            Assert.AreEqual(50.5F, target.FloatValue);
            Assert.AreEqual(50, target.IntValue);
            Assert.AreEqual(HideFlags.DontSave, target.Flags);
        }

        [Test]
        public void Deserialize2()
        {
            var target = JsonSerializer.Deserialize<Target2>(m_target2Serialized, m_resolver);

            Assert.NotNull(target);
            Assert.AreEqual(Vector2.one, target.Vector2);
            Assert.AreEqual(new Bounds(Vector3.one, Vector3.one), target.Bounds);
        }
    }
}
