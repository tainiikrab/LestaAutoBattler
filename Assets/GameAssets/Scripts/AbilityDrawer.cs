#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(AbilityAbstract), true)]
public class AbilityDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var current = property.managedReferenceValue as AbilityAbstract;

        var dropdownRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

        if (EditorGUI.DropdownButton(dropdownRect, new GUIContent(current != null ? current.GetType().Name : "None"),
                FocusType.Keyboard))
        {
            var menu = new GenericMenu();

            menu.AddItem(new GUIContent("None"), current == null, () =>
            {
                property.managedReferenceValue = null;
                property.serializedObject.ApplyModifiedProperties();
            });

            foreach (var type in TypeCache.GetTypesDerivedFrom<AbilityAbstract>())
            {
                if (type.IsAbstract) continue;

                menu.AddItem(new GUIContent(type.Name), current != null && current.GetType() == type, () =>
                {
                    property.managedReferenceValue = Activator.CreateInstance(type);
                    property.serializedObject.ApplyModifiedProperties();
                });
            }

            menu.ShowAsContext();
        }

        if (current != null)
        {
            var propertyRect = new Rect(position.x,
                position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing,
                position.width, position.height);
            EditorGUI.PropertyField(propertyRect, property, GUIContent.none, true);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property.managedReferenceValue == null) return EditorGUIUtility.singleLineHeight;

        return EditorGUI.GetPropertyHeight(property, true) + EditorGUIUtility.singleLineHeight +
               EditorGUIUtility.standardVerticalSpacing;
    }
}
#endif