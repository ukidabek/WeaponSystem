using UnityEngine.Events;

namespace WeaponSystem
{
    public interface IReloadable
    {
        bool Reload(params object[] parameters);
        void AddReloadStartListener(UnityAction call);
        void AddReloadEndListener(UnityAction call);
        void RemoveReloadStartListener(UnityAction call);
        void RemoveReloadEndtListener(UnityAction call);
    }
}