using System;

namespace WeaponSystem.Implementation.Animations
{
    [Serializable]
    public class AnimationTrigger : AnimationParameter
    {
        public override void Set(params object[] data)
        {
            animator.SetTrigger(parameterName);
        }
    }
}