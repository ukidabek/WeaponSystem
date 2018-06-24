using UnityEngine;

namespace WeaponSystem.Implementation.Logic
{
    public class Hitscan : MonoBehaviour, IWeaponLogic, IWeaponStatistics, IWeaponTransform
    {
        [SerializeField, Space] private float _range = 100f;
        [SerializeField] private HitLogic _hitLogic = new HitLogic();
        [SerializeField, Space] private LayerMask _hitLayer = new LayerMask();

        public string[] Name { get { return new string[] { "Damage", "Range" }; } }

        public string[] Value { get { return new string[] { _hitLogic.Damage.ToString(), _range.ToString() }; } }

        public object[] Object { get { return new object[] { _hitLogic.Damage, _range }; } }

        public Transform Transform { get; set; }

        public void Perform(params object[] data)
        {
            RaycastHit hit;
            if (Physics.Raycast(Transform.position, Transform.forward, out hit, _range, _hitLayer))
            {
                _hitLogic.ApplyHit(hit.collider.gameObject);
                Debug.DrawLine(hit.point, Transform.position, Color.red, 1f);
                Debug.Log(hit.transform.gameObject.name);
            }
        }
    }
}