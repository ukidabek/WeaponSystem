using System;

namespace WeaponSystem.Utility
{
    public class InitializeWeaponComponentAttribute : InitializationAttribute
    {
        public InitializeWeaponComponentAttribute() {}

        public InitializeWeaponComponentAttribute(Type type) : base(type) {}
    }
}