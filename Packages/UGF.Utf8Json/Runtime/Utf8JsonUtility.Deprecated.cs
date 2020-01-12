using System;
using Utf8Json.Resolvers;
using Utf8Json.Unity;

namespace UGF.Utf8Json.Runtime
{
    public static partial class Utf8JsonUtility
    {
        /// <summary>
        /// Creates default resolver with Unity and Builtin resolvers.
        /// </summary>
        [Obsolete("CreateDefaultResolver has been deprecated.")]
        public static Utf8JsonFormatterResolver CreateDefaultResolver()
        {
            var resolver = new Utf8JsonFormatterResolver();

            resolver.AddResolver(UnityResolver.Instance);
            resolver.AddResolver(BuiltinResolver.Instance);

            return resolver;
        }
    }
}
