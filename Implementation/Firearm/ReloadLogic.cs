﻿using UnityEngine;

using WeaponSystem.Utility;

namespace WeaponSystem.Implementation.Firearm
{
    public class ReloadLogic : MonoBehaviour, IWeaponValidationLogic
    {
        [SerializeField, WeaponPart] private Clip _clip = null;
        [SerializeField, WeaponPart] private AmmunitionStock _stack = null;

        [SerializeField, Space] private bool _isReloadind = false;
        [SerializeField, WeaponRequireComponent(typeof(Animator))] AnimatorHandler _reloadAnimationHander = new AnimatorHandler();

        public bool Validate()
        {
            return !_isReloadind;
        }

        public void Reload(object[] parameters)
        {
            _isReloadind = true;
        }

        private void Update()
        {
            
        }
    }
}