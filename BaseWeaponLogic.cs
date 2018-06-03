using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
	public abstract class BaseWeaponLogic : MonoBehaviour 
	{
		public abstract void Perform(params object[] data);
	}
}