using UnityEngine;
using UnityEngine.Events;

using System;
using WeaponSystem.Interfaces.Logic;
using WeaponSystem.Interfaces.Weapon;

namespace WeaponSystem.Implementation.Logic
{
    public class Hitscan : MonoBehaviour, IWeaponLogic, IWeaponTransform, IWeaponStatistics, IWeaponInitialization
    {
        [SerializeField, Space] private float _range = 100f;
        public GameObjectUnityEvent OnHitCallback = new GameObjectUnityEvent();
        [SerializeField] private LayerMask _hitLayer = new LayerMask();

        public Transform Transform { get; set; }

        public string[] Name { get { return new[] { "Range: " }; } }

        public string[] Value { get { return new[] { _range.ToString() }; } }

        public object[] Object { get { return new[] { (object)_range }; } }

        public void Perform(params object[] data)
        {
            RaycastHit hit;
            if (Physics.Raycast(Transform.position, Transform.forward, out hit, _range, _hitLayer))
            {
                OnHitCallback.Invoke(hit.collider.gameObject);
                Debug.DrawLine(hit.point, Transform.position, Color.red, 1f);
                Debug.Log(hit.transform.gameObject.name);
            }
        }

        public void Initialize(params object[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if(data[i] is ShotOrigin)
                    Transform = (data[i] as ShotOrigin).gameObject.transform;
            }
        }
    }

    [Serializable] public class GameObjectUnityEvent : UnityEvent<GameObject> {}
}