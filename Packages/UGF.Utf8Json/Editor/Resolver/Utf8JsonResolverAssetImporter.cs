using System.IO;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

namespace UGF.Utf8Json.Editor.Resolver
{
    [ScriptedImporter(0, Utf8JsonResolverAssetEditorUtility.RESOLVER_ASSET_EXTENSION_NAME)]
    public class Utf8JsonResolverAssetImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext context)
        {
            string source = File.ReadAllText(context.assetPath);
            var asset = new TextAsset(source);

            context.AddObjectToAsset("main", asset);
            context.SetMainObject(asset);
        }
    }
}
