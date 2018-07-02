namespace WeaponSystem.Interfaces.Weapon
{
	public interface IWeaponStatistics 
	{
		string [] Name { get; }
		string [] Value  { get; }
		object [] Object { get; }
	}
}