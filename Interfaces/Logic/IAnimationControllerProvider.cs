using UnityEngine;

namespace WeaponSystem.Interfaces.Logic
{
    public interface IAnimationControllerProvider
    {
        AnimatorOverrideController Controller { get; }
    }
}