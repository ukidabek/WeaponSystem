using System;
namespace WeaponSystem.Implementation.Animations
{
    [Serializable]
    public class AnimationBool : AnimationParameter
    {
        public override void Set(params object[] data)
        {
            animator.SetBool(parameterName, (bool)data[0]);
        }
    }
}