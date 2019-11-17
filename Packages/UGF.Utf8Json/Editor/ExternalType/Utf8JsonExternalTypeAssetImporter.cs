using UGF.Code.Generate.Editor.Container.External;
using UnityEditor.Experimental.AssetImporters;

namespace UGF.Utf8Json.Editor.ExternalType
{
    /// <summary>
    /// Represents scripted importer to handle the ".utf8json-external" extension files.
    /// </summary>
    [ScriptedImporter(0, Utf8JsonExternalTypeEditorUtility.EXTERNAL_TYPE_ASSET_EXTENSION_NAME)]
    public class Utf8JsonExternalTypeAssetImporter : CodeGenerateContainerExternalAssetImporter<Utf8JsonExternalTypeAssetInfo>
    {
    }
}
