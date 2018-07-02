using UnityEngine;

namespace WeaponSystem.Interfaces.Weapon
{
    public interface IRange
    {
        Transform ShotOrigin { get; set; }
    }
}