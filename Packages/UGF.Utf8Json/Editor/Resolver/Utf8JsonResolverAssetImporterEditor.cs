using System;
using System.IO;
using UGF.Code.Generate.Editor;
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
        protected override Type extraDataType { get; } = typeof(Utf8JsonResolverAssetData);

        private SerializedProperty m_propertyScript;
        private SerializedProperty m_propertyAutoGenerate;
        private SerializedProperty m_propertyName;
        private SerializedProperty m_propertyNamespaceRoot;
        private SerializedProperty m_propertyStaticCaching;
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
            m_propertyAutoGenerate = extraDataSerializedObject.FindProperty("m_autoGenerate");
            m_propertyName = extraDataSerializedObject.FindProperty("m_name");
            m_propertyNamespaceRoot = extraDataSerializedObject.FindProperty("m_namespaceRoot");
            m_propertyStaticCaching = extraDataSerializedObject.FindProperty("m_staticCaching");
            m_propertyResolverAsset = extraDataSerializedObject.FindProperty("m_resolverAsset");
            m_propertyIgnoreReadOnly = extraDataSerializedObject.FindProperty("m_ignoreReadOnly");
            m_propertyAttributeRequired = extraDataSerializedObject.FindProperty("m_attributeRequired");
            m_propertyAttributeTypeName = extraDataSerializedObject.FindProperty("m_attributeTypeName");

            float height = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing * 2F;

            SerializedProperty propertySources = extraDataSerializedObject.FindProperty("m_sources");

            m_sources = new ReorderableList(serializedObject, propertySources);
            m_sources.headerHeight = EditorGUIUtility.standardVerticalSpacing;
            m_sources.elementHeight = height;
            m_sources.drawElementCallback = (rect, index, active, focused) => DrawElement(m_sources, rect, index, typeof(Object));

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
            EditorGUILayout.PropertyField(m_propertyName);
            EditorGUILayout.PropertyField(m_propertyNamespaceRoot);
            EditorGUILayout.PropertyField(m_propertyStaticCaching);
            EditorGUILayout.PropertyField(m_propertyResolverAsset);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Generate", EditorStyles.boldLabel);
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
            var importer = (Utf8JsonResolverAssetImporter)targets[targetIndex];
            var textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(importer.assetPath);

            if (textAsset != null)
            {
                JsonUtility.FromJsonOverwrite(textAsset.text, extraData);
            }
        }

        protected override void Apply()
        {
            base.Apply();

            var data = (Utf8JsonResolverAssetData)extraDataTargets[0];
            var importer = (Utf8JsonResolverAssetImporter)targets[0];

            Utf8JsonResolverAssetEditorUtility.SaveResolverData(importer.assetPath, data);
        }

        protected override bool OnApplyRevertGUI()
        {
            var importer = (Utf8JsonResolverAssetImporter)targets[0];
            string path = CodeGenerateEditorUtility.GetPathForGeneratedScript(importer.assetPath, "Utf8Json");

            using (new EditorGUI.DisabledScope(HasModified()))
            {
                if (GUILayout.Button("Generate All"))
                {
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
                            AssetDatabase.MoveAssetToTrash(path);
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

            string guid = propertyElement.stringValue;
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Object asset = AssetDatabase.LoadAssetAtPath(path, objectType);

            asset = EditorGUI.ObjectField(rect, asset, objectType, false);
            path = AssetDatabase.GetAssetPath(asset);
            guid = AssetDatabase.AssetPathToGUID(path);

            propertyElement.stringValue = guid;
        }

        private void OnDropdownTypeSelected(Type type)
        {
            m_propertyAttributeTypeName.stringValue = type.AssemblyQualifiedName;
            m_propertyAttributeTypeName.serializedObject.ApplyModifiedProperties();
        }
    }
}
