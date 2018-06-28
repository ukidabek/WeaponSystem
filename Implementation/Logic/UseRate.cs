using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem.Implementation.Logic
{
    public class UseRate : MonoBehaviour, IWeaponValidationLogic, IWeaponLogic
    {
        [SerializeField] private float _useRate = 0.25f;
        [SerializeField] private float _counter = 0f;

        [SerializeField, Space] private bool _useFramesDelay = false;
        [SerializeField] private int _framesDelay = 1;
        [SerializeField] private int _framesDelayCounter = 0;

        public string[] Name { get { return new string[] { "Fire rate" }; } }

        public string[] Value { get { return new string[] { (1f / _useRate).ToString() }; } }

        public object[] Object { get { return new object[] { _useRate }; } }

        public void Perform(params object[] data)
        {
            _counter = _counter > 0 ? 0 : _useRate;
            enabled = true;
        }

        public bool Validate()
        {
            if (_useFramesDelay)
                return _counter <= 0f && _framesDelayCounter-- == 0;
            else
                return _counter <= 0f;
        }

        protected virtual void Update()
        {
            if (_counter > 0)
            {
                _counter -= Time.deltaTime;
            }
            else
            {
                _framesDelayCounter = _framesDelay;
                enabled = false;
            }
        }
    }
}