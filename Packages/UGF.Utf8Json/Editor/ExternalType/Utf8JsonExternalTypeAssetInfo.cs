using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.Utf8Json.Editor.ExternalType
{
    /// <summary>
    /// Represents information of the external type to generate formatter.
    /// </summary>
    [Serializable]
    public class Utf8JsonExternalTypeAssetInfo
    {
        [SerializeField] private string m_type = string.Empty;
        [SerializeField] private List<MemberInfo> m_members = new List<MemberInfo>();

        /// <summary>
        /// Gets or sets the assembly qualified name of the target type.
        /// </summary>
        public string Type { get { return m_type; } set { m_type = value; } }

        /// <summary>
        /// Gets the collection of the members to generate.
        /// </summary>
        public List<MemberInfo> Members { get { return m_members; } }

        /// <summary>
        /// Represents member information to generate in formatter.
        /// </summary>
        [Serializable]
        public struct MemberInfo
        {
            [SerializeField] private string m_name;
            [SerializeField] private string m_type;
            [SerializeField] private bool m_active;

            /// <summary>
            /// Gets or sets the name of the member.
            /// </summary>
            public string Name { get { return m_name; } set { m_name = value; } }

            /// <summary>
            /// Gets or sets the type information.
            /// </summary>
            public string Type { get { return m_type; } set { m_type = value; } }

            /// <summary>
            /// Gets or sets value determines whether to use this member in formatter generation.
            /// </summary>
            public bool Active { get { return m_active; } set { m_active = value; } }
        }

        /// <summary>
        /// Gets the target type from the type string representation.
        /// <para>
        /// This method will throw exception if the target type can not be get.
        /// </para>
        /// </summary>
        public Type GetTargetType()
        {
            return System.Type.GetType(m_type, true);
        }

        /// <summary>
        /// Determines whether this info contains valid target type information.
        /// </summary>
        public bool IsTargetTypeValid()
        {
            return !string.IsNullOrEmpty(m_type) && System.Type.GetType(m_type) != null;
        }

        /// <summary>
        /// Tries to get member info be the specified member name.
        /// </summary>
        /// <param name="name">The name of the member to find.</param>
        /// <param name="member">The found member info.</param>
        public bool TryGetMember(string name, out MemberInfo member)
        {
            for (int i = 0; i < m_members.Count; i++)
            {
                member = m_members[i];

                if (member.Name == name)
                {
                    return true;
                }
            }

            member = default;
            return false;
        }
    }
}
