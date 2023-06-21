using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Extensions.Interface
{
    [CustomPropertyDrawer(typeof(InterfaceProperty<>))]
    public class InterfacePropertyDrawer : PropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var interfaceField = property.FindPropertyRelative("field");
            var genericArguments = fieldInfo.FieldType.GetGenericArguments();
            var genericArgumentT = genericArguments[0];
            
            EditorGUI.PropertyField(position, interfaceField,new GUIContent($"{property.displayName} ({genericArgumentT.Name})"));
            if (!GUI.changed) return;

            
            // コンポーネントが指定したインターフェースを継承していない場合は参照を外す
            if (interfaceField.objectReferenceValue!=null&&!interfaceField.objectReferenceValue.GetType().GetInterfaces().Contains(genericArgumentT))
            {
                var component = interfaceField.objectReferenceValue.GetComponent(genericArgumentT);
                interfaceField.objectReferenceValue = component != null ? component : null;
            }

            interfaceField.serializedObject.ApplyModifiedProperties();
        }
    }
}