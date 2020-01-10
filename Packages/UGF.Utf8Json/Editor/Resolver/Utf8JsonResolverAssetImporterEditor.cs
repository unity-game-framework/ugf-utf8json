using System.IO;
using UGF.AssetPipeline.Editor.Asset.Info;
using UnityEditor;
using UnityEngine;

namespace UGF.Utf8Json.Editor.Resolver
{
    [CustomEditor(typeof(Utf8JsonResolverAssetImporter))]
    public class Utf8JsonResolverAssetImporterEditor : AssetInfoImporterEditor
    {
        public override bool showImportedObject { get; } = false;

        private AssetImporter m_importer;
        private string m_destinationPath;
        private bool m_destinationPathAnotherExist;

        public override void OnEnable()
        {
            base.OnEnable();

            m_importer = (AssetImporter)targets[0];
        }

        public override void OnInspectorGUI()
        {
            SerializedProperty propertyResolverName = extraDataSerializedObject.FindProperty("m_info.m_resolverName");
            SerializedProperty propertyDestinationSource = extraDataSerializedObject.FindProperty("m_info.m_destinationSource");

            m_destinationPath = Utf8JsonResolverAssetEditorUtility.GetDestinationSourcePath(m_importer.assetPath, propertyResolverName.stringValue, propertyDestinationSource.objectReferenceValue as TextAsset);
            m_destinationPathAnotherExist = propertyDestinationSource.objectReferenceValue == null && File.Exists(m_destinationPath);

            base.OnInspectorGUI();

            if (m_destinationPathAnotherExist)
            {
                string assetName = $"{propertyResolverName.stringValue}Asset";

                EditorGUILayout.Space();
                EditorGUILayout.HelpBox($"A file with the same name of the generate source already exists: '{assetName}'.\nPath: '{m_destinationPath}'.", MessageType.Warning);
            }
        }

        protected override bool OnApplyRevertGUI()
        {
            SerializedProperty propertyDestinationSource = extraDataSerializedObject.FindProperty("m_info.m_destinationSource");

            bool canClear = File.Exists(m_destinationPath) && propertyDestinationSource.objectReferenceValue != null;
            bool canGenerate = Utf8JsonResolverAssetEditorUtility.CanGenerateResolver(m_importer.assetPath);

            using (new EditorGUI.DisabledScope(HasModified()))
            {
                if (GUILayout.Button("Generate All"))
                {
                    Utf8JsonResolverAssetEditorUtility.GenerateResolverAll();
                }

                using (new EditorGUI.DisabledScope(m_destinationPathAnotherExist || !canGenerate))
                {
                    if (GUILayout.Button("Generate"))
                    {
                        Utf8JsonResolverAssetEditorUtility.GenerateResolver(m_importer.assetPath);
                    }
                }

                using (new EditorGUI.DisabledScope(!canClear || !canGenerate))
                {
                    if (GUILayout.Button("Clear"))
                    {
                        if (EditorUtility.DisplayDialog("Delete Utf8Json Generated Script?", $"{m_destinationPath}\nYou cannot undo this action.", "Delete", "Cancel"))
                        {
                            Utf8JsonResolverAssetEditorUtility.ClearResolver(m_importer.assetPath);
                        }
                    }
                }
            }

            return base.OnApplyRevertGUI();
        }
    }
}
