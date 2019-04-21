using System;
using System.IO;
using UGF.Code.Generate.Editor.Container;
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
            public readonly GUIStyle Box = new GUIStyle("Box");
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

            DrawTypeDropdown();
            DrawMembers();

            ApplyRevertGUI();
        }

        protected override void Apply()
        {
            base.Apply();

            SaveExtra();

            AssetDatabase.ImportAsset(m_importer.assetPath);
        }

        private void DrawTypeDropdown()
        {
            Rect rect = EditorGUILayout.GetControlRect();
            Rect rectButton = EditorGUI.PrefixLabel(rect, m_styles.TypeLabelContent);

            Type type = Type.GetType(m_propertyType.stringValue);
            GUIContent typeButtonContent = type != null ? new GUIContent(type.Name) : new GUIContent("None");

            if (EditorGUI.DropdownButton(rectButton, typeButtonContent, FocusType.Keyboard))
            {
                ShowDropdown(rectButton);
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Type Info", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(m_propertyType.stringValue, MessageType.None);
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

            // UpdateMembers(type);
        }

        private void DrawMembers()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Members", EditorStyles.boldLabel);

            using (new GUILayout.VerticalScope(m_styles.Box))
            {
                for (int i = 0; i < m_propertyMembers.arraySize; i++)
                {
                    SerializedProperty propertyMember = m_propertyMembers.GetArrayElementAtIndex(i);
                    SerializedProperty propertyName = propertyMember.FindPropertyRelative("m_name");
                    SerializedProperty propertyState = propertyMember.FindPropertyRelative("m_state");

                    propertyState.boolValue = EditorGUILayout.Toggle(propertyName.stringValue, propertyState.boolValue);
                }

                if (m_propertyMembers.arraySize == 0)
                {
                    EditorGUILayout.LabelField("Empty");
                }

                EditorGUILayout.Space();

                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Select all"))
                    {
                        SetMembersState(true);
                    }

                    if (GUILayout.Button("Deselect all"))
                    {
                        SetMembersState(false);
                    }

                    GUILayout.FlexibleSpace();
                }
            }
        }

        private void SetMembersState(bool state)
        {
            for (int i = 0; i < m_propertyMembers.arraySize; i++)
            {
                SerializedProperty propertyMember = m_propertyMembers.GetArrayElementAtIndex(i);
                SerializedProperty propertyState = propertyMember.FindPropertyRelative("m_state");

                propertyState.boolValue = state;
            }
        }

        private void UpdateMembers(string typeName)
        {
            m_propertyMembers.ClearArray();

            Type type = Type.GetType(typeName);

            if (type != null && Utf8JsonExternalTypeEditorUtility.IsValidExternalType(type))
            {
            }

            CodeGenerateContainer container = null;

            for (int i = 0; i < container.Fields.Count; i++)
            {
                CodeGenerateContainerField field = container.Fields[i];

                m_propertyMembers.InsertArrayElementAtIndex(m_propertyMembers.arraySize);

                SerializedProperty propertyMember = m_propertyMembers.GetArrayElementAtIndex(m_propertyMembers.arraySize - 1);
                SerializedProperty propertyName = propertyMember.FindPropertyRelative("m_name");
                SerializedProperty propertyState = propertyMember.FindPropertyRelative("m_state");

                propertyName.stringValue = field.Name;
                propertyState.boolValue = true;
            }
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
    }
}
