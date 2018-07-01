using UnityEngine;

using WeaponSystem.Utility;
using UnityEngine.Events;

namespace WeaponSystem.Implementation.Firearm
{
    public class Firearm : RangeWeapon, IReloadable, IAimable
    {
        [SerializeField, Space, WeaponPart, InitializeWeaponComponent(typeof(AmmunitionStock))]
        private ReloadLogic reloadLogic = null;

        public UnityEvent ReloadCallback { get { return reloadLogic.ReloadCallback; } }

        public bool Reload()
        {
            reloadLogic.Reload();
            return true;
        }

        public void Aim() {}

        public void AimOff() {}
    }
}