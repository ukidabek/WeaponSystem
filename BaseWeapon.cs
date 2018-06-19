using UnityEngine;
using UnityEngine.Events;

using System;
using System.Collections;
using System.Collections.Generic;

namespace WeaponSystem
{
	public abstract class BaseWeapon : MonoBehaviour, IWeapon
    {
        [Space]
        [SerializeField] protected List<BaseWeaponValidationLogic> _useValidateLogic = new List<BaseWeaponValidationLogic>();
		[SerializeField] protected List<BaseWeaponLogic> _useLogicList = new List<BaseWeaponLogic>();

        public IWeaponStatistics [] Statistics { get; protected set; }

        public GameObject GameObject { get { return this.gameObject; } }

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

		public virtual bool Use(params object [] data)
		{
			if(ValidateLogic(_useValidateLogic) && _useLogicList.Count != 0)
			{
				PerformLogic(_useLogicList, data);
				Used.Invoke();
                return true;
			}

            return false;
		}

        public static void PerformLogic(List<BaseWeaponLogic> logicList, params object[] data)
        {
			if(logicList.Count == 0) return;

            for (int i = 0; i < logicList.Count; i++)
                logicList[i].Perform(data);
        }

		protected static bool ValidateLogic(List<BaseWeaponValidationLogic> validateLogic)
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

        public virtual void Initialize(params object[] data)
        {
            for (int i = 0; i < _useValidateLogic.Count; i++)
                _useValidateLogic[i].Initialize(data);

            for (int i = 0; i < _useValidateLogic.Count; i++)
                _useLogicList[i].Initialize(data);
        }
    }
}
