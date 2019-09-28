using System.Linq;
using NUnit.Framework;
using Utf8Json;

namespace UGF.Utf8Json.Runtime.Tests
{
    public class TestUtf8JsonFormatterResolver
    {
        private class Formatter<T> : IJsonFormatter<T>
        {
            public void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver)
            {
            }

            public T Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
            {
                return default;
            }
        }

        private class Resolver : IJsonFormatterResolver
        {
            public IJsonFormatter<T> GetFormatter<T>()
            {
                return null;
            }
        }

        private class Resolver<TFormatter> : IJsonFormatterResolver
        {
            private readonly Formatter<TFormatter> m_formatter = new Formatter<TFormatter>();

            public IJsonFormatter<T> GetFormatter<T>()
            {
                return m_formatter as IJsonFormatter<T>;
            }
        }

        [Test]
        public void AddFormatter()
        {
            var resolver = new Utf8JsonFormatterResolver();
            var formatter = new Formatter<int>();

            Assert.False(resolver.Formatters.ContainsKey(typeof(int)));
            Assert.AreEqual(0, resolver.Formatters.Count);

            resolver.AddFormatter(formatter);

            Assert.True(resolver.Formatters.ContainsKey(typeof(int)));
            Assert.AreEqual(1, resolver.Formatters.Count);
        }

        [Test]
        public void RemoveFormatter()
        {
            var resolver = new Utf8JsonFormatterResolver();
            var formatter = new Formatter<int>();

            resolver.AddFormatter(formatter);

            Assert.True(resolver.Formatters.ContainsKey(typeof(int)));
            Assert.AreEqual(1, resolver.Formatters.Count);

            resolver.RemoveFormatter(typeof(int));

            Assert.False(resolver.Formatters.ContainsKey(typeof(int)));
            Assert.AreEqual(0, resolver.Formatters.Count);
        }

        [Test]
        public void AddResolver()
        {
            var resolver = new Utf8JsonFormatterResolver();
            var resolver1 = new Resolver();

            Assert.AreEqual(0, resolver.Resolvers.Count);
            Assert.False(resolver.Resolvers.Contains(resolver1));

            resolver.AddResolver(resolver1);

            Assert.AreEqual(1, resolver.Resolvers.Count);
            Assert.True(resolver.Resolvers.Contains(resolver1));
        }

        [Test]
        public void RemoveResolver()
        {
            var resolver = new Utf8JsonFormatterResolver();
            var resolver1 = new Resolver();

            resolver.AddResolver(resolver1);

            Assert.AreEqual(1, resolver.Resolvers.Count);
            Assert.True(resolver.Resolvers.Contains(resolver1));

            resolver.RemoveResolver(resolver1);

            Assert.AreEqual(0, resolver.Resolvers.Count);
            Assert.False(resolver.Resolvers.Contains(resolver1));
        }

        [Test]
        public void TryGetFormatterGeneric()
        {
            var resolver = new Utf8JsonFormatterResolver();
            var formatterInt = new Formatter<int>();
            var resolver1 = new Resolver<float>();

            resolver.AddFormatter(formatterInt);
            resolver.AddResolver(resolver1);

            bool result0 = resolver.TryGetFormatter(out IJsonFormatter<int> formatter0);
            bool result1 = resolver.TryGetFormatter(out IJsonFormatter<bool> formatter1);
            bool result2 = resolver.TryGetFormatter(out IJsonFormatter<float> formatter2);

            Assert.True(result0);
            Assert.False(result1);
            Assert.True(result2);
            Assert.NotNull(formatter0);
            Assert.Null(formatter1);
            Assert.NotNull(formatter2);
        }

        [Test]
        public void TryGetFormatter()
        {
            var resolver = new Utf8JsonFormatterResolver();
            var formatterInt = new Formatter<int>();
            var resolver1 = new Resolver<float>();

            resolver.AddFormatter(formatterInt);
            resolver.AddResolver(resolver1);

            bool result0 = resolver.TryGetFormatter(typeof(int), out IJsonFormatter formatter0);
            bool result1 = resolver.TryGetFormatter(typeof(bool), out IJsonFormatter formatter1);
            bool result2 = resolver.TryGetFormatter(typeof(float), out IJsonFormatter formatter2);

            Assert.True(result0);
            Assert.False(result1);
            Assert.False(result2);
            Assert.NotNull(formatter0);
            Assert.Null(formatter1);
            Assert.Null(formatter2);
        }

        [Test]
        public void GetFormatter()
        {
            var resolver = new Utf8JsonFormatterResolver();
            var formatterInt = new Formatter<int>();
            var resolver1 = new Resolver<float>();

            resolver.AddFormatter(formatterInt);
            resolver.AddResolver(resolver1);

            IJsonFormatter<int> formatter0 = resolver.GetFormatter<int>();
            IJsonFormatter<bool> formatter1 = resolver.GetFormatter<bool>();
            IJsonFormatter<float> formatter2 = resolver.GetFormatter<float>();

            Assert.NotNull(formatter0);
            Assert.Null(formatter1);
            Assert.NotNull(formatter2);
        }
    }
}
