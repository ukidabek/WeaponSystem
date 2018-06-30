using UnityEngine;

using System.Collections.Generic;

namespace WeaponSystem
{
    public class WeaponSpawner : MonoBehaviour
    {
        [SerializeField] private WeaponHandler _weaponHandler = null;

        [Header("Weapon associated transforms")]
        [SerializeField] private Transform _weaponHolder = null;

        [Space, Header("Weapons slots")]
        [SerializeField] private int _currentWeaponIndex = 0;
        [SerializeField] private List<GameObject> _defaultWeaponPrefabList = new List<GameObject>();
        [SerializeField] private List<IWeapon> _weaponSlots = new List<IWeapon>();
        public List<IWeapon> WeaponSlots { get { return _weaponSlots; } }

        public void CreateWeaponsInstances()
        {
            for (int i = 0; i < _defaultWeaponPrefabList.Count; i++)
            {
                GameObject instance = GameObject.Instantiate(_defaultWeaponPrefabList[i], _weaponHolder.position, _weaponHolder.rotation, _weaponHolder);
                IWeapon weapon = instance.GetComponent<IWeapon>();
                _weaponSlots.Add(weapon);
                weapon.GameObject.SetActive(false);
            }
        }

        private void Start()
        {
            CreateWeaponsInstances();
            _weaponHandler.EquipWeapon(_weaponSlots[_currentWeaponIndex]);
        }
    }
}
