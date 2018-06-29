using UnityEngine;
using UnityEngine.Events;

using System;

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

        private bool _isAming = false;

        private IWeapon _currentWeapon = null;
        private IRange _rangeWeapon = null;
        private IMelee _meleeWeapon = null;
        private IReloadable _reloadableWeapon = null;
        private IAimable _aimableWeapon = null;

        public void EquipWeapon(IWeapon baseWeapon)
        {
            _currentWeapon = baseWeapon;
            baseWeapon.GameObject.SetActive(true);
            HandleReloadableWeapon(baseWeapon);
            HandleAimableWeapon(baseWeapon);
            HandleWeaponInitialization(baseWeapon);
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

        public void Aim(bool aim)
        {
            if (_aimableWeapon != null && aim != _isAming)
            {
                _isAming = aim;
                if (aim)
                {
                    _aimableWeapon.Aim();
                    OnAimWeapon.Invoke(true);
                }
                else
                {
                    _aimableWeapon.AimOff();
                    OnAimWeapon.Invoke(false);
                }
            }
        }

        public void Reload()
        {
            if (_reloadableWeapon != null && _reloadableWeapon.Reload())
                OnReloadWeapon.Invoke();
        }
    }
}