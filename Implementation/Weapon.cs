using UnityEngine;

using System.Collections.Generic;

using WeaponSystem.Utility;
using System;
using System.Reflection;

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
        public List<IWeaponStatistics> WeaponSatisticks { get { return _weaponSatisticks; } }

        public GameObject GameObject { get { return this.gameObject; } }

        protected virtual void Awake()
        {
            WeaponSystemUtility.FillWeaponLogicList(this);
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

        private void Reset()
        {
            List<Type> partsType = new List<Type>();
            var componentDictionary = new Dictionary<Type, Object>();
            GetPartTypes(this, componentDictionary);

            //for (int i = 0; i < parts.Length; i++)
            //    if (!partsType.Contains(parts[i].FieldType))
            //        partsType.Add(parts[i].FieldType);


            //for (int i = 0; i < partsType.Count; i++)
            //    componentDictionary.Add(partsType[i], AddPart(partsType[i]));

            //for (int i = 0; i < parts.Length; i++)
            //{
            //    parts[i].SetValue()
            //}
        }

        public void GetPartTypes(object instance, Dictionary<Type, Object> dictionary)
        {
            List<FieldInfo> parts = new List<FieldInfo>();
            parts.AddRange(WeaponSystemUtility.GetAllFieldsWithAttribute(instance.GetType(), typeof(WeaponPartAttribute)));

            for (int i = 0; i < parts.Count; i++)
            {
                Object @object = null;
                if(dictionary.TryGetValue(parts[i].FieldType, out @object))
                {
                    parts[i].SetValue(instance, @object);
                }
                else
                {
                    @object = AddPart(parts[i].FieldType);
                    parts[i].SetValue(instance, @object);
                    dictionary.Add(parts[i].FieldType, @object);
                    GetPartTypes(@object, dictionary);
                }
            }
        }

        public Object AddPart(Type partType)
        {
            GameObject gameObject = new GameObject();

            gameObject.transform.SetParent(this.transform);
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.identity;

            gameObject.name = partType.Name;

            var component = gameObject.AddComponent(partType);

            weaponLogicObjectList.Add(component);

            return component;
        }
    }
}