using UnityEngine;
using UnityEngine.Events;
using WeaponSystem.Utility;

namespace WeaponSystem.Implementation.Firearm
{
    public class ReloadLogic : MonoBehaviour, IWeaponValidationLogic
    {
        [SerializeField, WeaponPart] private Clip _clip = null;
        [SerializeField] private AmmunitionStock _stack = null;
        public AmmunitionStock Stack
        {
            get { return _stack; }
            set { _stack = value; }
        }

        [SerializeField, Space] private bool _isReloadind = false;
        [SerializeField, WeaponRequireComponent(typeof(Animator))] AnimatorHandler _reloadAnimationHander = new AnimatorHandler();

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