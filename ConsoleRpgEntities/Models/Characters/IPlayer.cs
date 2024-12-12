using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Equipments;
using ConsoleRpgEntities.Models.Rooms;

namespace ConsoleRpgEntities.Models.Characters;

public interface IPlayer
{
    int Id { get; set; }
    string Name { get; set; }
    string Class { get; set; }
    int Health { get; set; }
    ICollection<Ability> Abilities { get; set; }
    Inventory Inventory { get; set; }
    Equipment Equipment { get; set; }
    Room Room { get; set; }
}