using UnityEngine;
using UnityEngine.Events;

using System;
using WeaponSystem.Interfaces.Logic;
using WeaponSystem.Interfaces.Weapon;

namespace WeaponSystem
{
    using Object = UnityEngine.Object;

    [Serializable]
    public class WeaponHandler : MonoBehaviour
    {
        [Space, Header("Weapon handling data")]
        [SerializeField] private Object[] _initializeData = null;
        [SerializeField] private Object[] _useData = null;

        [Header("Unity events")]
        public UnityEvent OnUseMeleeWeapon = new UnityEvent();
        public UnityEvent OnUseRangedWeapon = new UnityEvent();
        public UnityEvent OnReloadWeapon = new UnityEvent();
        public OnWeaponAim OnAimWeapon = new OnWeaponAim();
        public AmmunitionUpdateCallback OnAmmunitionUpdate = new AmmunitionUpdateCallback();
        public AmmunitionProcentaeUpdateCallback AmmunitionProcentaeUpdate = new AmmunitionProcentaeUpdateCallback();

        private IWeapon _currentWeapon = null;
        private IRange _rangeWeapon = null;
        private IMelee _meleeWeapon = null;
        private IReloadable _reloadableWeapon = null;
        private IAimable _aimableWeapon = null;
        private IAmmunition _ammunition = null;

        public void EquipWeapon(IWeapon baseWeapon)
        {
            _currentWeapon = baseWeapon;
            baseWeapon.GameObject.SetActive(true);
            HandleReloadableWeapon(baseWeapon);
            HandleAimableWeapon(baseWeapon);
            HandleWeaponInitialization(baseWeapon);
            HandleWeaponAmmunition(baseWeapon);
        }

        private void HandleAimableWeapon(IWeapon baseWeapon)
        {
            if (baseWeapon is IAimable)
                _aimableWeapon = baseWeapon as IAimable;
        }

        private void HandleReloadableWeapon(IWeapon baseWeapon)
        {
            if (baseWeapon is IReloadable)
            {
                _reloadableWeapon = baseWeapon as IReloadable;
                _reloadableWeapon.ReloadCallback.AddListener(Reload);
            }
        }

        private void HandleWeaponInitialization(IWeapon baseWeapon)
        {
            if (baseWeapon is IWeaponInitialization)
                (baseWeapon as IWeaponInitialization).Initialize(_initializeData);
        }

        private void HandleWeaponAmmunition(IWeapon baseWeapon)
        {
            if (baseWeapon is IAmmunition)
                _ammunition = (baseWeapon as IAmmunition);
        }

        public void UseWeapon()
        {
            if (_currentWeapon != null && _currentWeapon.Use(_useData))
            {
                if (_rangeWeapon != null)
                    OnUseRangedWeapon.Invoke();

                else if (_meleeWeapon != null)
                    OnUseMeleeWeapon.Invoke();
            }
        }

        public void Aim()
        {
            if(_aimableWeapon != null)
            {
                _aimableWeapon.Aim();
                OnAimWeapon.Invoke(true);
            }
        }

        public void AimOff()
        {
            if (_aimableWeapon != null)
            {
                _aimableWeapon.AimOff();
                OnAimWeapon.Invoke(false);
            }
        }

        public void Reload()
        {
            if (_reloadableWeapon != null && _reloadableWeapon.Reload())
                OnReloadWeapon.Invoke();
        }

        private void Update()
        {
            if (_ammunition != null)
            {
                OnAmmunitionUpdate.Invoke(_ammunition.ClipStatus, _ammunition.AmmunitionStatus);
                AmmunitionProcentaeUpdate.Invoke(_ammunition.ClipProcentage, _ammunition.AmmunitionProcentage);
            }
        }
    }

    [Serializable] public class AmmunitionUpdateCallback : UnityEvent<int, int> {}
    [Serializable] public class AmmunitionProcentaeUpdateCallback : UnityEvent<float, float> { }

}