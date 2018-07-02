using UnityEngine.Events;

namespace WeaponSystem.Interfaces.Weapon
{
    public interface IReloadable
    {
        bool Reload();
        UnityEvent ReloadCallback { get; }
    }
}