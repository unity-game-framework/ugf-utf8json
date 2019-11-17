using UnityEngine;
using Utf8Json;

namespace UGF.Utf8Json.Runtime.Resolver
{
    public abstract class Utf8JsonResolverAsset : ScriptableObject
    {
        public abstract IJsonFormatterResolver GetResolver();
    }
}
