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
            string extension = Utf8JsonExternalTypeEditorUtility.ExternalTypeAssetExtensionName;

            ProjectWindowUtil.CreateAssetWithContent($"New Utf8Json External Type.{extension}", "{}", icon);
        }
    }
}
