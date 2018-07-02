using UnityEngine;

using UnityEngine.Events;

using WeaponSystem.Utility;
using WeaponSystem.Implementation.Animations;
using WeaponSystem.Interfaces.Weapon;

namespace WeaponSystem.Implementation.Firearm
{
    public class Firearm : RangeWeapon, IReloadable, IAimable, IAmmunition
    {
        [SerializeField, Space, WeaponPart, InitializeWeaponComponent(typeof(AmmunitionStock))]
        private ReloadLogic reloadLogic = null;

        public UnityEvent ReloadCallback { get { return reloadLogic.ReloadCallback; } }

        public int ClipStatus { get { return reloadLogic.Clip.Counter; } }

        public float ClipProcentage { get { return (float)reloadLogic.Clip.Counter / (float)reloadLogic.Clip.Size; } }

        public int AmmunitionStatus { get { return reloadLogic.Stack.Resource; } }

        public float AmmunitionProcentage { get { return (float)reloadLogic.Stack.Resource / (float)reloadLogic.Stack.MaxRosourceCount; } }

        [SerializeField, InitializeWeaponComponent(typeof(Animator))] private AnimationBool _aim = new AnimationBool();

        public bool Reload()
        {
            reloadLogic.Reload();
            return true;
        }

        public void Aim()
        {
            _aim.Set(true);
        }

        public void AimOff()
        {
            _aim.Set(false);
        }
    }
}