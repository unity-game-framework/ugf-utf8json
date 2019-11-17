using UnityEngine;

namespace UGF.Utf8Json.Editor.Resolver
{
    internal class Utf8JsonResolverAssetImporterData : ScriptableObject
    {
        [SerializeField] private Utf8JsonResolverAssetInfo m_info = new Utf8JsonResolverAssetInfo();

        public Utf8JsonResolverAssetInfo Info { get { return m_info; } set { m_info = value; } }
    }
}
