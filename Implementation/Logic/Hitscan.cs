using UnityEngine;
using UnityEngine.Events;

using System;

namespace WeaponSystem.Implementation.Logic
{
    public class Hitscan : MonoBehaviour, IWeaponLogic, IWeaponTransform, IWeaponStatistics
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
    }

    [Serializable] public class GameObjectUnityEvent : UnityEvent<GameObject> {}
}