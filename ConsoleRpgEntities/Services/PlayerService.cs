using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Equipments;
using ConsoleRpgEntities.Models.Rooms;
using ConsoleRpgEntities.Services;

public class PlayerService
{
    private readonly IOutputService _outputService;
    private readonly EquipmentRepository _equipmentRepository;
    private readonly AbilityService _abilityService;

    public PlayerService(IOutputService outputService, EquipmentRepository equipmentRepository, AbilityService abilityService)
    {
        _outputService = outputService;
        _equipmentRepository = equipmentRepository;
        _abilityService = abilityService;
    }

    public void Attack(IPlayer player, ITargetable target)
    {
        // "Dice roll"
        Random rand = new Random();
        int roll = rand.Next(1, 7);

        // Stats
        int attack = 0;
        int playerDefense = 0;
        int monsterDefense = 0;

        // Monster defense stats
        if (target.Race.Equals("Balrog"))
        {
            monsterDefense = 19;
        }
        else if (target.Race.Equals("Fallen"))
        {
            monsterDefense = 18;
        }
        else if (target.Race.Equals("Goblin"))
        {
            monsterDefense = 5;
        }
        else if (target.Race.Equals("Kumiho"))
        {
            monsterDefense = 10;
        }
        else if (target.Race.Equals("Ogre"))
        {
            monsterDefense = 11;
        }
        else if (target.Race.Equals("Orrok") || target.Race.Equals("Troll"))
        {
            monsterDefense = 15;
        }
        else if (target.Race.Equals("Orrok"))
        {
            monsterDefense = 16;
        }
        else if (target.Race.Equals("Uruk"))
        {
            monsterDefense = 7;
        }

        if (player.Equipment.Armor != null)
        {
            playerDefense = player.Equipment.Armor.Defense;
        }
        if (player.Equipment.Weapon != null)
        {
            attack = player.Equipment.Weapon.Attack;

            // Damage calculations
            decimal damage = (roll + attack) / ((monsterDefense + 100) / 100);

            // Total damage
            int totalDamage = (int)Math.Round(damage, MidpointRounding.AwayFromZero);
            int overkill = totalDamage + (target.Health - totalDamage);

            // Player-specific attack logic
            _outputService.WriteLine($"{player.Name} attacks {target.Name} with a {player.Equipment.Weapon.Name}!");
            target.Health -= totalDamage;
            if (target.Health > 0)
            {
                _outputService.WriteLine($"{target.Name} took {totalDamage} damage.");
            }
            else
            {
                if (target.Health - totalDamage < 0)
                {
                    _outputService.WriteLine($"{target.Name} took {overkill} damage.\n{target.Name} has been defeated.");
                }
                else if (target.Health - totalDamage == 0)
                {
                    _outputService.WriteLine($"{target.Name} took {totalDamage} damage.\n{target.Name} has been defeated.");
                }
            }
        }
        else
        {
            _outputService.WriteLine($"{player.Name} has no weapon equipped!");
        }
    }

    public void UseAbility(IPlayer player, IAbility ability, ITargetable target)
    {
        if (player.Abilities?.Contains(ability) == true)
        {
            _abilityService.Activate(ability, player, target);
        }
        else
        {
            _outputService.WriteLine($"{player.Name} does not have the ability {ability.Name}!");
        }
    }

    public void EquipItemFromInventory(IPlayer player, Item item)
    {
        if (player.Inventory?.Items.Contains(item) == true)
        {
            player.Equipment?.EquipItem(item);
            _equipmentRepository.UpdateEquipmentList(player.Equipment);
            _outputService.WriteLine($"You have equipped the {item.Name}.");
        }
        else
        {
            _outputService.WriteLine($"{player.Name} does not have the item {item.Name} in their inventory!");
        }
    }

    public void Move(IPlayer player, string direction)
    {
        Room nextRoom = new Room();

        if (direction == "north")
        {
            nextRoom = player.Room.North;
        }
        else if (direction == "south")
        {
            nextRoom = player.Room.South;
        }
        else if (direction == "east")
        {
            nextRoom = player.Room.East;
        }
        else if (direction == "west")
        {
            nextRoom = player.Room.West;
        }

        if (nextRoom != null)
        {
            player.Room = nextRoom;
            _outputService.WriteLine($"{player.Name} has entered {player.Room.Name}.\n{player.Room.Description}");

            foreach (var monster in player.Room.Monsters)
            {
                _outputService.WriteLine($"{monster.Name} is here.");
            }
        }
        else
        {
            _outputService.WriteLine($"{player.Name} cannot move to the specified room.");
        }
    }
}
