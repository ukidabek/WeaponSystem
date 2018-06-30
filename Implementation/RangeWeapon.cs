using UnityEngine;
using System.Collections;

using System.Collections.Generic;

using WeaponSystem.Utility;

namespace WeaponSystem.Implementation
{
    public class RangeWeapon : Weapon, IRange
    {
        //[LogicList] protected List<IWeaponTransform> _weaponTransformsList = new List<IWeaponTransform>();
        [SerializeField, InitializeWeaponComponent] protected ShotOrigin shotOrigin = null;

        //private Transform _shotOrigin = null;
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