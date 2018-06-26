using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace WeaponSystem.Utility
{
    [CustomPropertyDrawer(typeof(AnimatorHandler))]
    public class AnimatorHandlerPropertyDrawer : PropertyDrawer
    {
        private List<string> parameters = new List<string>();
        private int index = 0;
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 2;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty animatorProperty = property.FindPropertyRelative("Animator");
            SerializedProperty nameProperty = property.FindPropertyRelative("_name");
            SerializedProperty nameHashProperty = property.FindPropertyRelative("_nameHash");

            var currentPosition = position;
            currentPosition.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(currentPosition, animatorProperty, new GUIContent("Animator"));
            currentPosition.y += EditorGUIUtility.singleLineHeight;

            Animator animatorController = animatorProperty.objectReferenceValue as Animator;
            if (animatorController != null)
            {
                if (animatorController.parameters.Length != parameters.Count)
                {
                    parameters.Clear();
                    foreach (var item in animatorController.parameters)
                    {
                        parameters.Add(item.name);
                    }
                }
            }
            else
            {
                nameProperty.stringValue = string.Empty;
                nameHashProperty.intValue = 0;
            }

            currentPosition.width = position.width / 2;
            EditorGUI.LabelField(currentPosition, "Parameter name: ");
            index = parameters.IndexOf(nameProperty.stringValue);
            currentPosition.x = position.width / 2;
            currentPosition.width = position.width / 2;
            index = EditorGUI.Popup(currentPosition, index, parameters.ToArray());

            if(index > -1)
            {
                nameProperty.stringValue = parameters[index];
                nameHashProperty.intValue = Animator.StringToHash(parameters[index]);
            }
        }
    }
}