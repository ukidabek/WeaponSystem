using UnityEngine;

namespace WeaponSystem.Interfaces.Weapon
{
    public interface IMelee
    {
        float Range { get; }
        Transform HitOrigin { get; set; }
    }
}