using UnityEditor;
using UnityEngine;

namespace UGF.Utf8Json.Editor.ExternalType
{
    internal static class Utf8JsonExternalTypeEditorMenu
    {
        [MenuItem("Assets/Create/UGF/Utf8Json External Type", false, 2000)]
        private static void ExternalTypeCreateMenu()
        {
            var asset = new Utf8JsonExternalTypeAsset();
            string content = JsonUtility.ToJson(asset, true);
            Texture2D icon = AssetPreview.GetMiniTypeThumbnail(typeof(DefaultAsset));

            ProjectWindowUtil.CreateAssetWithContent("New Utf8Json External Type.utf8json-ext", content, icon);
        }
    }
}
