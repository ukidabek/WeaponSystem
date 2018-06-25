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

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 2;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var currentPosition = position;
            currentPosition.height = EditorGUIUtility.singleLineHeight;
            SerializedProperty animatorProperty = property.FindPropertyRelative("Animator");
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

            EditorGUI.Popup(currentPosition, 0, parameters.ToArray());
        }
    }
}