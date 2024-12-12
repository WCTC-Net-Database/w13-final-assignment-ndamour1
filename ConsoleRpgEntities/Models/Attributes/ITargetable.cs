using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Equipments;

namespace ConsoleRpgEntities.Models.Attributes;

public interface ITargetable
{
    string Name { get; set; }
    string Race { get; set; }
    int Health { get; set; }
    string? Class { get; set; }
    ICollection<Ability>? Abilities { get; set; }
    Equipment? Equipment { get; set; }
}
