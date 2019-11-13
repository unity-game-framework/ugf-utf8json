using System;
using System.Collections.Generic;
using System.IO;
using UGF.Utf8Json.Editor.ExternalType;
using UnityEditor;
using UnityEngine;

namespace UGF.Utf8Json.Editor.Resolver
{
    internal class Utf8JsonResolverPostprocessor : AssetPostprocessor
    {
        private static readonly Dictionary<string, string> m_resolvers = new Dictionary<string, string>();
        private static readonly HashSet<string> m_resolversGenerate = new HashSet<string>();

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            m_resolversGenerate.Clear();

            CollectGenerate(m_resolvers, m_resolversGenerate, importedAssets);
            CollectGenerate(m_resolvers, m_resolversGenerate, deletedAssets);
            CollectGenerate(m_resolvers, m_resolversGenerate, movedFromAssetPaths);

            Generate(m_resolversGenerate);
        }

        private static void Generate(HashSet<string> generate)
        {
            if (generate == null) throw new ArgumentNullException(nameof(generate));

            foreach (string path in generate)
            {
                Utf8JsonResolverAssetEditorUtility.GenerateResolver(path);
            }
        }

        private static void CollectGenerate(IReadOnlyDictionary<string, string> resolvers, ISet<string> generate, IReadOnlyList<string> paths)
        {
            if (resolvers == null) throw new ArgumentNullException(nameof(resolvers));
            if (generate == null) throw new ArgumentNullException(nameof(generate));
            if (paths == null) throw new ArgumentNullException(nameof(paths));

            for (int i = 0; i < paths.Count; i++)
            {
                string path = paths[i];

                if (Utf8JsonResolverAssetEditorUtility.IsCSharpFile(path) || Utf8JsonExternalTypeEditorUtility.IsExternalFile(path))
                {
                    if (resolvers.TryGetValue(path, out string resolverPath))
                    {
                        generate.Add(resolverPath);
                    }
                }
            }
        }

        private static void CollectResolvers(IDictionary<string, string> resolvers)
        {
            if (resolvers == null) throw new ArgumentNullException(nameof(resolvers));

            string[] paths = Directory.GetFiles(Application.dataPath, Utf8JsonResolverAssetEditorUtility.RESOLVER_SEARCH_PATTERN, SearchOption.AllDirectories);

            for (int i = 0; i < paths.Length; i++)
            {
                string path = paths[i];
                Utf8JsonResolverAssetInfo info = Utf8JsonResolverAssetEditorUtility.LoadResolverInfo(path);

                CollectResolvers(resolvers, path, info);
            }
        }

        private static void CollectResolvers(IDictionary<string, string> resolvers, string path, Utf8JsonResolverAssetInfo info)
        {
            if (resolvers == null) throw new ArgumentNullException(nameof(resolvers));
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));
            if (info == null) throw new ArgumentNullException(nameof(info));

            for (int i = 0; i < info.Sources.Count; i++)
            {
                string source = info.Sources[i];
                string sourcePath = AssetDatabase.GUIDToAssetPath(source);

                if (!string.IsNullOrEmpty(sourcePath))
                {
                    resolvers[sourcePath] = path;
                }
            }
        }
    }
}
