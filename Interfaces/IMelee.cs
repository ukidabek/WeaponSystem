using UnityEngine;

namespace WeaponSystem
{
    public interface IMelee
    {
        float Range { get; }
        Transform HitOrigin { get; set; }
    }
}