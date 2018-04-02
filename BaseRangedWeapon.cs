using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace WeaponSystem
{
	public class BaseRangedWeapon : BaseWeapon 
	{
		// protected override bool CanBeUsed { get { return base.CanBeUsed && !Reloading; } }
		// protected virtual bool Reloading { get; set; }
		// [SerializeField] private List<BaseWeaponLogic> _reloadStartLogic = new List<BaseWeaponLogic>();
		// [SerializeField] private List<BaseWeaponLogic> _reloadEndLogic = new List<BaseWeaponLogic>();
		
		// public virtual void ReloadStart()
		// {
		// 	if(!Reloading)
		// 	{
		// 		PerformLogic(_reloadStartLogic);
		// 		Reloading = !Reloading;
		// 	}
		// }

		// public virtual void ReloadEnd()
		// {
		// 	if(Reloading)
		// 	{
		// 		PerformLogic(_reloadEndLogic);
		// 		Reloading = !Reloading;
		// 	}
		// }
	}
}