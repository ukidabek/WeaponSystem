using System;

namespace WeaponSystem.Utility
{
    public class WeaponRequireComponentAttribute : Attribute
    {
        public Type Type { get; private set; }

        public WeaponRequireComponentAttribute() {}
        public WeaponRequireComponentAttribute(Type type)
        {
            Type = type;
        }
    }
}
