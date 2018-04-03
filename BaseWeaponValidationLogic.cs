using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace WeaponSystem
{
	public abstract class BaseWeaponValidationLogic : BaseWeaponLogic 
	{
		public abstract bool Validate();
	}
}