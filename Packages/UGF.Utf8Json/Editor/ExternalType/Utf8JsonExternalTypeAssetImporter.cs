using System.IO;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

namespace UGF.Utf8Json.Editor.ExternalType
{
    [ScriptedImporter(0, "utf8json-external")]
    public class Utf8JsonExternalTypeAssetImporter : ScriptedImporter
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
