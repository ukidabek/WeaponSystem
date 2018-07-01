using System;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem.Utility;

namespace WeaponSystem.Implementation.Firearm
{
    public class ReloadLogic : MonoBehaviour, IWeaponValidationLogic
    {
        [Serializable]
        public abstract class AnimationParameter
        {
            [SerializeField] protected Animator animator = null;
            [SerializeField] protected string parameterName = string.Empty;
            public abstract void Set(params object[] data);
        }

        [Serializable]
        public class AnimationTrigger : AnimationParameter
        {
            public override void Set(params object[] data)
            {
                animator.SetTrigger(parameterName);
            }
        }

        [SerializeField, WeaponPart] private Clip _clip = null;
        [SerializeField, InitializeWeaponComponent] private AmmunitionStock _stack = null;
        public AmmunitionStock Stack
        {
            get { return _stack; }
            set { _stack = value; }
        }

        [SerializeField, Space] private bool _isReloadind = false;

        [SerializeField, WeaponRequireComponent(typeof(Animator))] private AnimationTrigger _weaponReload = new AnimationTrigger();
        [SerializeField, InitializeWeaponComponent(typeof(Animator))] private AnimationTrigger _userReload = new AnimationTrigger();

        public UnityEvent ReloadCallback = new UnityEvent();

        public bool Validate()
        {
            return !_isReloadind;
        }

        public void Reload()
        {
            var refil = 0;
            if (_stack.Resource > _clip.Delta)
            {
                _stack.Resource -= _clip.Delta;
                refil = _clip.Delta;
            }
            else
            {
                refil = _stack.Resource;
                _stack.Resource -= _stack.Resource;
            }

            _clip.Reload(refil);

            //_isReloadind = true;
        }

        private void Update()
        {
            if(_clip.Counter == 0)
            {
                Reload();
                ReloadCallback.Invoke();
            }
        }
    }
}