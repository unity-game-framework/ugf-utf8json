using UGF.Utf8Json.Runtime.Resolver;
using UnityEditor;

namespace UGF.Utf8Json.Editor.Resolver
{
    [CustomEditor(typeof(Utf8JsonResolverAsset), true)]
    internal class Utf8JsonResolverAssetEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();
            EditorGUILayout.HelpBox("This asset provides access to generated Utf8Json Resolver.", MessageType.Info);
        }
    }
}
