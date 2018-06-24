using UnityEngine;
using System.Collections;

using System.Collections.Generic;

using WeaponSystem.Utility;

namespace WeaponSystem.Implementation
{
    public class RangeWeapon : Weapon, IRange
    {
        [LogicList] protected List<IWeaponTransform> _weaponTransformsList = new List<IWeaponTransform>();

        private Transform _shotOrigin = null;
        public Transform ShotOrigin
        {
            get { return _shotOrigin; }
            set
            {
                _shotOrigin = value;
                for (int i = 0; i < _weaponTransformsList.Count; i++)
                    _weaponTransformsList[i].Transform = value;
            }
        }
    }
}