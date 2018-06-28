using UnityEngine;

using WeaponSystem.Utility;
using UnityEngine.Events;

namespace WeaponSystem.Implementation.Firearm
{
    public class Firearm : RangeWeapon, IReloadable, IAmunitionUser
    {
        [SerializeField, Space, WeaponPart] ReloadLogic reloadLogic = null;

        public UnityEvent ReloadCallback { get { return reloadLogic.ReloadCallback; } }

        public void GetAmunition(params object[] stackObjcts)
        {
            for (int i = 0; i < stackObjcts.Length; i++)
            {
                if (stackObjcts[i] is AmmunitionStock)
                {
                    reloadLogic.Stack = (stackObjcts[i] as AmmunitionStock);
                    break;
                }
            }
            WeaponSystemUtility.FillWeaponLogicList(this, fieldInfo, stackObjcts);
        }

        public bool Reload()
        {
            reloadLogic.Reload();
            return true;
        }
    }
}