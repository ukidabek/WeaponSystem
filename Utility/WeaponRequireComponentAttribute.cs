using System;

namespace WeaponSystem.Utility
{
    public abstract class InitializationAttribute : Attribute
    {
        public Type Type { get; private set; }

        public InitializationAttribute() { }
        public InitializationAttribute(Type type)
        {
            Type = type;
        }
    }

    public class WeaponRequireComponentAttribute : InitializationAttribute
    {
        public WeaponRequireComponentAttribute() {}

        public WeaponRequireComponentAttribute(Type type) : base(type) {}
    }
}
