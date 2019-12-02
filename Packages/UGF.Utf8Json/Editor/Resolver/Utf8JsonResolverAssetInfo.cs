using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.Utf8Json.Editor.Resolver
{
    [Serializable]
    public class Utf8JsonResolverAssetInfo
    {
        [SerializeField] private bool m_autoGenerate = true;
        [SerializeField] private string m_resolverName = "Resolver";
        [SerializeField] private string m_namespaceRoot = "Generated";
        [SerializeField] private string m_destinationSource;
        [SerializeField] private bool m_resolverAsset = true;
        [SerializeField] private bool m_ignoreReadOnly = true;
        [SerializeField] private bool m_attributeRequired = true;
        [SerializeField] private string m_attributeTypeName = typeof(SerializableAttribute).AssemblyQualifiedName;
        [SerializeField] private List<string> m_sources = new List<string>();

        public bool AutoGenerate { get { return m_autoGenerate; } set { m_autoGenerate = value; } }
        public string ResolverName { get { return m_resolverName; } set { m_resolverName = value; } }
        public string NamespaceRoot { get { return m_namespaceRoot; } set { m_namespaceRoot = value; } }
        public string DestinationSource { get { return m_destinationSource; } set { m_destinationSource = value; } }
        public bool ResolverAsset { get { return m_resolverAsset; } set { m_resolverAsset = value; } }
        public bool IgnoreReadOnly { get { return m_ignoreReadOnly; } set { m_ignoreReadOnly = value; } }
        public bool AttributeRequired { get { return m_attributeRequired; } set { m_attributeRequired = value; } }
        public string AttributeTypeName { get { return m_attributeTypeName; } set { m_attributeTypeName = value; } }
        public List<string> Sources { get { return m_sources; } }

        public bool TryGetAttributeType(out Type type)
        {
            type = Type.GetType(m_attributeTypeName);

            return type != null;
        }
    }
}
