using UnityEditor;
using UnityEngine;

namespace UGF.Utf8Json.Editor.ExternalType
{
    internal static class Utf8JsonExternalTypeEditorMenu
    {
        [MenuItem("Assets/Create/UGF/Utf8Json/Utf8Json External Type", false, 2000)]
        private static void ExternalTypeCreateMenu()
        {
            Texture2D icon = AssetPreview.GetMiniTypeThumbnail(typeof(TextAsset));
            const string name = "New Utf8Json External Type." + Utf8JsonExternalTypeEditorUtility.EXTERNAL_TYPE_ASSET_EXTENSION_NAME;

            ProjectWindowUtil.CreateAssetWithContent(name, "{}", icon);
        }
    }
}
