using System;
using System.IO;
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

        private SerializedObject m_extraSerializedObject;
        private AssetImporter m_importer;
        private SerializedProperty m_propertyScript;
        private SerializedProperty m_propertyType;
        private SerializedProperty m_propertyMembers;
        private TypesDropdown m_dropdown;
        private Styles m_styles;

        private sealed class Styles
        {
            public readonly GUIContent TypeLabelContent = new GUIContent("Type");
        }

        private sealed class Extra : ScriptableObject
        {
            [SerializeField] private Utf8JsonExternalTypeAssetInfo m_info = new Utf8JsonExternalTypeAssetInfo();

            public Utf8JsonExternalTypeAssetInfo Info { get { return m_info; } }
        }

        public override void OnEnable()
        {
            base.OnEnable();

            m_extraSerializedObject = new SerializedObject(CreateInstance<Extra>());
            m_importer = (AssetImporter)target;

            SerializedProperty propertyInfo = m_extraSerializedObject.FindProperty("m_info");

            m_propertyScript = serializedObject.FindProperty("m_Script");
            m_propertyType = propertyInfo.FindPropertyRelative("m_type");
            m_propertyMembers = propertyInfo.FindPropertyRelative("m_members");

            LoadExtra();
        }

        public override void OnDisable()
        {
            base.OnDisable();

            DestroyImmediate(m_extraSerializedObject.targetObject);

            m_extraSerializedObject.Dispose();
        }

        public override bool HasModified()
        {
            return base.HasModified() || m_extraSerializedObject.hasModifiedProperties;
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

            SaveExtra();

            AssetDatabase.ImportAsset(m_importer.assetPath);
        }

        private void LoadExtra()
        {
            var asset = (TextAsset)assetTarget;
            var extra = (Extra)m_extraSerializedObject.targetObject;

            JsonUtility.FromJsonOverwrite(asset.text, extra.Info);

            m_extraSerializedObject.Update();
        }

        private void SaveExtra()
        {
            m_extraSerializedObject.ApplyModifiedProperties();

            var extra = (Extra)m_extraSerializedObject.targetObject;
            string text = JsonUtility.ToJson(extra.Info, true);

            File.WriteAllText(m_importer.assetPath, text);
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
        }
    }
}
