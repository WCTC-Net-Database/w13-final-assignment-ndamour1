using ConsoleRpgEntities.Models.Characters;

namespace ConsoleRpgEntities.Models.Abilities.PlayerAbilities;

public interface IAbility
{
    int Id { get; set; }
    string Name { get; set; }
    int Damage { get; set; }
    int Defense { get; set; }
    int? Distance { get; set; }
    bool Dodge { get; set; }
    bool InUse { get; set; }
    ICollection<Player> Players { get; set; }
}
