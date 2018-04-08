using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
	public abstract class BaseTransformWeaponLogic : BaseWeaponLogic 
	{
		[SerializeField] protected Transform _weaponLogicTransform = null;

		public void SetTransform(Object transform)
		{
			if(transform is Transform)
			{
				this._weaponLogicTransform = transform as Transform;
			}
		}	
	}
}