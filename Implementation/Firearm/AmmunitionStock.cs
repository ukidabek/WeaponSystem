using UnityEngine;
using WeaponSystem.Utility;

namespace WeaponSystem.Implementation.Firearm
{
    public class AmmunitionStock : MonoBehaviour, IWeaponLogic/*, IWeaponValidationLogic*/
    {
        [SerializeField, WeaponPart] private Clip _clip = null;

        [SerializeField] private int _resource = 200;
        public int Resource { get { return _resource ; } }


        public void Perform(params object[] data)
        {
            throw new System.NotImplementedException();
        }

        //public bool Validate()
        //{
        //    _clip.Counter > 0 && _resource.
        //}
    }
}