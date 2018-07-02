namespace WeaponSystem.Interfaces.Weapon
{
    public interface IAmmunition
    {
        int ClipStatus { get; }
        float ClipProcentage { get; }
        int AmmunitionStatus { get; }
        float AmmunitionProcentage { get; }
    }
}