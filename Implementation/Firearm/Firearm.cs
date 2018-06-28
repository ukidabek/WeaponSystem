using UnityEngine;

using System.Collections;
using System.Collections.Generic;

using WeaponSystem.Utility;
using UnityEngine.Events;

namespace WeaponSystem.Implementation.Firearm
{
    public class Firearm : RangeWeapon, IReloadable
    {
        [SerializeField, Space, WeaponPart] ReloadLogic reloadLogic = null;

        public UnityEvent ReloadCallback { get { return reloadLogic.ReloadCallback; } }

        public bool Reload()
        {
            reloadLogic.Reload();
            return true;
        }
    }
}