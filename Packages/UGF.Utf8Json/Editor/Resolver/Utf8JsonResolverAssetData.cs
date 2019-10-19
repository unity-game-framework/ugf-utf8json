using System.Collections.Generic;
using UnityEngine;

namespace UGF.Utf8Json.Editor.Resolver
{
    public class Utf8JsonResolverAssetData : ScriptableObject
    {
        [SerializeField] private bool m_staticCaching = true;
        [SerializeField] private bool m_resolverAsset = true;
        [SerializeField] private bool m_ignoreReadOnly = true;
        [SerializeField] private bool m_attributeRequired = true;
        [SerializeField] private string m_attributeShortName = "Serializable";
        [SerializeField] private List<string> m_sources = new List<string>();
        [SerializeField] private List<string> m_externals = new List<string>();

        public bool StaticCaching { get { return m_staticCaching; } set { m_staticCaching = value; } }
        public bool ResolverAsset { get { return m_resolverAsset; } set { m_resolverAsset = value; } }
        public bool IgnoreReadOnly { get { return m_ignoreReadOnly; } set { m_ignoreReadOnly = value; } }
        public bool AttributeRequired { get { return m_attributeRequired; } set { m_attributeRequired = value; } }
        public string AttributeShortName { get { return m_attributeShortName; } set { m_attributeShortName = value; } }
        public List<string> Sources { get { return m_sources; } }
        public List<string> Externals { get { return m_externals; } }
    }
}
