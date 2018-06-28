using UnityEngine;
using WeaponSystem.Utility;

namespace WeaponSystem.Implementation.Firearm
{
    public class AmmunitionStock : MonoBehaviour, IWeaponLogic
    {
        [SerializeField] private int _resource = 200;
        public int Resource
        {
            get { return _resource; }
            set { _resource = value; }
        }

        public void Perform(params object[] data)
        {
        }
    }
}