using UnityEngine;
using UnityEngine.Events;

using System;
using System.Collections;
using System.Collections.Generic;

namespace WeaponSystem
{
    [Serializable]
    public class WeaponHandler
    {
        [Header("Animations")]
        [SerializeField]
        private Animator _characterAnimatior = null;

        [Header("Weapon associated transforms")]
        [SerializeField]
        private Transform _weaponHolder = null;
        [SerializeField] private Transform _shootOrigin = null;
        [SerializeField] private Transform _hitOrigin = null;

        [Header("Unity events")]
        public UnityEvent OnUseMeleeWeapon = new UnityEvent();
        public UnityEvent OnUseRangedWeapon = new UnityEvent();
        public UnityEvent OnReloadWeapon = new UnityEvent();
        public OnWeaponAim OnAimWeapon = new OnWeaponAim();

        [Header("Weapons slots")]
        [SerializeField]
        private int _weaponSlotsCount = 3;
        public int WeaponSlotsCount { get { return _weaponSlotsCount; } }

        [SerializeField] private int _currentWeaponIndex = 0;

        [SerializeField, Space] private List<BaseWeapon> _weaponSlots = new List<BaseWeapon>();

        [SerializeField, Space] private List<GameObject> _defaultWeaponPrefabList = new List<GameObject>();

        public BaseWeapon CurrentWeapon { get { return _weaponSlots[_currentWeaponIndex]; } }


        private bool _isAming = false;

        private bool _isRangedWeapon = false;
        private IRange _rangeWeapon = null;

        private bool _isMeleeWeapon;
        private IMelee _meleeWeapon = null;

        private bool _isReloadable = false;
        private IReloadable _reloadableWeapon = null;

        private bool _isAimable = false;
        private IAimable _aimableWeapon = null;

        public void CreateWeaponsInstances()
        {
            for (int i = 0; i < _defaultWeaponPrefabList.Count; i++)
            {
                GameObject instance = GameObject.Instantiate(_defaultWeaponPrefabList[i], _weaponHolder.position, _weaponHolder.rotation, _weaponHolder);
                BaseWeapon weapon = instance.GetComponent<BaseWeapon>();
                _weaponSlots.Add(weapon);

                weapon.Initialize();
                weapon.gameObject.SetActive(false);

                if (i == _currentWeaponIndex) SwitchToWeapon(weapon);
            }
        }

        public void SwitchToWeapon(int index)
        {
            _weaponSlots[_currentWeaponIndex].gameObject.SetActive(false);
            _currentWeaponIndex = index;
            SwitchToWeapon(_weaponSlots[index]);
        }

        private void SwitchToWeapon(BaseWeapon baseWeapon)
        {
            baseWeapon.gameObject.SetActive(true);
            _characterAnimatior.runtimeAnimatorController = baseWeapon.CharacterAnimatorController;

            HandleRangeWeapon(baseWeapon);
            HandleMeleeWeapon(baseWeapon);
            HandleReloadableWeapon(baseWeapon);
            HandleAimableWeapon(baseWeapon);
        }

        private void HandleRangeWeapon(BaseWeapon baseWeapon)
        {
            _isRangedWeapon = baseWeapon is IRange;
            if (_isRangedWeapon)
            {
                _rangeWeapon = baseWeapon as IRange;
                _rangeWeapon.ShotOrigin = _shootOrigin;
            }
        }

        private void HandleMeleeWeapon(BaseWeapon baseWeapon)
        {
            _isMeleeWeapon = baseWeapon is IMelee;
            if (_isMeleeWeapon)
            {
                _meleeWeapon = baseWeapon as IMelee;
                _meleeWeapon.HitOrigin = _hitOrigin;
            }
        }

        private void HandleAimableWeapon(BaseWeapon baseWeapon)
        {
            _isAimable = baseWeapon is IAimable;
            if (_isAimable)
                _aimableWeapon = baseWeapon as IAimable;
        }

        private void HandleReloadableWeapon(BaseWeapon baseWeapon)
        {
            _isReloadable = baseWeapon is IReloadable;
            if (_isReloadable)
                _reloadableWeapon = baseWeapon as IReloadable;
        }

        public void OnValidate()
        {
            if (_defaultWeaponPrefabList.Count > WeaponSlotsCount)
                _defaultWeaponPrefabList.RemoveAt(_defaultWeaponPrefabList.Count - 1);
        }

        public void UseWeapon()
        {
            if (CurrentWeapon.Use())
            {
                if (_isRangedWeapon)
                    OnUseRangedWeapon.Invoke();
                if (_isMeleeWeapon)
                    OnUseMeleeWeapon.Invoke();
            }
        }

        public void Aim(bool aim)
        {
            if (_isAimable && aim != _isAming)
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
            if (_isReloadable && _reloadableWeapon.Reload())
            {
                OnReloadWeapon.Invoke();
            }
        }
    }
    [Serializable] public class OnWeaponAim : UnityEvent<object> {}
}