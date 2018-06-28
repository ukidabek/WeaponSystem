using UnityEngine.Events;

namespace WeaponSystem
{
    public interface IReloadable
    {
        bool Reload();
        UnityEvent ReloadCallback { get; }
    }
}