using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem.Utility;

namespace WeaponSystem.Implementation.Firearm
{
    public class Firearm : RangeWeapon, IReloadable
    {
        [SerializeField, WeaponPart] ReloadLogic reloadLogic = null;

        public void AddReloadEndListener(UnityAction call)
        {
        }

        public void AddReloadStartListener(UnityAction call)
        {
        }

        public bool Reload(params object[] parameters)
        {
            reloadLogic.Reload(parameters);
            return false;
        }

        public void RemoveReloadEndtListener(UnityAction call)
        {
        }

        public void RemoveReloadStartListener(UnityAction call)
        {
        }
    }
}