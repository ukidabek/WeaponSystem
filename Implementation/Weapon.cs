using UnityEngine;

using System;
using System.Reflection;
using System.Collections.Generic;

using WeaponSystem.Utility;

namespace WeaponSystem.Implementation
{
    using Object = UnityEngine.Object;

    public class Weapon : MonoBehaviour, IWeapon, IWeaponInitialization
    {
        [SerializeField, LogicObjects] protected List<Object> weaponLogicObjectList = new List<Object>();

        [LogicList] protected List<IWeaponLogic> _useLogic = new List<IWeaponLogic>();
        [LogicList] protected List<IWeaponValidationLogic> _useValidateLogicList = new List<IWeaponValidationLogic>();
        [LogicList] protected List<IWeaponInitialization> _weaponInitialization = new List<IWeaponInitialization>();

        [LogicList] protected List<IWeaponStatistics> _weaponSatisticks = new List<IWeaponStatistics>();
        protected FieldInfo[] logicListField = null;
        protected FieldInfo[] initializeFields = null;
        public List<IWeaponStatistics> WeaponSatisticks { get { return _weaponSatisticks; } }

        public GameObject GameObject { get { return this.gameObject; } }

        protected virtual void Awake()
        {
            logicListField = WeaponSystemUtility.GetAllFieldsWithAttribute(this.GetType(), typeof(LogicListAttribute));
            initializeFields = WeaponSystemUtility.GetAllFieldsWithAttribute(this.GetType(), typeof(InitializeWeaponComponentAttribute));
            WeaponSystemUtility.FillWeaponLogicList(this, logicListField, weaponLogicObjectList.ToArray());
        }

        public virtual void Initialize(params object[] data)
        {
            for (int i = 0; i < data.Length; i++)
                WeaponSystemUtility.FillRequirements<InitializeWeaponComponentAttribute>(this, this, initializeFields, data[i]);

            for (int i = 0; i < _weaponInitialization.Count; i++)
                _weaponInitialization[i].Initialize(data);

            WeaponSystemUtility.FillWeaponLogicList(this, logicListField, data);
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

        private void Reset()
        {
            List<Type> partsType = new List<Type>();
            var componentDictionary = new Dictionary<Type, Object>();
            WeaponSystemUtility.GetPartTypes(this, gameObject.transform, componentDictionary, weaponLogicObjectList);
        }
    }
}