using UnityEngine;
using WeaponSystem.Utility;

namespace WeaponSystem.Implementation.Firearm
{
    public class AmmunitionStock : MonoBehaviour
    {
        [SerializeField] private int _resource = 0;
        public int Resource
        {
            get { return _resource; }
            set { _resource = value; }
        }

        [SerializeField] private int _maxRosiurceCount = 200;
        public int MaxRosourceCount { get { return _maxRosiurceCount; } }

        private void Awake()
        {
            _resource = MaxRosourceCount;
        }
    }
}