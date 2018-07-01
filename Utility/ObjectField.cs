using System;
using System.Reflection;

namespace WeaponSystem.Utility
{
    public struct ObjectFields<T> where T : Attribute
    {
        public object FieldsOwner;
        public FieldInfo[] FieldsToInitialize;

        public ObjectFields(object fieldsOwner) : this()
        {
            FieldsOwner = fieldsOwner;
            FieldsToInitialize = WeaponSystemUtility.GetAllFieldsWithAttribute(FieldsOwner.GetType(), typeof(T));
        }
    }
}