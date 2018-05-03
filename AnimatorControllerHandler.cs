using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

namespace WeaponSystem
{
    [Serializable]
    public class AnimatorControllerHandler<T>
    {
        [SerializeField] private T _key;
        public T Key { get { return _key; } }

        [SerializeField] private AnimatorOverrideController _controller = null;
        public AnimatorOverrideController Controller { get { return _controller; } }
    }
}