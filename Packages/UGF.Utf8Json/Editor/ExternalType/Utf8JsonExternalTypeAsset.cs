using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.Utf8Json.Editor.ExternalType
{
    [Serializable]
    public class Utf8JsonExternalTypeAsset
    {
        [SerializeField] private string m_type;
        [SerializeField] private List<string> m_members = new List<string>();

        public string Type { get { return m_type; } set { m_type = value; } }
        public List<string> Members { get { return m_members; } }
    }
}
