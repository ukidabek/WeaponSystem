using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

namespace WeaponSystem
{
    [Serializable]
	public class HitLogic 
	{
		[SerializeField] private float _baseDamage = 20f;

		public float Damage{ get { return _baseDamage; } }
		
		public void ApplyHit(GameObject gameObject)
		{
			IHit hit = gameObject.GetComponent<IHit>();
			if(hit != null)
			{
				hit.DealDamage(Damage);
			}
		}
	}
}