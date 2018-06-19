using UnityEngine;
using UnityEngine.Events;

using System;
using System.Collections;
using System.Collections.Generic;

namespace WeaponSystem
{
    using Object = UnityEngine.Object;

    [Serializable]
    public class WeaponHandler
    {
        [Header("Animations")]
        [SerializeField] private Animator _characterAnimatior = null;

        [Header("Weapon associated transforms")]
        [SerializeField] private Transform _weaponHolder = null;
        [SerializeField] private Transform _shootOrigin = null;
        [SerializeField] private Transform _hitOrigin = null;

        [Header("Unity events")]
        public UnityEvent OnUseMeleeWeapon = new UnityEvent();
        public UnityEvent OnUseRangedWeapon = new UnityEvent();
        public UnityEvent OnReloadWeapon = new UnityEvent();
        public OnWeaponAim OnAimWeapon = new OnWeaponAim();

        [Header("Weapons slots")]
        [SerializeField] private int _weaponSlotsCount = 3;
        public int WeaponSlotsCount { get { return _weaponSlotsCount; } }

        [SerializeField] private int _currentWeaponIndex = 0;

        [Space]
        [SerializeField] private List<IWeapon> _weaponSlots = new List<IWeapon>();
        public List<IWeapon> WeaponSlots { get { return _weaponSlots; } }
        public IWeapon CurrentWeapon { get { return _weaponSlots[_currentWeaponIndex]; } }

        [SerializeField, Space] private List<GameObject> _defaultWeaponPrefabList = new List<GameObject>();

        [Space]
        [SerializeField] private Object[] _initializeData = null;
        [SerializeField] private Object[] _useData = null;

        private bool _isAming = false;

        private IRange _rangeWeapon = null;
        private IMelee _meleeWeapon = null;
        private IReloadable _reloadableWeapon = null;
        private IAimable _aimableWeapon = null;

        public void CreateWeaponsInstances()
        {
            for (int i = 0; i < _defaultWeaponPrefabList.Count; i++)
            {
                GameObject instance = GameObject.Instantiate(_defaultWeaponPrefabList[i], _weaponHolder.position, _weaponHolder.rotation, _weaponHolder);
                IWeapon weapon = instance.GetComponent<IWeapon>();
                _weaponSlots.Add(weapon);

                weapon.Initialize(_initializeData);
                weapon.GameObject.SetActive(false);
            }
        }

        public void SwichToDefaultWeapon()
        {
            SwitchToWeapon(_currentWeaponIndex);
        }

        public void SwitchToWeapon(int index)
        {
            _weaponSlots[_currentWeaponIndex].GameObject.SetActive(false);
            _currentWeaponIndex = index;
            SwitchToWeapon(_weaponSlots[index]);
        }

        private void SwitchToWeapon(IWeapon baseWeapon)
        {
            baseWeapon.GameObject.SetActive(true);
            var controllerProvider = baseWeapon.GameObject.GetComponent<IAnimationControllerProvider>();
            if(controllerProvider != null)
                _characterAnimatior.runtimeAnimatorController = controllerProvider.Controller;

            HandleRangeWeapon(baseWeapon);
            HandleMeleeWeapon(baseWeapon);
            HandleReloadableWeapon(baseWeapon);
            HandleAimableWeapon(baseWeapon);
        }

        private void HandleRangeWeapon(IWeapon baseWeapon)
        {
            if (baseWeapon is IRange)
                (baseWeapon as IRange).ShotOrigin = _shootOrigin;
        }

        private void HandleMeleeWeapon(IWeapon baseWeapon)
        {
            if (baseWeapon is IMelee)
                (baseWeapon as IMelee).HitOrigin = _hitOrigin;
        }

        private void HandleAimableWeapon(IWeapon baseWeapon)
        {
            if (baseWeapon is IAimable)
                _aimableWeapon = baseWeapon as IAimable;
        }

        private void HandleReloadableWeapon(IWeapon baseWeapon)
        {
            if (baseWeapon is IReloadable)
                _reloadableWeapon = baseWeapon as IReloadable;
        }

        public void OnValidate()
        {
            if (_defaultWeaponPrefabList.Count > WeaponSlotsCount)
                _defaultWeaponPrefabList.RemoveAt(_defaultWeaponPrefabList.Count - 1);
        }

        public void UseWeapon()
        {
            if (CurrentWeapon != null && CurrentWeapon.Use(_useData))
            {
                if (_rangeWeapon != null)
                    OnUseRangedWeapon.Invoke();
                if (_meleeWeapon != null)
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
                    _aimableWeapon.Aim(aim);
                    OnAimWeapon.Invoke(true);
                }
                else
                {
                    _aimableWeapon.AimOff(aim);
                    OnAimWeapon.Invoke(false);
                }
            }
        }

        public void Reload()
        {
            if (_reloadableWeapon != null && _reloadableWeapon.Reload())
            {
                OnReloadWeapon.Invoke();
            }
        }
    }
}