using UnityEngine;

namespace WeaponSystem.Interfaces.Weapon
{
    public interface IWeapon
    {
        GameObject GameObject { get; }

        bool Use(params object[] data);
    }
}