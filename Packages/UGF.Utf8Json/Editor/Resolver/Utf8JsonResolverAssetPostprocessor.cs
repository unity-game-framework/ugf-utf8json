using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace UGF.Utf8Json.Editor.Resolver
{
    [InitializeOnLoad]
    internal class Utf8JsonResolverAssetPostprocessor : AssetPostprocessor
    {
        private static readonly Dictionary<string, string> m_sources = new Dictionary<string, string>();
        private static readonly HashSet<string> m_resolvers = new HashSet<string>();

        static Utf8JsonResolverAssetPostprocessor()
        {
            CollectSources(m_sources);
        }

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            m_resolvers.Clear();

            ProcessPaths(importedAssets, m_sources, m_resolvers);
            ProcessPaths(deletedAssets, m_sources, m_resolvers);
            ProcessPaths(movedAssets, m_sources, m_resolvers);

            foreach (string resolverPath in m_resolvers)
            {
                Utf8JsonResolverAssetEditorUtility.GenerateResolver(resolverPath);
            }
        }

        private static void ProcessPaths(IReadOnlyList<string> paths, IReadOnlyDictionary<string, string> sources, ISet<string> resolvers)
        {
            for (int i = 0; i < paths.Count; i++)
            {
                string path = paths[i];

                if (path.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
                {
                    if (sources.TryGetValue(path, out string resolverPath))
                    {
                        resolvers.Add(resolverPath);
                    }
                }
            }
        }

        private static void CollectSources(IDictionary<string, string> cache)
        {
            const string resolverSearchPattern = "*." + Utf8JsonResolverAssetEditorUtility.RESOLVER_ASSET_EXTENSION_NAME;
            string[] paths = Directory.GetFiles("Assets", resolverSearchPattern, SearchOption.AllDirectories);

            for (int i = 0; i < paths.Length; i++)
            {
                string path = paths[i];
                Utf8JsonResolverAssetInfo info = Utf8JsonResolverAssetEditorUtility.LoadResolverInfo(path);

                if (info.AutoGenerate)
                {
                    foreach (string source in info.Sources)
                    {
                        string sourcePath = AssetDatabase.GUIDToAssetPath(source);

                        cache[sourcePath] = path;
                    }
                }
            }
        }
    }
}
