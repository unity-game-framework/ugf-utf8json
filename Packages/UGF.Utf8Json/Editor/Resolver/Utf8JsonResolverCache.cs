using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace UGF.Utf8Json.Editor.Resolver
{
    [InitializeOnLoad]
    internal static class Utf8JsonResolverCache
    {
        public static IDictionary<string, string> Cache { get { return m_cache; } }

        private static readonly Dictionary<string, string> m_cache = new Dictionary<string, string>();
        private const string kCachePath = "Library/Packages/com.ugf.utf8json/resolvers.cache.json";

        static Utf8JsonResolverCache()
        {
            ReloadCache();
        }

        public static void Add(string path, Utf8JsonResolverAssetInfo info)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));
            if (info == null) throw new ArgumentNullException(nameof(info));

            for (int i = 0; i < info.Sources.Count; i++)
            {
                string source = info.Sources[i];
                string sourcePath = AssetDatabase.GUIDToAssetPath(source);

                if (!string.IsNullOrEmpty(sourcePath))
                {
                    m_cache[sourcePath] = path;
                }
            }
        }

        public static void ReloadCache()
        {
            m_cache.Clear();

            Utf8JsonResolverCacheInfo info = LoadInfo();

            for (int i = 0; i < info.Pairs.Count; i++)
            {
                Utf8JsonResolverCacheInfo.Pair pair = info.Pairs[i];

                m_cache[pair.Key] = pair.Value;
            }
        }

        public static void SaveCache()
        {
            var info = new Utf8JsonResolverCacheInfo();

            foreach (KeyValuePair<string, string> pair in m_cache)
            {
                info.Pairs.Add(new Utf8JsonResolverCacheInfo.Pair
                {
                    Key = pair.Key,
                    Value = pair.Value
                });
            }

            SaveInfo(info);
        }

        private static Utf8JsonResolverCacheInfo LoadInfo()
        {
            var info = new Utf8JsonResolverCacheInfo();

            if (File.Exists(kCachePath))
            {
                string source = File.ReadAllText(kCachePath);

                if (!string.IsNullOrEmpty(source))
                {
                    EditorJsonUtility.FromJsonOverwrite(source, info);
                }
            }

            return info;
        }

        private static void SaveInfo(Utf8JsonResolverCacheInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));

            string source = EditorJsonUtility.ToJson(info, true);

            File.WriteAllText(kCachePath, source);
        }
    }
}
