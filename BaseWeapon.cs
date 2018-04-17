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

		[SerializeField] protected List<BaseWeaponValidationLogic> _useValidateLogic = new List<BaseWeaponValidationLogic>();
		[SerializeField] protected List<BaseWeaponLogic> _useLogicList = new List<BaseWeaponLogic>();

		public IWeaponStatistics [] Statistics { get; protected set; }
		
		public UnityEvent Used = new UnityEvent();

		protected virtual void Awake()
		{
			Statistics = GetAllStatistics();
		}

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
				if(!item.Validate())
					return false;

			return  true; 
		}

		public IWeaponStatistics [] GetAllStatistics()
		{
			List<IWeaponStatistics> statistics = new List<IWeaponStatistics>();
			statistics.AddRange(GetComponentsInChildren<IWeaponStatistics>());

			return statistics.ToArray();
		}

		public virtual void Initialize() {}
    }
}
