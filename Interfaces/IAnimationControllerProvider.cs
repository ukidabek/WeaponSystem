using UnityEngine;

namespace WeaponSystem
{
    public interface IAnimationControllerProvider
    {
        AnimatorOverrideController Controller { get; }
    }
}