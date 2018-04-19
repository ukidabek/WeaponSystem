using UnityEngine;
using UnityEngine.Events;
using UnityEditor.Animations;

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

        [SerializeField] private AnimatorOverrideController _characterAnimatorController = null;
        public AnimatorOverrideController CharacterAnimatorController { get { return _characterAnimatorController; } }

        [SerializeField, Space] protected List<BaseWeaponValidationLogic> _useValidateLogic = new List<BaseWeaponValidationLogic>();
		[SerializeField] protected List<BaseWeaponLogic> _useLogicList = new List<BaseWeaponLogic>();

        public IWeaponStatistics [] Statistics { get; protected set; }

        private UnityEvent Used = new UnityEvent();

        public void AddUsedListener(UnityAction call)
        {
            Used.AddListener(call);
        }

        public void RemoveUsedListener(UnityAction call)
        {
            Used.RemoveListener(call);
        }

        protected virtual void Awake()
		{
			Statistics = GetAllStatistics();
		}

		public virtual bool Use()
		{
			if(ValidateLogic(_useValidateLogic))
			{
				PerformLogic(_useLogicList);
				Used.Invoke();
                return true;
			}

            return false;
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
