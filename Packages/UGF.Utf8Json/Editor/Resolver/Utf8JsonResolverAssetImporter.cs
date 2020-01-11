using UGF.AssetPipeline.Editor.Asset.Info;
using UnityEditor.Experimental.AssetImporters;

namespace UGF.Utf8Json.Editor.Resolver
{
    [ScriptedImporter(0, Utf8JsonResolverAssetEditorUtility.RESOLVER_ASSET_EXTENSION_NAME)]
    public class Utf8JsonResolverAssetImporter : AssetInfoImporter<Utf8JsonResolverAssetInfo>
    {
    }
}
