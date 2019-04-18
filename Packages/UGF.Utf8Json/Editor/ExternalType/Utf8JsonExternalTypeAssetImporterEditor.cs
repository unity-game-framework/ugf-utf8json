using System;
using UGF.Types.Editor;
using UGF.Types.Editor.IMGUI;
using UnityEditor;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

namespace UGF.Utf8Json.Editor.ExternalType
{
    [CustomEditor(typeof(Utf8JsonExternalTypeAssetImporter))]
    public class Utf8JsonExternalTypeAssetImporterEditor : ScriptedImporterEditor
    {
        public override bool showImportedObject { get; } = false;
        protected override bool useAssetDrawPreview { get; } = false;

        private Utf8JsonExternalTypeAssetImporter m_importer;
        private SerializedProperty m_propertyScript;
        private SerializedProperty m_propertyType;
        private SerializedProperty m_propertyMembers;
        private TypesDropdown m_dropdown;
        private Styles m_styles;

        private sealed class Styles
        {
            public readonly GUIContent TypeLabelContent = new GUIContent("Type");
        }

        public override void OnEnable()
        {
            base.OnEnable();

            m_importer = (Utf8JsonExternalTypeAssetImporter)target;
            m_propertyScript = serializedObject.FindProperty("m_Script");

            SerializedProperty asset = serializedObject.FindProperty("m_asset");

            m_propertyType = asset.FindPropertyRelative("m_type");
            m_propertyMembers = asset.FindPropertyRelative("m_members");
        }

        public override void OnInspectorGUI()
        {
            if (m_styles == null)
            {
                m_styles = new Styles();
            }

            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.PropertyField(m_propertyScript);
            }

            TypeDropdown();

            ApplyRevertGUI();
        }

        protected override void Apply()
        {
            base.Apply();

            m_importer.Save();
        }

        private void TypeDropdown()
        {
            Rect rect = EditorGUILayout.GetControlRect();
            Rect rectButton = EditorGUI.PrefixLabel(rect, m_styles.TypeLabelContent);

            var typeButtonContent = new GUIContent(m_propertyType.stringValue);

            if (EditorGUI.DropdownButton(rectButton, typeButtonContent, FocusType.Keyboard))
            {
                ShowDropdown(rectButton);
            }
        }

        private void ShowDropdown(Rect rect)
        {
            if (m_dropdown == null)
            {
                m_dropdown = TypesEditorGUIUtility.GetTypesDropdown(OnDropdownValidateType);
                m_dropdown.Selected += OnDropdownTypeSelected;
            }

            m_dropdown.Show(rect);
        }

        private bool OnDropdownValidateType(Type type)
        {
            return Utf8JsonExternalTypeEditorUtility.IsValidExternalType(type);
        }

        private void OnDropdownTypeSelected(Type type)
        {
            m_propertyType.stringValue = type.AssemblyQualifiedName;
            m_propertyType.serializedObject.ApplyModifiedProperties();
        }
    }
}
