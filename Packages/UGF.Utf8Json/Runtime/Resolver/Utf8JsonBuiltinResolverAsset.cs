using UnityEngine;
using Utf8Json;
using Utf8Json.Resolvers;

namespace UGF.Utf8Json.Runtime.Resolver
{
    [CreateAssetMenu(menuName = "UGF/Utf8Json/Resolvers/Utf8JsonBuiltinResolver", order = 2000)]
    public class Utf8JsonBuiltinResolverAsset : Utf8JsonResolverAsset
    {
        public override IJsonFormatterResolver GetResolver()
        {
            return BuiltinResolver.Instance;
        }
    }
}
