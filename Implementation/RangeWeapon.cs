using UnityEngine;

using WeaponSystem.Utility;

namespace WeaponSystem.Implementation
{
    public class RangeWeapon : Weapon, IRange
    {
        [SerializeField, InitializeWeaponComponent] protected ShotOrigin shotOrigin = null;

        public Transform ShotOrigin
        {
            get { return shotOrigin.transform; }
            set
            {
                shotOrigin.transform.position = value.position;
                shotOrigin.transform.rotation = value.rotation;
                shotOrigin.transform.localScale = value.localScale;
            }
        }
    }
}