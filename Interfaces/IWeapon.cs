using UnityEngine;

namespace WeaponSystem
{
    public interface IWeapon
    {
        GameObject GameObject { get; }

        bool Use(params object[] data);
    }
}