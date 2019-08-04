using UGF.Code.Generate.Editor.Container.External;
using UnityEditor;

namespace UGF.Utf8Json.Editor.ExternalType
{
    /// <summary>
    /// Represents inspector drawer for the <see cref="Utf8JsonExternalTypeAssetImporter"/>.
    /// </summary>
    [CustomEditor(typeof(Utf8JsonExternalTypeAssetImporter))]
    public class Utf8JsonExternalTypeAssetImporterEditor : CodeGenerateContainerExternalAssetImporterEditor
    {
    }
}
