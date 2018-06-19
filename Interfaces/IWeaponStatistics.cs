using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace WeaponSystem
{
	public interface IWeaponStatistics 
	{
		string [] Name { get; }
		string [] Value  { get; }
		object [] Object { get; }
	}
}