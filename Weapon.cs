using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace WeaponSystem
{
    public class Weapon : MonoBehaviour, IWeapon, IWeaponInitialization
    {
        [SerializeField] protected List<Object> weaponLogicObjectList = new List<Object>();
        
        protected List<IWeaponLogic> _useLogic = new List<IWeaponLogic>();
        protected List<IWeaponValidationLogic> _useValidateLogicList = new List<IWeaponValidationLogic>();
        protected List<IWeaponInitialization> _weaponInitialization = new List<IWeaponInitialization>();

        protected List<IWeaponStatistics> _weaponSatisticks = new List<IWeaponStatistics>();

        public GameObject GameObject { get { return this.gameObject; } }

        protected virtual void Awake()
        {
            for (int i = 0; i < weaponLogicObjectList.Count; i++)
            {
                Add(_useLogic, weaponLogicObjectList[i]);
                Add(_useValidateLogicList, weaponLogicObjectList[i]);
                Add(_weaponInitialization, weaponLogicObjectList[i]);
                Add(_weaponSatisticks, weaponLogicObjectList[i]);
            }
        }

        protected void Add<T>(List<T> list, Object gameObject) where T : class
        {
            if (gameObject is T)
                list.Add(gameObject as T);
        }

        public void Initialize(params object[] data)
        {
            for (int i = 0; i < _weaponInitialization.Count; i++)
                _weaponInitialization[i].Initialize(data);
        }

        protected bool ValidateLogic()
        {
            for (int i = 0; i < _useValidateLogicList.Count; i++)            
                if (!_useValidateLogicList[i].Validate())
                    return false;

            return true;
        }

        public void PerformLogic(params object[] data)
        {
            if (_useLogic.Count == 0)
                return;

            for (int i = 0; i < _useLogic.Count; i++)
                _useLogic[i].Perform(data);
        }

        public bool Use(params object[] data)
        {
            if(ValidateLogic())
            {
                PerformLogic(data);
                return true;
            }

            return false;
        }
    }
}