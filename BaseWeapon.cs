using UnityEngine;
using UnityEngine.Events;

using System.Collections;
using System.Collections.Generic;
using System;

namespace WeaponSystem
{
	public abstract class BaseWeapon : MonoBehaviour 
	{
		protected virtual bool CanBeUsed 
		{ 
			get 
			{ 
				foreach (var item in _useValidateLogic)
					if(!item.Validate())
						return false;

				return  true; 
			} 
		}

		[SerializeField] private List<BaseWeaponValidationLogic> _useValidateLogic = new List<BaseWeaponValidationLogic>();
		[SerializeField] private List<BaseWeaponLogic> _useLogicList = new List<BaseWeaponLogic>();
		
		public UnityEvent Used = new UnityEvent();
		public virtual void Use()
		{
			if(ValidateLogic(_useValidateLogic))
			{
				PerformLogic(_useLogicList);
				Used.Invoke();
			}
		}

        public static void PerformLogic(List<BaseWeaponLogic> logicList)
        {
			if(logicList.Count == 0) return;
			
            foreach (BaseWeaponLogic item in logicList)
			{
				item.Perform();
			}
        }

		public static bool ValidateLogic(List<BaseWeaponValidationLogic> validateLogic)
		{
			foreach (var item in validateLogic)
			{
				if(!item.Validate())
				{
					return false;
				}
			}

			return  true; 
		}
    }
}
