using UnityEngine;

namespace WeaponSystem
{
	public abstract class BaseWeaponLogic : MonoBehaviour 
	{
        protected T GetObjectFormData<T>(params object[] data) where T : class
        {
            for (int i = 0; i < data.Length; i++)
                if (data[i] is T)
                    return data[i] as T;

            return null;
        }

        public virtual void Initialize(params object[] data) {} 
		public abstract void Perform(params object[] data);
	}
}