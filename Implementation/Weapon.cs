﻿using UnityEngine;

using System;
using System.Reflection;
using System.Collections.Generic;

using WeaponSystem.Utility;
using WeaponSystem.Interfaces.Logic;
using WeaponSystem.Interfaces.Weapon;

namespace WeaponSystem.Implementation
{
    using Object = UnityEngine.Object;

    public class Weapon : MonoBehaviour, IWeapon, IWeaponInitialization
    {
        public GameObject GameObject { get { return this.gameObject; } }

        [SerializeField, LogicObjects] protected List<Object> weaponLogicObjectList = new List<Object>();

        [LogicList] protected List<IWeaponLogic> _useLogic = new List<IWeaponLogic>();
        [LogicList] protected List<IWeaponValidationLogic> _useValidateLogicList = new List<IWeaponValidationLogic>();
        [LogicList] protected List<IWeaponInitialization> _weaponInitialization = new List<IWeaponInitialization>();

        [LogicList] protected List<IWeaponStatistics> _weaponSatisticks = new List<IWeaponStatistics>();
        protected FieldInfo[] logicListField = null;

        protected List<ObjectFields<InitializeWeaponComponentAttribute>> objectFieldList = new List<ObjectFields<InitializeWeaponComponentAttribute>>();

        public List<IWeaponStatistics> WeaponSatisticks { get { return _weaponSatisticks; } }

        protected virtual void Awake()
        {
            logicListField = WeaponSystemUtility.GetAllFieldsWithAttribute(this.GetType(), typeof(LogicListAttribute));

            for (int i = 0; i < weaponLogicObjectList.Count; i++)
                objectFieldList.Add(new ObjectFields<InitializeWeaponComponentAttribute>(weaponLogicObjectList[i]));
            objectFieldList.Add(new ObjectFields<InitializeWeaponComponentAttribute>(this));

            WeaponSystemUtility.FillWeaponLogicList(this, logicListField, weaponLogicObjectList.ToArray());
        }

        public virtual void Initialize(params object[] data)
        {
            for (int i = 0; i < objectFieldList.Count; i++)
                WeaponSystemUtility.FillListOfFields(objectFieldList[i].FieldsOwner, objectFieldList[i].FieldsToInitialize, data);

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