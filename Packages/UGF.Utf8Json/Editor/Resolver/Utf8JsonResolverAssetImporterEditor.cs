using System;
using System.IO;
using UGF.AssetPipeline.Editor.Asset.Info;
using UGF.EditorTools.Editor.IMGUI.Types;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.Utf8Json.Editor.Resolver
{
    [CustomEditor(typeof(Utf8JsonResolverAssetImporter))]
    public class Utf8JsonResolverAssetImporterEditor : AssetInfoImporterEditor
    {
        public override bool showImportedObject { get; } = false;

        private AssetImporter m_importer;
        private ReorderableList m_sources;

        private TypesDropdown m_dropdown;
        private string m_destinationPath;
        private bool m_destinationPathAnotherExist;

        public override void OnEnable()
        {
            base.OnEnable();

            m_importer = (AssetImporter)targets[0];

            SerializedProperty propertySources = extraDataSerializedObject.FindProperty("m_info.m_sources");

            m_sources = new ReorderableList(extraDataSerializedObject, propertySources);
            m_sources.drawHeaderCallback = DrawHeader;
            m_sources.elementHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing * 2F;
            m_sources.drawElementCallback = (rect, index, active, focused) => DrawElement(m_sources, rect, index, typeof(TextAsset));
            m_sources.onAddCallback = OnAddSource;

            m_dropdown = new TypesDropdown(() => TypeCache.GetTypesDerivedFrom<Attribute>());
            m_dropdown.Selected += OnDropdownTypeSelected;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (m_destinationPathAnotherExist)
            {
                SerializedProperty propertyResolverName = extraDataSerializedObject.FindProperty("m_info.m_resolverName");

                string assetName = $"{propertyResolverName.stringValue}Asset";

                EditorGUILayout.Space();
                EditorGUILayout.HelpBox($"A file with the same name of the generate source already exists: '{assetName}'.\nPath: '{m_destinationPath}'.", MessageType.Warning);
            }
        }

        protected override void OnDrawInfo()
        {
            extraDataSerializedObject.UpdateIfRequiredOrScript();

            SerializedProperty propertyAutoGenerate = extraDataSerializedObject.FindProperty("m_info.m_autoGenerate");
            SerializedProperty propertyResolverName = extraDataSerializedObject.FindProperty("m_info.m_resolverName");
            SerializedProperty propertyNamespaceRoot = extraDataSerializedObject.FindProperty("m_info.m_namespaceRoot");
            SerializedProperty propertyDestinationSource = extraDataSerializedObject.FindProperty("m_info.m_destinationSource");
            SerializedProperty propertyResolverAsset = extraDataSerializedObject.FindProperty("m_info.m_resolverAsset");
            SerializedProperty propertyIgnoreReadOnly = extraDataSerializedObject.FindProperty("m_info.m_ignoreReadOnly");
            SerializedProperty propertyIgnoreEmpty = extraDataSerializedObject.FindProperty("m_info.m_ignoreEmpty");
            SerializedProperty propertyAttributeRequired = extraDataSerializedObject.FindProperty("m_info.m_attributeRequired");
            SerializedProperty propertyAttributeTypeName = extraDataSerializedObject.FindProperty("m_info.m_attributeTypeName");

            m_sources.serializedProperty = extraDataSerializedObject.FindProperty("m_info.m_sources");

            m_destinationPath = Utf8JsonResolverAssetEditorUtility.GetDestinationSourcePath(m_importer.assetPath, propertyResolverName.stringValue, propertyDestinationSource.objectReferenceValue as TextAsset);
            m_destinationPathAnotherExist = propertyDestinationSource.objectReferenceValue == null && File.Exists(m_destinationPath);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField(InfoName, EditorStyles.boldLabel);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Resolver", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(propertyResolverName);
            EditorGUILayout.PropertyField(propertyNamespaceRoot);
            EditorGUILayout.PropertyField(propertyResolverAsset);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Generate", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(propertyAutoGenerate);
            EditorGUILayout.PropertyField(propertyDestinationSource);
            EditorGUILayout.PropertyField(propertyIgnoreReadOnly);
            EditorGUILayout.PropertyField(propertyIgnoreEmpty);
            EditorGUILayout.PropertyField(propertyAttributeRequired);

            using (new EditorGUI.DisabledScope(!propertyAttributeRequired.boolValue))
            {
                Rect rect = EditorGUILayout.GetControlRect(true);
                Rect rectButton = EditorGUI.PrefixLabel(rect, new GUIContent("Attribute Type"));
                var type = Type.GetType(propertyAttributeTypeName.stringValue);
                GUIContent typeButtonContent = type != null ? new GUIContent(type.Name) : new GUIContent("None");

                if (EditorGUI.DropdownButton(rectButton, typeButtonContent, FocusType.Keyboard))
                {
                    m_dropdown.Show(rectButton);
                }
            }

            EditorGUILayout.Space();

            m_sources.DoLayoutList();

            extraDataSerializedObject.ApplyModifiedProperties();
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

        private void DrawHeader(Rect rect)
        {
            GUI.Label(rect, $"{m_sources.serializedProperty.displayName} (Size: {m_sources.serializedProperty.arraySize})", EditorStyles.boldLabel);
        }

        private static void DrawElement(ReorderableList list, Rect rect, int index, Type objectType)
        {
            rect.y += EditorGUIUtility.standardVerticalSpacing;
            rect.height = EditorGUIUtility.singleLineHeight;

            SerializedProperty propertyElement = list.serializedProperty.GetArrayElementAtIndex(index);

            DrawObjectField(rect, propertyElement, null, objectType);
        }

        private static void DrawObjectField(Rect rect, SerializedProperty serializedProperty, string label, Type objectType)
        {
            string guid = serializedProperty.stringValue;
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Object asset = AssetDatabase.LoadAssetAtPath(path, objectType);

            asset = string.IsNullOrEmpty(label)
                ? EditorGUI.ObjectField(rect, GUIContent.none, asset, objectType, false)
                : EditorGUI.ObjectField(rect, label, asset, objectType, false);

            path = AssetDatabase.GetAssetPath(asset);
            guid = AssetDatabase.AssetPathToGUID(path);

            serializedProperty.stringValue = guid;
        }

        private void OnDropdownTypeSelected(Type type)
        {
            SerializedProperty propertyAttributeTypeName = extraDataSerializedObject.FindProperty("m_info.m_attributeTypeName");

            propertyAttributeTypeName.stringValue = type.AssemblyQualifiedName;
            propertyAttributeTypeName.serializedObject.ApplyModifiedProperties();
        }

        private void OnAddSource(ReorderableList list)
        {
            list.serializedProperty.InsertArrayElementAtIndex(list.serializedProperty.arraySize);

            SerializedProperty propertyElement = list.serializedProperty.GetArrayElementAtIndex(list.serializedProperty.arraySize - 1);

            propertyElement.stringValue = string.Empty;
            propertyElement.serializedObject.ApplyModifiedProperties();
        }
    }
}
