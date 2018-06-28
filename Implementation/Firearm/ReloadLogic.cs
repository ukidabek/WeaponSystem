using UnityEngine;
using UnityEngine.Events;
using WeaponSystem.Utility;

namespace WeaponSystem.Implementation.Firearm
{
    public class ReloadLogic : MonoBehaviour, IWeaponValidationLogic
    {
        [SerializeField, WeaponPart] private Clip _clip = null;
        [SerializeField, WeaponPart] private AmmunitionStock _stack = null;

        [SerializeField, Space] private bool _isReloadind = false;
        [SerializeField, WeaponRequireComponent(typeof(Animator))] AnimatorHandler _reloadAnimationHander = new AnimatorHandler();

        public UnityEvent ReloadCallback = new UnityEvent();

        public bool Validate()
        {
            return !_isReloadind;
        }

        public void Reload()
        {
            if (_stack.Resource > 0)
            {
                _stack.Resource -= _clip.Delta;
                _clip.Reload(_clip.Delta);
            }

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