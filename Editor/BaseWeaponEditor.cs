using UnityEngine;
using UnityEditor;

using System.Collections;
using System.Collections.Generic;

namespace WeaponSystem
{
	[CustomEditor(typeof(BaseWeapon), true)]
	public class BaseWeaponEditor : Editor 
	{
		private BaseWeapon weapon = null;
		private IWeaponStatistics[] statistics = null;

		private void OnEnable() 
		{
			weapon = target as BaseWeapon;
			statistics = weapon.GetAllStatistics();
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Statistics", EditorStyles.boldLabel);

			for (int i = 0; i < statistics.Length; i++)
			{
				for (int j = 0; j < statistics[i].Name.Length; j++)
				{
					EditorGUILayout.BeginHorizontal();
					{
						EditorGUILayout.LabelField(statistics[i].Name[j]);
						EditorGUILayout.LabelField(statistics[i].Value[j]);
					}
					EditorGUILayout.EndHorizontal();
				}
			}
		}
	}
}