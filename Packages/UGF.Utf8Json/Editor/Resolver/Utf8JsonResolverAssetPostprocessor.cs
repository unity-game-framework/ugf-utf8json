using System;
using System.Collections.Generic;
using System.IO;
using UGF.AssetPipeline.Editor.Asset.Info;
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
                if (Utf8JsonResolverAssetEditorUtility.CanGenerateResolver(resolverPath))
                {
                    Utf8JsonResolverAssetEditorUtility.GenerateResolver(resolverPath);
                }
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
            string[] underAssets = Directory.GetFiles("Assets", resolverSearchPattern, SearchOption.AllDirectories);
            string[] underPackages = Directory.GetFiles("Packages", resolverSearchPattern, SearchOption.AllDirectories);

            CollectSources(cache, underAssets);
            CollectSources(cache, underPackages);
        }

        private static void CollectSources(IDictionary<string, string> cache, IReadOnlyList<string> paths)
        {
            for (int i = 0; i < paths.Count; i++)
            {
                string path = paths[i];
                var info = AssetInfoEditorUtility.LoadInfo<Utf8JsonResolverAssetInfo>(path);

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
