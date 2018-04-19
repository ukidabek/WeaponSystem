using UnityEngine;

namespace WeaponSystem
{
    public interface IHit
	{
		void DealDamage(float damage);
        GameObject GameObject { get; }        
	}
}