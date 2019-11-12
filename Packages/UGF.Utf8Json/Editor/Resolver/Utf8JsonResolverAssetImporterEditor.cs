using System;
using System.IO;
using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;
using UnityEditor.Experimental.AssetImporters;
using UnityEditorInternal;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.Utf8Json.Editor.Resolver
{
    [CustomEditor(typeof(Utf8JsonResolverAssetImporter))]
    public class Utf8JsonResolverAssetImporterEditor : ScriptedImporterEditor
    {
        public override bool showImportedObject { get; } = false;
        protected override Type extraDataType { get; } = typeof(Utf8JsonResolverAssetImporterData);

        private SerializedProperty m_propertyScript;
        private SerializedProperty m_propertyAutoGenerate;
        private SerializedProperty m_propertyResolverName;
        private SerializedProperty m_propertyNamespaceRoot;
        private SerializedProperty m_propertyDestinationSource;
        private SerializedProperty m_propertyResolverAsset;
        private SerializedProperty m_propertyIgnoreReadOnly;
        private SerializedProperty m_propertyAttributeRequired;
        private SerializedProperty m_propertyAttributeTypeName;
        private ReorderableList m_sources;
        private TypesDropdownDrawer m_dropdown;

        public override void OnEnable()
        {
            base.OnEnable();

            m_propertyScript = serializedObject.FindProperty("m_Script");
            m_propertyAutoGenerate = extraDataSerializedObject.FindProperty("m_info.m_autoGenerate");
            m_propertyResolverName = extraDataSerializedObject.FindProperty("m_info.m_resolverName");
            m_propertyNamespaceRoot = extraDataSerializedObject.FindProperty("m_info.m_namespaceRoot");
            m_propertyDestinationSource = extraDataSerializedObject.FindProperty("m_info.m_destinationSource");
            m_propertyResolverAsset = extraDataSerializedObject.FindProperty("m_info.m_resolverAsset");
            m_propertyIgnoreReadOnly = extraDataSerializedObject.FindProperty("m_info.m_ignoreReadOnly");
            m_propertyAttributeRequired = extraDataSerializedObject.FindProperty("m_info.m_attributeRequired");
            m_propertyAttributeTypeName = extraDataSerializedObject.FindProperty("m_info.m_attributeTypeName");

            float height = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing * 2F;

            SerializedProperty propertySources = extraDataSerializedObject.FindProperty("m_info.m_sources");

            m_sources = new ReorderableList(serializedObject, propertySources);
            m_sources.headerHeight = EditorGUIUtility.standardVerticalSpacing;
            m_sources.elementHeight = height;
            m_sources.drawElementCallback = (rect, index, active, focused) => DrawElement(m_sources, rect, index, typeof(TextAsset));

            m_dropdown = new TypesDropdownDrawer(m_propertyAttributeTypeName, () => TypeCache.GetTypesDerivedFrom<Attribute>());
            m_dropdown.Selected += OnDropdownTypeSelected;
        }

        public override void OnDisable()
        {
            base.OnDisable();

            if (m_dropdown != null)
            {
                m_dropdown.Selected -= OnDropdownTypeSelected;
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();
            extraDataSerializedObject.UpdateIfRequiredOrScript();

            using (new EditorGUI.DisabledScope(false))
            {
                EditorGUILayout.PropertyField(m_propertyScript);
            }

            EditorGUILayout.PropertyField(m_propertyAutoGenerate);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Resolver", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(m_propertyResolverName);
            EditorGUILayout.PropertyField(m_propertyNamespaceRoot);
            EditorGUILayout.PropertyField(m_propertyResolverAsset);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Generate", EditorStyles.boldLabel);

            DrawObjectField(m_propertyDestinationSource, m_propertyDestinationSource.displayName, typeof(TextAsset));

            EditorGUILayout.PropertyField(m_propertyIgnoreReadOnly);
            EditorGUILayout.PropertyField(m_propertyAttributeRequired);

            using (new EditorGUI.DisabledScope(!m_propertyAttributeRequired.boolValue))
            {
                m_dropdown.DrawGUILayout(new GUIContent(m_propertyAttributeTypeName.displayName));
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Sources", EditorStyles.boldLabel);

            m_sources.DoLayoutList();

            serializedObject.ApplyModifiedProperties();
            extraDataSerializedObject.ApplyModifiedProperties();

            ApplyRevertGUI();
        }

        protected override void InitializeExtraDataInstance(Object extraData, int targetIndex)
        {
            var data = (Utf8JsonResolverAssetImporterData)extraData;
            var importer = (Utf8JsonResolverAssetImporter)targets[targetIndex];
            Utf8JsonResolverAssetInfo info = Utf8JsonResolverAssetEditorUtility.LoadResolverInfo(importer.assetPath);

            data.Info = info;
        }

        protected override void Apply()
        {
            base.Apply();

            var data = (Utf8JsonResolverAssetImporterData)extraDataTargets[0];
            var importer = (Utf8JsonResolverAssetImporter)targets[0];

            Utf8JsonResolverAssetEditorUtility.SaveResolverInfo(importer.assetPath, data.Info);
        }

        protected override bool OnApplyRevertGUI()
        {
            var importer = (Utf8JsonResolverAssetImporter)targets[0];
            string path = Utf8JsonResolverAssetEditorUtility.GetDestinationSourcePath(importer.assetPath, m_propertyResolverName.stringValue, m_propertyDestinationSource.stringValue);

            using (new EditorGUI.DisabledScope(HasModified()))
            {
                if (GUILayout.Button("Generate All"))
                {
                    Utf8JsonResolverAssetEditorUtility.GenerateResolverAll();
                }

                if (GUILayout.Button("Generate"))
                {
                    Utf8JsonResolverAssetEditorUtility.GenerateResolver(importer.assetPath);
                }

                using (new EditorGUI.DisabledScope(!File.Exists(path)))
                {
                    if (GUILayout.Button("Clear"))
                    {
                        if (EditorUtility.DisplayDialog("Delete Utf8Json Generated Script?", $"{path}\nYou cannot undo this action.", "Delete", "Cancel"))
                        {
                            Utf8JsonResolverAssetEditorUtility.ClearResolver(importer.assetPath);
                        }
                    }
                }
            }

            EditorGUILayout.Space();

            return base.OnApplyRevertGUI();
        }

        private static void DrawElement(ReorderableList list, Rect rect, int index, Type objectType)
        {
            rect.y += EditorGUIUtility.standardVerticalSpacing;
            rect.height = EditorGUIUtility.singleLineHeight;

            SerializedProperty propertyElement = list.serializedProperty.GetArrayElementAtIndex(index);

            DrawObjectField(rect, propertyElement, null, objectType);
        }

        private static void DrawObjectField(SerializedProperty serializedProperty, string label, Type objectType)
        {
            Rect rect = EditorGUILayout.GetControlRect(!string.IsNullOrEmpty(label));

            DrawObjectField(rect, serializedProperty, label, objectType);
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
            m_propertyAttributeTypeName.stringValue = type.AssemblyQualifiedName;
            m_propertyAttributeTypeName.serializedObject.ApplyModifiedProperties();
        }
    }
}
