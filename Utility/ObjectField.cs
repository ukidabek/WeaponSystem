using System.Reflection;

namespace WeaponSystem.Utility
{
    public struct ObjectField
    {
        public object FieldsOwner;
        public FieldInfo[] FieldsToInitialize;

        public ObjectField(object fieldsOwner) : this()
        {
            FieldsOwner = fieldsOwner;
            FieldsToInitialize = WeaponSystemUtility.GetAllFieldsWithAttribute(FieldsOwner.GetType(), typeof(InitializeWeaponComponentAttribute));
        }
    }
}