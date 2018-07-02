using UnityEngine;
using UnityEngine.Events;

using WeaponSystem.Implementation.Animations;
using WeaponSystem.Utility;
using WeaponSystem.Interfaces.Logic;

namespace WeaponSystem.Implementation.Firearm
{
    public class ReloadLogic : MonoBehaviour, IWeaponValidationLogic
    {
        [SerializeField, WeaponPart] private Clip _clip = null;
        public Clip Clip { get { return _clip; } }

        [SerializeField, InitializeWeaponComponent] private AmmunitionStock _stack = null;
        public AmmunitionStock Stack
        {
            get { return _stack; }
            set { _stack = value; }
        }

        [SerializeField, Space] private bool _isReloadind = false;

        [SerializeField, WeaponRequireComponent(typeof(Animator))] private AnimationTrigger _weaponReload = new AnimationTrigger();
        [SerializeField, InitializeWeaponComponent(typeof(Animator))] private AnimationTrigger _userReload = new AnimationTrigger();
        [SerializeField, InitializeWeaponComponent(typeof(Animator))] private AnimatorState _reloadAnimationState = new AnimatorState();

        public UnityEvent ReloadCallback = new UnityEvent();

        public bool Validate()
        {
            return !_isReloadind;
        }

        public void Reload()
        {
            var refil = 0;
            if (_stack.Resource > Clip.Delta)
            {
                _stack.Resource -= Clip.Delta;
                refil = Clip.Delta;
            }
            else
            {
                refil = _stack.Resource;
                _stack.Resource -= _stack.Resource;
            }

            Clip.Reload(refil);
            _userReload.Set();

            _isReloadind = true;
        }

        private void Update()
        {
            _isReloadind = _reloadAnimationState.InState();

            if (!_isReloadind)
            {
                if(Clip.Counter == 0)
                {
                    Reload();
                    ReloadCallback.Invoke();
                }
            }
        }
    }
}