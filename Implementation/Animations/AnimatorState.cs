using System;
using UnityEngine;

namespace WeaponSystem.Implementation.Animations
{
    [Serializable]
    public class AnimatorState
    {
        [SerializeField] protected Animator animator = null;
        [SerializeField] protected string stateName = string.Empty;
        [SerializeField] protected int layerIndex = 0;

        public bool InState()
        {
            var stateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);
            int hash = Animator.StringToHash(stateName);

            return stateInfo.shortNameHash == hash;
        }
    }
}