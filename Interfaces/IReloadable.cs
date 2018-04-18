using UnityEngine.Events;

namespace WeaponSystem
{
    public interface IReloadable
    {
        void Reload(params object[] parameters);
        void AddReloadStartListener(UnityAction call);
        void AddReloadEndListener(UnityAction call);
        void RemoveReloadStartListener(UnityAction call);
        void RemoveReloadEndtListener(UnityAction call);
    }
}