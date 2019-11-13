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
            const string name = "New Utf8Json Resolver." + Utf8JsonResolverAssetEditorUtility.RESOLVER_ASSET_EXTENSION_NAME;

            ProjectWindowUtil.CreateAssetWithContent(name, "{}", icon);
        }
    }
}
