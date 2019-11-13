using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.Utf8Json.Editor.Resolver
{
    [Serializable]
    internal class Utf8JsonResolverCacheInfo
    {
        [SerializeField] private List<Pair> m_pairs = new List<Pair>();

        public List<Pair> Pairs { get { return m_pairs; } }

        [Serializable]
        public class Pair
        {
            [SerializeField] private string m_key;
            [SerializeField] private string m_value;

            public string Key { get { return m_key; } set { m_key = value; } }
            public string Value { get { return m_value; } set { m_value = value; } }
        }
    }
}
