using ConsoleRpgEntities.Models.Rooms;

namespace ConsoleRpgEntities.Models.Characters.Monsters;

public interface IMonster
{
    int Id { get; set; }
    string Name { get; set; }
    string Race { get; set; }
    int Health { get; set; }
    Room Room { get; set; }
}
