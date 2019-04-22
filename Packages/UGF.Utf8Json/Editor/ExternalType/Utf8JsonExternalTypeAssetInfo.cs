using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.Utf8Json.Editor.ExternalType
{
    [Serializable]
    public class Utf8JsonExternalTypeAssetInfo
    {
        [SerializeField] private string m_type = string.Empty;
        [SerializeField] private List<MemberInfo> m_members = new List<MemberInfo>();

        public string Type { get { return m_type; } set { m_type = value; } }
        public List<MemberInfo> Members { get { return m_members; } }

        [Serializable]
        public struct MemberInfo
        {
            [SerializeField] private string m_name;
            [SerializeField] private string m_type;
            [SerializeField] private bool m_active;

            public string Name { get { return m_name; } set { m_name = value; } }
            public string Type { get { return m_type; } set { m_type = value; } }
            public bool Active { get { return m_active; } set { m_active = value; } }
        }

        public Type GetTargetType()
        {
            return System.Type.GetType(m_type, true);
        }

        public bool Contains(string memberName)
        {
            for (int i = 0; i < m_members.Count; i++)
            {
                MemberInfo member = m_members[i];

                if (member.Active && member.Name == memberName)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(m_type) && System.Type.GetType(m_type) != null;
        }
    }
}
