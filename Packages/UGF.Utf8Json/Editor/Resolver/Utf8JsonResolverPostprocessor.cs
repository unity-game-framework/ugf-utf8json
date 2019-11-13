using System;
using System.Collections.Generic;
using UGF.Utf8Json.Editor.ExternalType;
using UnityEditor;

namespace UGF.Utf8Json.Editor.Resolver
{
    internal class Utf8JsonResolverPostprocessor : AssetPostprocessor
    {
        private static readonly HashSet<string> m_resolversGenerate = new HashSet<string>();

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            m_resolversGenerate.Clear();

            CollectGenerate(Utf8JsonResolverCache.Cache, m_resolversGenerate, importedAssets);
            CollectGenerate(Utf8JsonResolverCache.Cache, m_resolversGenerate, deletedAssets);
            CollectGenerate(Utf8JsonResolverCache.Cache, m_resolversGenerate, movedFromAssetPaths);

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

        private static void CollectGenerate(IDictionary<string, string> cache, ISet<string> generate, IReadOnlyList<string> paths)
        {
            if (cache == null) throw new ArgumentNullException(nameof(cache));
            if (generate == null) throw new ArgumentNullException(nameof(generate));
            if (paths == null) throw new ArgumentNullException(nameof(paths));

            for (int i = 0; i < paths.Count; i++)
            {
                string path = paths[i];

                if (Utf8JsonResolverAssetEditorUtility.IsCSharpFile(path) || Utf8JsonExternalTypeEditorUtility.IsExternalFile(path))
                {
                    if (cache.TryGetValue(path, out string resolverPath))
                    {
                        generate.Add(resolverPath);
                    }
                }
            }
        }
    }
}
