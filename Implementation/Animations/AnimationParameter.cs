using System;
using UnityEngine;

namespace WeaponSystem.Implementation.Animations
{
    [Serializable]
    public abstract class AnimationParameter
    {
        [SerializeField] protected Animator animator = null;
        [SerializeField] protected string parameterName = string.Empty;

        public abstract void Set(params object[] data);
    }
}