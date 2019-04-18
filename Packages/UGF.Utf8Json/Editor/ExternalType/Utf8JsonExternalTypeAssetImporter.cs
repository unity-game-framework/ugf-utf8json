using System.IO;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

namespace UGF.Utf8Json.Editor.ExternalType
{
    [ScriptedImporter(0, "utf8json-ext")]
    public class Utf8JsonExternalTypeAssetImporter : ScriptedImporter
    {
        [SerializeField] private Utf8JsonExternalTypeAsset m_asset = new Utf8JsonExternalTypeAsset();

        public override void OnImportAsset(AssetImportContext context)
        {
            string source = File.ReadAllText(context.assetPath);

            JsonUtility.FromJsonOverwrite(source, m_asset);
        }

        public void Save()
        {
            string source = JsonUtility.ToJson(m_asset, true);

            File.WriteAllText(assetPath, source);

            SaveAndReimport();
        }
    }
}
