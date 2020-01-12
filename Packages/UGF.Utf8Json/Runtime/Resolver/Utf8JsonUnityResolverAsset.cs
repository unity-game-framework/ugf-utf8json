using UnityEngine;
using Utf8Json;
using Utf8Json.Unity;

namespace UGF.Utf8Json.Runtime.Resolver
{
    [CreateAssetMenu(menuName = "UGF/Utf8Json/Resolvers/Utf8JsonUnityResolver", order = 2000)]
    public class Utf8JsonUnityResolverAsset : Utf8JsonResolverAsset
    {
        public override IJsonFormatterResolver GetResolver()
        {
            return UnityResolver.Instance;
        }
    }
}
