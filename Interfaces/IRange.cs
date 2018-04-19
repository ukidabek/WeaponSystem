using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public interface IRange
    {
        Transform ShotOrigin { get; set; }
    }
}