using UnityEngine;

namespace WeaponSystem
{
    public interface IRange
    {
        Transform ShotOrigin { get; set; }
    }
}