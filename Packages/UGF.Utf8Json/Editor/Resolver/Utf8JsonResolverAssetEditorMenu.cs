using UnityEditor;
using UnityEngine;

namespace UGF.Utf8Json.Editor.Resolver
{
    internal static class Utf8JsonResolverAssetEditorMenu
    {
        [MenuItem("Assets/Create/UGF/Utf8Json/Utf8Json Resolver", false, 2000)]
        private static void ExternalTypeCreateMenu()
        {
            Texture2D icon = AssetPreview.GetMiniTypeThumbnail(typeof(TextAsset));
            string extension = Utf8JsonResolverAssetEditorUtility.ResolverAssetExtensionName;

            ProjectWindowUtil.CreateAssetWithContent($"New Utf8Json Resolver.{extension}", "{}", icon);
        }
    }
}
