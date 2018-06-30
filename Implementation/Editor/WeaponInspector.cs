using UnityEngine;
using UnityEditor;
using WeaponSystem.Utility;

namespace WeaponSystem.Implementation
{
    [CustomEditor(typeof(Weapon), true)]
    public class WeaponInspector : Editor
    {
        private Weapon weapon = null;

        private void OnEnable()
        {
            weapon = target as Weapon;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Get required components"))
                WeaponSystemUtility.FillRequirements<WeaponRequireComponentAttribute>(weapon);
        }
    }
}