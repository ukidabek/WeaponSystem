using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public interface IWeapon
    {
        GameObject GameObject { get; }

        bool Use(params object[] data);
        void Initialize(params object[] data);
    }
}