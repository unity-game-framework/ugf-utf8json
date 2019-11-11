using System;
using System.Collections.Generic;
using System.IO;
using UGF.Code.Generate.Editor;
using UnityEditor;
using UnityEngine;

namespace UGF.Utf8Json.Editor.Resolver
{
    public class Utf8JsonResolverAssetData : ScriptableObject
    {
        [SerializeField] private bool m_autoGenerate = true;
        [SerializeField] private string m_name = "Resolver";
        [SerializeField] private string m_namespaceRoot = "Generated";
        [SerializeField] private bool m_staticCaching = true;
        [SerializeField] private bool m_resolverAsset = true;
        [SerializeField] private bool m_ignoreReadOnly = true;
        [SerializeField] private bool m_attributeRequired = true;
        [SerializeField] private string m_attributeTypeName = $"{typeof(SerializableAttribute).AssemblyQualifiedName}";
        [SerializeField] private bool m_generateSource = true;
        [SerializeField] private string m_source;
        [SerializeField] private List<string> m_sources = new List<string>();

        public bool AutoGenerate { get { return m_autoGenerate; } set { m_autoGenerate = value; } }
        public string Name { get { return m_name; } set { m_name = value; } }
        public string NamespaceRoot { get { return m_namespaceRoot; } set { m_namespaceRoot = value; } }
        public bool StaticCaching { get { return m_staticCaching; } set { m_staticCaching = value; } }
        public bool ResolverAsset { get { return m_resolverAsset; } set { m_resolverAsset = value; } }
        public bool IgnoreReadOnly { get { return m_ignoreReadOnly; } set { m_ignoreReadOnly = value; } }
        public bool AttributeRequired { get { return m_attributeRequired; } set { m_attributeRequired = value; } }
        public string AttributeTypeName { get { return m_attributeTypeName; } set { m_attributeTypeName = value; } }
        public bool GenerateSource { get { return m_generateSource; } set { m_generateSource = value; } }
        public string Source { get { return m_source; } set { m_source = value; } }
        public List<string> Sources { get { return m_sources; } }

        public bool TryGetAttributeType(out Type type)
        {
            type = Type.GetType(m_attributeTypeName);

            return type != null;
        }
    }
}
