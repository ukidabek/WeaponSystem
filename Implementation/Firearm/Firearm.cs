using UnityEngine;

using WeaponSystem.Utility;
using UnityEngine.Events;

namespace WeaponSystem.Implementation.Firearm
{
    public class Firearm : RangeWeapon, IReloadable, IAimable
    {
        [SerializeField, Space, WeaponPart] ReloadLogic reloadLogic = null;

        public UnityEvent ReloadCallback { get { return reloadLogic.ReloadCallback; } }

        public bool Reload()
        {
            reloadLogic.Reload();
            return true;
        }

        public override void Initialize(params object[] data)
        {
            base.Initialize(data);
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] is AmmunitionStock)
                {
                    reloadLogic.Stack = (data[i] as AmmunitionStock);
                    break;
                }
            }
            WeaponSystemUtility.FillWeaponLogicList(this, fieldInfo, data);
        }

        public void Aim() {}

        public void AimOff() {}
    }
}