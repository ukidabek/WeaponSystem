using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
	public abstract class BaseTransformWeaponLogic : BaseWeaponLogic 
	{
		[SerializeField] protected Transform transform = null;

		public void SetTransform(Object transform)
		{
			if(transform is Transform)
			{
				this.transform = transform as Transform;
			}
		}	
	}
}