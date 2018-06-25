using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem.Utility
{
    [Serializable]
    public class AnimatorHandler
    {
        public Animator Animator = null;
        [SerializeField] private string _name = string.Empty;
        [SerializeField] private int _nameHash = 0;

        private bool _oldBoolValue;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                _nameHash = Animator.StringToHash(value);
            }
        }

        public void Set()
        {
            Set(Animator);
        }

        public void Set(float value)
        {
            Set(Animator, value);
        }

        public void Set(float value, float dampTime)
        {
            Set(Animator, value, dampTime);
        }

        public void Set(bool value)
        {
            Set(Animator, value);
        }

        public void Set(Animator animator)
        {
            animator.SetTrigger(_nameHash);
        }

        public void Set(Animator animator, float value)
        {
            animator.SetFloat(_nameHash, value);
        }

        public void Set(Animator animator, float value, float dampTime)
        {
            animator.SetFloat(_nameHash, value, dampTime, Time.deltaTime);
        }

        public void Set(Animator animator, bool value)
        {
            if (_oldBoolValue != value)
            {
                animator.SetBool(_nameHash, value);
                _oldBoolValue = value;
            }
        }
    }
}
