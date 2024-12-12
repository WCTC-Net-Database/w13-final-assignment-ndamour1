using Castle.Core.Internal;
using ConsoleRpg.Helpers;
using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Characters.Monsters;
using ConsoleRpgEntities.Models.Equipments;
using ConsoleRpgEntities.Models.Rooms;
using ConsoleRpgEntities.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Services
{
    public class AssetCreationService
    {
        private readonly GameContext _context;
        private readonly OutputManager _outputManager;
        private readonly SearchService _searchService;
        private readonly PlayerRepository _playerRepository;
        private readonly AbilityRepository _abilityRepository;
        private readonly RoomRepository _roomRepository;
        private Table _logTable;
        private Panel _mapPanel;

        public AssetCreationService(GameContext context, OutputManager outputManager, SearchService searchService, PlayerRepository playerRepository, AbilityRepository abilityRepository, RoomRepository roomRepository)
        {
            _context = context;
            _outputManager = outputManager;
            _searchService = searchService;
            _playerRepository = playerRepository;
            _abilityRepository = abilityRepository;
            _roomRepository = roomRepository;
        }

        public Player CreateCharacter(int modifier)
        {
            // Variables
            List<Player> players = _context.Players.ToList();
            List<Ability> abilities = new List<Ability>();
            List<Ability> loggedAbilities = _context.Abilities.ToList();
            Player newCharacter = new Player();
            int playerId = players.Count + 1;

            // Player variables
            string playerName = null;
            string race = null;
            string playerClass = null;
            Item weapon = new Item();
            Item armor = new Item();
            Room room = new Room();
            int level = 1;
            int health = 0;

            // Ability variables
            string abilityName = null;
            string abilityType = null;
            string description = "A big empty room.";
            int damage = 0;
            int defense = 0;
            int distance = 0;
            bool dodge = false;
            bool inUse = false;

            // Other
            bool complete = false;

            // Name input
            while (true)
            {
                while (true)
                {
                    _outputManager.AddLogEntry("Enter the name your character.");

                    try
                    {
                        string nameInput = _outputManager.GetUserInput("Name:").ToString();

                        if (nameInput.IsNullOrEmpty())
                        {
                            _outputManager.AddLogEntry("You cannot move forward unless you name your character.");
                        }
                        else
                        {
                            _outputManager.AddLogEntry($"Your character's name is {nameInput}.");
                            playerName = nameInput;
                            break;
                        }
                    }
                    catch (Exception e)
                    {
                        _outputManager.AddLogEntry(e.Message);
                    }
                }

                // Name confirmation
                while (true)
                {
                    _outputManager.AddLogEntry("Type 1 to continue.");
                    var continueFromRace = _outputManager.GetUserInput("Continue:");

                    if (continueFromRace == "1")
                    {
                        break;
                    }
                    else
                    {
                        _outputManager.AddLogEntry("Invalid input.");
                    }
                }

                // Race input
                while (true)
                {
                    _outputManager.AddLogEntry("1. Cambion");
                    _outputManager.AddLogEntry("2. Changeling");
                    _outputManager.AddLogEntry("3. Demon");
                    _outputManager.AddLogEntry("4. Draconian");
                    _outputManager.AddLogEntry("5. Dwarf");
                    _outputManager.AddLogEntry("6. Elf");
                    _outputManager.AddLogEntry("7. Fairy");
                    _outputManager.AddLogEntry("8. Garuda");
                    _outputManager.AddLogEntry("9. Goblin");
                    _outputManager.AddLogEntry("10. Golem");
                    _outputManager.AddLogEntry("11. Half-Dwarf");
                    _outputManager.AddLogEntry("12. Half-Elf");
                    _outputManager.AddLogEntry("13. Half-Goblin");
                    _outputManager.AddLogEntry("14. Half-Ogre");
                    _outputManager.AddLogEntry("15. Half-Troll");
                    _outputManager.AddLogEntry("17. Hobbit");
                    _outputManager.AddLogEntry("18. Human");
                    _outputManager.AddLogEntry("19. Kitsune");
                    _outputManager.AddLogEntry("20. Lamia");
                    _outputManager.AddLogEntry("21. Merman");
                    _outputManager.AddLogEntry("22. Minotaur");
                    _outputManager.AddLogEntry("23. Naga");
                    _outputManager.AddLogEntry("24. Ogre");
                    _outputManager.AddLogEntry("25. Orrok");
                    _outputManager.AddLogEntry("26. Saurian");
                    _outputManager.AddLogEntry("27. Troll");
                    _outputManager.AddLogEntry("28. Uruk");
                    _outputManager.AddLogEntry("Select your character\'s race.");

                    try
                    {
                        int raceInput = Convert.ToInt32(_outputManager.GetUserInput("Selection:"));

                        switch (raceInput)
                        {
                            case 1:
                                _outputManager.AddLogEntry("You have chosen to be a Cambion.");
                                race = "Cambion";
                                break;
                            case 2:
                                _outputManager.AddLogEntry("You have chosen to be a Changeling.");
                                race = "Changeling";
                                break;
                            case 3:
                                _outputManager.AddLogEntry("You have chosen to be a Demon.");
                                race = "Demon";
                                break;
                            case 4:
                                _outputManager.AddLogEntry("You have chosen to be a Draconian.");
                                race = "Draconian";
                                break;
                            case 5:
                                _outputManager.AddLogEntry("You have chosen to be a Dwarf.");
                                race = "Dwarf";
                                break;
                            case 6:
                                _outputManager.AddLogEntry("You have chosen to be an Elf.");
                                race = "Elf";
                                break;
                            case 7:
                                _outputManager.AddLogEntry("You have chosen to be a Fairy.");
                                race = "Fairy";
                                break;
                            case 8:
                                _outputManager.AddLogEntry("You have chosen to be a Garuda.");
                                race = "Garuda";
                                break;
                            case 9:
                                _outputManager.AddLogEntry("You have chosen to be a Goblin.");
                                race = "Goblin";
                                break;
                            case 10:
                                _outputManager.AddLogEntry("You have chosen to be a Golem.");
                                break;
                            case 11:
                                _outputManager.AddLogEntry("You have chosen to be a Half-Dwarf.");
                                race = "Half-Dwarf";
                                break;
                            case 12:
                                _outputManager.AddLogEntry("You have chosen to be a Half-Elf.");
                                race = "Half-Elf";
                                break;
                            case 13:
                                _outputManager.AddLogEntry("You have chosen to be a Half-Goblin.");
                                race = "Half-Goblin";
                                break;
                            case 14:
                                _outputManager.AddLogEntry("You have chosen to be a Half-Ogre.");
                                race = "Half-Ogre";
                                break;
                            case 15:
                                _outputManager.AddLogEntry("You have chosen to be a Half-Troll.");
                                race = "Half-Troll";
                                break;
                            case 17:
                                _outputManager.AddLogEntry("You have chosen to be a Hobbit.");
                                race = "Hobbit";
                                break;
                            case 18:
                                _outputManager.AddLogEntry("You have chosen to be a Human.");
                                race = "Human";
                                break;
                            case 19:
                                _outputManager.AddLogEntry("You have chosen to be a Kitsune.");
                                race = "Kitsune";
                                break;
                            case 20:
                                _outputManager.AddLogEntry("You have chosen to be a Lamia.");
                                race = "Lamia";
                                break;
                            case 21:
                                _outputManager.AddLogEntry("You have chosen to be a Merman.");
                                race = "Merman";
                                break;
                            case 22:
                                _outputManager.AddLogEntry("You have chosen to be a Minotaur.");
                                race = "Minotaur";
                                break;
                            case 23:
                                _outputManager.AddLogEntry("You have chosen to be a Naga.");
                                race = "Naga";
                                break;
                            case 24:
                                _outputManager.AddLogEntry("You have chosen to be an Ogre.");
                                race = "Ogre";
                                break;
                            case 25:
                                _outputManager.AddLogEntry("You have chosen to be an Orrok.");
                                race = "Orrok";
                                break;
                            case 26:
                                _outputManager.AddLogEntry("You have chosen to be a Saurian.");
                                race = "Saurian";
                                break;
                            case 27:
                                _outputManager.AddLogEntry("You have chosen to be a Troll.");
                                race = "Troll";
                                break;
                            case 28:
                                _outputManager.AddLogEntry("You have chosen to be an Uruk.");
                                race = "Uruk";
                                break;
                            default:
                                _outputManager.AddLogEntry("Invalid selection. Please choose between 0 and 29.");
                                break;
                        }

                        if (raceInput >= 1 && raceInput < 29)
                        {
                            break;
                        }
                    }
                    catch (Exception e)
                    {
                        _outputManager.AddLogEntry(e.Message);
                    }
                }

                // Race confirmation
                while (true)
                {
                    _outputManager.AddLogEntry("Type 1 to continue.");
                    var continueFromRace = _outputManager.GetUserInput("Continue:");

                    if (continueFromRace == "1")
                    {
                        break;
                    }
                    else
                    {
                        _outputManager.AddLogEntry("Invalid input.");
                    }
                }

                // Class input
                while (true)
                {
                    _outputManager.AddLogEntry("1. Artificer");
                    _outputManager.AddLogEntry("2. Barbarian");
                    _outputManager.AddLogEntry("3. Bard");
                    _outputManager.AddLogEntry("4. Cleric");
                    _outputManager.AddLogEntry("5. Druid");
                    _outputManager.AddLogEntry("6. Fighter");
                    _outputManager.AddLogEntry("7. Monk");
                    _outputManager.AddLogEntry("8. Paladin");
                    _outputManager.AddLogEntry("9. Ranger");
                    _outputManager.AddLogEntry("10. Revenant");
                    _outputManager.AddLogEntry("11. Rogue");
                    _outputManager.AddLogEntry("12. Sorcerer");
                    _outputManager.AddLogEntry("13. Therianthrope");
                    _outputManager.AddLogEntry("14. Vampire");
                    _outputManager.AddLogEntry("15. Warlock");
                    _outputManager.AddLogEntry("16. Wizard");
                    _outputManager.AddLogEntry("Select your character\'s class.");

                    try
                    {
                        double totalHealth = 0;
                        int classInput = Convert.ToInt32(_outputManager.GetUserInput("Selection:"));

                        switch (classInput)
                        {
                            case 1:
                                playerClass = "Artificer";
                                totalHealth = (8 + (level + 8) + (2 * level) + (2 * level * modifier) - 2) / 2;
                                health = (int)Math.Round(totalHealth, MidpointRounding.AwayFromZero);
                                _outputManager.AddLogEntry($"You have chosen to be an Artificer with a total HP of {health}.");
                                break;
                            case 2:
                                playerClass = "Barbarian";
                                totalHealth = (12 + (level + 12) + (2 * level) + (2 * level * modifier) - 2) / 2;
                                health = (int)Math.Round(totalHealth, MidpointRounding.AwayFromZero);
                                _outputManager.AddLogEntry($"You have chosen to be a Barbarian with a total HP of {health}.");
                                break;
                            case 3:
                                playerClass = "Bard";
                                totalHealth = (8 + (level + 8) + (2 * level) + (2 * level * modifier) - 2) / 2;
                                health = (int)Math.Round(totalHealth, MidpointRounding.AwayFromZero);
                                _outputManager.AddLogEntry($"You have chosen tarmor inputo be a Bard with a total HP of {health}.");
                                break;
                            case 4:
                                playerClass = "Cleric";
                                totalHealth = (8 + (level + 8) + (2 * level) + (2 * level * modifier) - 2) / 2;
                                health = (int)Math.Round(totalHealth, MidpointRounding.AwayFromZero);
                                _outputManager.AddLogEntry($"You have chosen to be a Cleric with a total HP of {health}.");
                                break;
                            case 5:
                                playerClass = "Druid";
                                totalHealth = (8 + (level + 8) + (2 * level) + (2 * level * modifier) - 2) / 2;
                                health = (int)Math.Round(totalHealth, MidpointRounding.AwayFromZero);
                                _outputManager.AddLogEntry($"You have chosen to be a Druid with a total HP of {health}.");
                                break;
                            case 6:
                                playerClass = "Fighter";
                                totalHealth = (10 + (level + 10) + (2 * level) + (2 * level * modifier) - 2) / 2;
                                health = (int)Math.Round(totalHealth, MidpointRounding.AwayFromZero);
                                _outputManager.AddLogEntry($"You have chosen to be a Fighter with a total HP of {health}.");
                                break;
                            case 7:
                                playerClass = "Monk";
                                totalHealth = (8 + (level + 8) + (2 * level) + (2 * level * modifier) - 2) / 2;
                                health = (int)Math.Round(totalHealth, MidpointRounding.AwayFromZero);
                                _outputManager.AddLogEntry($"You have chosen to be a Monk with a total HP of {health}.");
                                break;
                            case 8:
                                playerClass = "Paladin";
                                totalHealth = (10 + (level + 10) + (2 * level) + (2 * level * modifier) - 2) / 2;
                                health = (int)Math.Round(totalHealth, MidpointRounding.AwayFromZero);
                                _outputManager.AddLogEntry($"You have chosen to be a Paladin with a total HP of {health}.");
                                break;
                            case 9:
                                playerClass = "Ranger";
                                totalHealth = (10 + (level + 10) + (2 * level) + (2 * level * modifier) - 2) / 2;
                                health = (int)Math.Round(totalHealth, MidpointRounding.AwayFromZero);
                                _outputManager.AddLogEntry($"You have chosen to be a Ranger with a total HP of {health}.");
                                break;
                            case 10:
                                playerClass = "Revenant";
                                totalHealth = (10 + (level + 10) + (2 * level) + (2 * level * modifier) - 2) / 2;
                                health = (int)Math.Round(totalHealth, MidpointRounding.AwayFromZero);
                                _outputManager.AddLogEntry($"You have chosen to be a Revenant with a total HP of {health}.");
                                break;
                            case 11:
                                playerClass = "Rogue";
                                totalHealth = (8 + (level + 8) + (2 * level) + (2 * level * modifier) - 2) / 2;
                                health = (int)Math.Round(totalHealth, MidpointRounding.AwayFromZero);
                                _outputManager.AddLogEntry($"You have chosen to be a Rogue with a total HP of {health}.");
                                break;
                            case 12:
                                playerClass = "Sorcerer";
                                totalHealth = (6 + (level + 6) + (2 * level) + (2 * level * modifier) - 2) / 2;
                                health = (int)Math.Round(totalHealth, MidpointRounding.AwayFromZero);
                                _outputManager.AddLogEntry($"You have chosen to be a Sorcerer with a total HP of {health}.");
                                break;
                            case 13:
                                playerClass = "Therianthrope";
                                totalHealth = (8 + (level + 8) + (2 * level) + (2 * level * modifier) - 2) / 2;
                                health = (int)Math.Round(totalHealth, MidpointRounding.AwayFromZero);
                                _outputManager.AddLogEntry($"You have chosen to be a Therianthrope with a total HP of {health}.");
                                break;
                            case 14:
                                playerClass = "Vampire";
                                totalHealth = (8 + (level + 8) + (2 * level) + (2 * level * modifier) - 2) / 2;
                                health = (int)Math.Round(totalHealth, MidpointRounding.AwayFromZero);
                                _outputManager.AddLogEntry($"You have chosen to be a Vampire with a total HP of {health}.");
                                break;
                            case 15:
                                playerClass = "Warlock";
                                totalHealth = (8 + (level + 8) + (2 * level) + (2 * level * modifier) - 2) / 2;
                                health = (int)Math.Round(totalHealth, MidpointRounding.AwayFromZero);
                                _outputManager.AddLogEntry($"You have chosen to be a Warlock with a total HP of {health}.");
                                break;
                            case 16:
                                playerClass = "Wizard";
                                totalHealth = (6 + (level + 6) + (2 * level) + (2 * level * modifier) - 2) / 2;
                                health = (int)Math.Round(totalHealth, MidpointRounding.AwayFromZero);
                                _outputManager.AddLogEntry($"You have chosen to be a Wizard with a total HP of {health}.");
                                break;
                            default:
                                _outputManager.AddLogEntry("Invalid selection. Please choose between 0 and 17.");
                                break;
                        }

                        if (classInput >= 1 && classInput < 17)
                        {
                            break;
                        }
                    }
                    catch (Exception e)
                    {
                        _outputManager.AddLogEntry(e.Message);
                    }
                }

                // Class confirmation
                while (true)
                {
                    _outputManager.AddLogEntry("Type 1 to continue.");
                    var continueFromClass = _outputManager.GetUserInput("Continue:");

                    if (continueFromClass == "1")
                    {
                        break;
                    }
                    else
                    {
                        _outputManager.AddLogEntry("Invalid input.");
                    }
                }

                // Weapon input
                List<Item> weapons = _context.Items.Where(w => w.Type == "Weapon").ToList();
                List<Item> untakenWeapons = new List<Item>();
                Item chosen = new Item();

                foreach (Item freeWeapon in weapons)
                {
                    foreach (Player equippedPlayer in players)
                    {
                        if (freeWeapon.Id != equippedPlayer.Equipment.WeaponId || equippedPlayer.Equipment.Weapon == null)
                        {
                            untakenWeapons.Add(freeWeapon);
                        }
                    }
                }

                List<Item> untakenWeaponsFinal = untakenWeapons.GroupBy(n => n.Name).Select(a => a.First()).ToList();

                if (untakenWeaponsFinal.Count > 1)
                {
                    for (int i = 0; i < untakenWeaponsFinal.Count; ++i)
                    {
                        _outputManager.AddLogEntry($"{i + 1}. {untakenWeaponsFinal.ElementAt(i).Name}, Attack: {untakenWeaponsFinal.ElementAt(i).Attack}, Defense: {untakenWeaponsFinal.ElementAt(i).Defense}");
                    }

                    // Selection
                    while (true)
                    {
                        try
                        {
                            _outputManager.AddLogEntry("\nChoose a weapon to equip.");
                            int chosenNumber = Convert.ToInt32(_outputManager.GetUserInput("Selection:"));

                            if (chosenNumber > 0 && chosenNumber <= untakenWeaponsFinal.Count)
                            {
                                _outputManager.AddLogEntry($"You have equipped {untakenWeaponsFinal.ElementAt(chosenNumber - 1).Name}");
                                weapon = untakenWeaponsFinal.ElementAt(chosenNumber - 1);
                                break;
                            }
                            else
                            {
                                _outputManager.AddLogEntry($"Invalid input. Please choose a number betwen 0 and {untakenWeaponsFinal.Count + 1}.");
                            }
                        }
                        catch (Exception e)
                        {
                            _outputManager.AddLogEntry(e.Message);
                        }
                    }
                }
                else if (untakenWeaponsFinal.Count == 1)
                {
                    _outputManager.AddLogEntry($"Weapon: {untakenWeaponsFinal.ElementAt(0).Name}, Attack: {untakenWeaponsFinal.ElementAt(0).Attack}, Defense: {untakenWeaponsFinal.ElementAt(0).Defense}");
                    weapon = untakenWeaponsFinal.ElementAt(0);
                }
                else if (untakenWeaponsFinal.Count == 0)
                {
                    _outputManager.AddLogEntry("There are no available weapons.");
                }

                // Weapon confirmation
                while (true)
                {
                    var input = _outputManager.GetUserInput("Type 1 to continue.");

                    if (input == "1")
                    {
                        break;
                    }
                    else
                    {
                        _outputManager.AddLogEntry("Invalid input.");
                    }
                }

                // Armor input
                List<Item> freeArmor = _context.Items.Where(a => a.Type == "Armor").ToList();
                List<Item> untakenArmor = new List<Item>();

                foreach (Item armorPiece in freeArmor)
                {
                    foreach (Player equippedPlayer in players)
                    {
                        if (armorPiece.Id != equippedPlayer.Equipment.ArmorId || equippedPlayer.Equipment.Armor == null)
                        {
                            untakenArmor.Add(armorPiece);
                        }
                    }
                }

                List<Item> untakenArmorFinal = untakenArmor.GroupBy(n => n.Name).Select(a => a.First()).ToList();

                if (untakenArmorFinal.Count > 1)
                {
                    for (int i = 0; i < untakenArmorFinal.Count; ++i)
                    {
                        _outputManager.AddLogEntry($"{i + 1}. {untakenArmorFinal.ElementAt(i).Name}, Attack: {untakenArmorFinal.ElementAt(i).Attack}, Defense: {untakenArmorFinal.ElementAt(i).Defense}");
                    }

                    // Selection
                    while (true)
                    {
                        try
                        {
                            _outputManager.AddLogEntry("\nChoose a weapon to equip.");
                            int chosenNumber = Convert.ToInt32(_outputManager.GetUserInput("Selection:"));

                            if (chosenNumber > 0 && chosenNumber <= untakenArmorFinal.Count)
                            {
                                _outputManager.AddLogEntry($"You have equipped {untakenArmorFinal.ElementAt(chosenNumber - 1).Name}.");
                                armor = untakenArmorFinal.ElementAt(chosenNumber - 1);
                                break;
                            }
                            else
                            {
                                _outputManager.AddLogEntry($"Invalid input. Please choose a number betwen 0 and {untakenArmorFinal.Count + 1}");
                            }
                        }
                        catch (Exception e)
                        {
                            _outputManager.AddLogEntry(e.Message);
                        }
                    }
                }
                else if (untakenArmorFinal.Count == 1)
                {
                    _outputManager.AddLogEntry($"Armor: {untakenArmorFinal.ElementAt(0).Name}, Attack: {untakenArmorFinal.ElementAt(0).Attack}, Defense: {untakenArmorFinal.ElementAt(0).Defense}");
                    armor = untakenArmorFinal.ElementAt(0);
                }
                else if (untakenArmorFinal.Count == 0)
                {
                    _outputManager.AddLogEntry("There are no available pieces of armor.");
                }

                // Confirmation
                while (true)
                {
                    var input = _outputManager.GetUserInput("Type 1 to continue.");

                    if (input == "1")
                    {
                        break;
                    }
                    else
                    {
                        _outputManager.AddLogEntry("Invalid input.");
                    }
                }

                // Room input
                List<Room> freeRooms = _context.Rooms.Where(p => p.PlayerId == null && !p.Name.Equals("")).ToList();

                if (freeRooms.Count > 1)
                {
                    for (int i = 0; i < freeRooms.Count; ++i)
                    {
                        _outputManager.AddLogEntry($"{i + 1}. {freeRooms.ElementAt(i).Name}");
                    }

                    while (true)
                    {
                        try
                        {
                            _outputManager.AddLogEntry("Choose the starting room for your character.");
                            int roomId = Convert.ToInt32(_outputManager.GetUserInput("Selection:"));

                            if (roomId > 0 && roomId <= freeRooms.Count)
                            {
                                _outputManager.AddLogEntry($"You have chosen the {freeRooms.ElementAt(roomId - 1).Name}.");
                                room = freeRooms.ElementAt(roomId - 1);
                                break;
                            }
                            else
                            {
                                _outputManager.AddLogEntry($"Invalid input. Please choose between 0 and {freeRooms.Count + 1}.");
                            }
                        }
                        catch (Exception e)
                        {
                            _outputManager.AddLogEntry(e.Message);
                        }
                    }
                }
                else if (freeRooms.Count == 1)
                {
                    _outputManager.AddLogEntry($"The only room available is {freeRooms.ElementAt(0).Name}.");
                    room = freeRooms.ElementAt(0);
                }
                else
                {
                    _outputManager.AddLogEntry("There are no free rooms.");
                }

                // Confirmation
                while (true)
                {
                    _outputManager.AddLogEntry("Type 1 to continue.");
                    var continueFromRoom = _outputManager.GetUserInput("Continue:");

                    if (continueFromRoom == "1")
                    {
                        break;
                    }
                    else
                    {
                        _outputManager.AddLogEntry("Invalid input.");
                    }
                }

                // Ability input
                int abilityInput = 0;

                while (true)
                {
                    // Type input
                    while (true)
                    {
                        _outputManager.AddLogEntry("Ability types:");
                        _outputManager.AddLogEntry("1. Bite");
                        _outputManager.AddLogEntry("2. Bubble");
                        _outputManager.AddLogEntry("3. Drain");
                        _outputManager.AddLogEntry("4. Fire");
                        _outputManager.AddLogEntry("5. Heal");
                        _outputManager.AddLogEntry("6. Lightning");
                        _outputManager.AddLogEntry("7. Mist");
                        _outputManager.AddLogEntry("8. Rage");
                        _outputManager.AddLogEntry("9. Shove");

                        try
                        {
                            _outputManager.AddLogEntry($"Create an ability for your character.\nRemember, they are a {race.ToLower()} {playerClass.ToLower()}.");
                            abilityInput = Convert.ToInt32(_outputManager.GetUserInput("Selection:"));

                            switch (abilityInput)
                            {
                                case 1:
                                    abilityName = "Bite";
                                    abilityType = "BiteAbility";
                                    break;
                                case 2:
                                    abilityName = "Bubble";
                                    abilityType = "BubbleAbility";
                                    dodge = true;
                                    break;
                                case 3:
                                    abilityName = "Drain";
                                    abilityType = "DrainAbility";
                                    break;
                                case 4:
                                    abilityName = "Fire";
                                    abilityType = "FireAbility";
                                    break;
                                case 5:
                                    abilityName = "Heal";
                                    abilityType = "HealAbility";
                                    break;
                                case 6:
                                    abilityName = "Lightning";
                                    abilityType = "LightningAbility";
                                    break;
                                case 7:
                                    abilityName = "Mist";
                                    abilityType = "MistAbility";
                                    dodge = true;
                                    break;
                                case 8:
                                    abilityName = "Rage";
                                    abilityType = "RageAbility";
                                    break;
                                case 9:
                                    abilityName = "Shove";
                                    abilityType = "ShoveAbility";
                                    break;
                                default:
                                    _outputManager.AddLogEntry("Invalid selection. Please choose between 0 and 10.");
                                    break;
                            }

                            if (abilityInput > 0 && abilityInput < 10)
                            {
                                _outputManager.AddLogEntry($"You have chosen a {abilityName} ability.");
                                break;
                            }
                        }
                        catch (Exception e)
                        {
                            _outputManager.AddLogEntry(e.Message);
                        }
                    }

                    // Type confirmation
                    while (true)
                    {
                        _outputManager.AddLogEntry("Type 1 to continue.");
                        var continueFromType = _outputManager.GetUserInput("Continue:");

                        if (continueFromType == "1")
                        {
                            break;
                        }
                        else
                        {
                            _outputManager.AddLogEntry("Invalid input.");
                        }
                    }

                    // Description input
                    while (true)
                    {
                        _outputManager.AddLogEntry("Add a descriptive name to your ability.");
                        description = _outputManager.GetUserInput("Description:").ToString();

                        if (description.IsNullOrEmpty())
                        {
                            _outputManager.AddLogEntry("You must describe your ability before you can move forward.");
                        }
                        else
                        {
                            _outputManager.AddLogEntry($"You\'re ability\'s description is {description}.");
                            break;
                        }
                    }

                    // Description confirmation
                    while (true)
                    {
                        _outputManager.AddLogEntry("Type 1 to continue.");
                        var continueFromDescription = _outputManager.GetUserInput("Continue:");

                        if (continueFromDescription == "1")
                        {
                            break;
                        }
                        else
                        {
                            _outputManager.AddLogEntry("Invalid input.");
                        }
                    }

                    // Bonus input
                    if (abilityInput == 1 || ((abilityInput > 2 && (abilityInput < 5) || (abilityInput == 6 || (abilityInput > 7) && (abilityInput < 9)))))
                    {
                        while (true)
                        {
                            try
                            {
                                _outputManager.AddLogEntry("Set the ability\'s attack bonus.");
                                damage = Convert.ToInt32(_outputManager.GetUserInput("Attack Bonus:"));

                                if (damage > 0)
                                {
                                    break;
                                }
                                else
                                {
                                    _outputManager.AddLogEntry("Invalid input.");
                                }
                            }
                            catch (Exception e)
                            {
                                _outputManager.AddLogEntry(e.Message);
                            }
                        }

                        _outputManager.AddLogEntry($"You\'re ability\'s attack is {damage}.");
                    }
                    else if (abilityInput == 9)
                    {
                        while (true)
                        {
                            try
                            {
                                _outputManager.AddLogEntry("Set the ability\'s attack bonus.");
                                damage = Convert.ToInt32(_outputManager.GetUserInput("Attack Bonus:"));

                                if (damage > 0)
                                {
                                    break;
                                }
                                else
                                {
                                    _outputManager.AddLogEntry("Invalid input.");
                                }
                            }
                            catch (Exception e)
                            {
                                _outputManager.AddLogEntry(e.Message);
                            }
                        }

                        while (true)
                        {
                            try
                            {
                                _outputManager.AddLogEntry("Set the amount of feet the target will got knocked back by.");
                                distance = Convert.ToInt32(_outputManager.GetUserInput("Distance:"));

                                if (distance >= 0)
                                {
                                    break;
                                }
                                else
                                {
                                    _outputManager.AddLogEntry("Invalid input.");
                                }
                            }
                            catch (Exception e)
                            {
                                _outputManager.AddLogEntry(e.Message);
                            }
                        }

                        _outputManager.AddLogEntry($"You\'re ability\'s attack is {damage} with a knockback of {distance} ft.");
                    }
                    else if (abilityInput == 5)
                    {
                        if (playerClass.Equals("Sorcerer") || playerClass.Equals("Wizard"))
                        {
                            damage = 6 + modifier;
                        }
                        else if (playerClass.Equals("Artificer") || playerClass.Equals("Bard") || playerClass.Equals("Cleric") || playerClass.Equals("Druid") || playerClass.Equals("Monk") || playerClass.Equals("Rogue") || playerClass.Equals("Therianthrope") || playerClass.Equals("Vampire") || playerClass.Equals("Warlock"))
                        {
                            damage = 8 + modifier;
                        }
                        else if (playerClass.Equals("Fighter") || playerClass.Equals("Paladin") || playerClass.Equals("Ranger") || playerClass.Equals("Revenant"))
                        {
                            damage = 10 + modifier;
                        }
                        else if (playerClass.Equals("Barbarain"))
                        {
                            damage = 12 + modifier;
                        }

                        _outputManager.AddLogEntry($"You\'re ability\'s attack is {damage}.");
                    }
                    else if (abilityInput == 2)
                    {
                        while (true)
                        {
                            try
                            {
                                _outputManager.AddLogEntry("Set the ability\'s defense bonus.");
                                defense = Convert.ToInt32(_outputManager.GetUserInput("Defense Bonus:"));

                                if (defense > 0)
                                {
                                    break;
                                }
                                else
                                {
                                    _outputManager.AddLogEntry("Invalid input.");
                                }
                            }
                            catch (Exception e)
                            {
                                _outputManager.AddLogEntry(e.Message);
                            }
                        }

                        _outputManager.AddLogEntry($"You\'re ability\'s defense is {defense}.");
                    }

                    // Bonus confirmation
                    while (true)
                    {
                        _outputManager.AddLogEntry("Type 1 to continue.");
                        var continueFromRace = _outputManager.GetUserInput("Continue:");

                        if (continueFromRace == "1")
                        {
                            break;
                        }
                        else
                        {
                            _outputManager.AddLogEntry("Invalid input.");
                        }
                    }

                    // Results
                    while (true)
                    {
                        try
                        {
                            _outputManager.AddLogEntry("Ability results:");

                            if (abilityInput == 9)
                            {
                                _outputManager.AddLogEntry($"     Name: {abilityName}");
                                _outputManager.AddLogEntry($"     Description {description}");
                                _outputManager.AddLogEntry($"     Ability Type: {abilityType}");
                                _outputManager.AddLogEntry($"     Damage: {damage}");
                                _outputManager.AddLogEntry($"     Defense: {defense}");
                                _outputManager.AddLogEntry($"     Distance: {distance}");
                            }
                            else
                            {
                                _outputManager.AddLogEntry($"     Name: {abilityName}");
                                _outputManager.AddLogEntry($"     Description {description}");
                                _outputManager.AddLogEntry($"     Ability Type: {abilityType}");
                                _outputManager.AddLogEntry($"     Damage: {damage}");
                                _outputManager.AddLogEntry($"     Defense: {defense}");
                            }
                            _outputManager.AddLogEntry($"\nAre you okay with this setup?");
                            _outputManager.AddLogEntry($"1. Yes");
                            _outputManager.AddLogEntry($"1. No");
                            var finalizeAbility = _outputManager.GetUserInput("Selection:");

                            switch (finalizeAbility)
                            {
                                case "1":
                                    _outputManager.AddLogEntry($"{description} has been created.");
                                    complete = true;
                                    break;
                                case "2":
                                    break;
                                default:
                                    _outputManager.AddLogEntry("Invalid selection. Please choose 1 or 2.");
                                    break;
                            }

                            if (finalizeAbility == "1" || finalizeAbility == "2")
                            {
                                break;
                            }
                        }
                        catch (Exception e)
                        {
                            _outputManager.AddLogEntry(e.Message);
                        }
                    }

                    if (complete)
                    {
                        break;
                    }
                }

                // Result confirmation
                while (true)
                {
                    _outputManager.AddLogEntry("Type 1 to continue.");
                    var continueFromAbility = _outputManager.GetUserInput("Continue:");

                    if (continueFromAbility == "1")
                    {
                        break;
                    }
                    else
                    {
                        _outputManager.AddLogEntry("Invalid input.");
                    }
                }

                // Results
                while (true)
                {
                    _outputManager.AddLogEntry("Character results:");
                    if (weapon != null && armor != null)
                    {
                        _outputManager.AddLogEntry($"     Name: {playerName}");
                        _outputManager.AddLogEntry($"     Race: {race}");
                        _outputManager.AddLogEntry($"     Class {playerClass}");
                        _outputManager.AddLogEntry($"     Health: {health}");
                        _outputManager.AddLogEntry($"     Weapon: {weapon.Name}");
                        _outputManager.AddLogEntry($"     Armor: {armor.Name}");
                        _outputManager.AddLogEntry($"     Ability: {description}");
                    }
                    else if (weapon != null && armor == null)
                    {
                        _outputManager.AddLogEntry($"     Name: {playerName}");
                        _outputManager.AddLogEntry($"     Race: {race}");
                        _outputManager.AddLogEntry($"     Class {playerClass}");
                        _outputManager.AddLogEntry($"     Health: {health}");
                        _outputManager.AddLogEntry($"     Weapon: {weapon.Name}");
                        _outputManager.AddLogEntry($"     Ability: {description}");
                    }
                    else if (weapon == null && armor != null)
                    {
                        _outputManager.AddLogEntry($"     Name: {playerName}");
                        _outputManager.AddLogEntry($"     Race: {race}");
                        _outputManager.AddLogEntry($"     Class {playerClass}");
                        _outputManager.AddLogEntry($"     Health: {health}");
                        _outputManager.AddLogEntry($"     Armor: {armor.Name}");
                        _outputManager.AddLogEntry($"     Ability: {description}");
                    }
                    else
                    {
                        _outputManager.AddLogEntry($"     Name: {playerName}");
                        _outputManager.AddLogEntry($"     Race: {race}");
                        _outputManager.AddLogEntry($"     Class {playerClass}");
                        _outputManager.AddLogEntry($"     Health: {health}");
                        _outputManager.AddLogEntry($"     Ability: {description}");
                    }
                    _outputManager.AddLogEntry("\nAre you okay with this setup?");
                    _outputManager.AddLogEntry("1. Yes");
                    _outputManager.AddLogEntry("2. No");
                    var finalizeCharacter = _outputManager.GetUserInput("Selection:");

                    switch (finalizeCharacter)
                    {
                        case "1":
                            if (abilityInput == 9 && weapon != null && armor != null)
                            {
                                newCharacter.Name = playerName;
                                newCharacter.Race = race;
                                newCharacter.Class = playerClass;
                                newCharacter.Experience = 0;
                                newCharacter.Health = health;
                                newCharacter.Modifier = modifier;
                                newCharacter.Equipment = new Equipment()
                                {
                                    Weapon = new Item()
                                    {
                                        Name = weapon.Name,
                                        Type = weapon.Type,
                                        Attack = weapon.Attack,
                                        Defense = weapon.Defense,
                                        Weight = weapon.Weight,
                                        Value = weapon.Value
                                    },
                                    WeaponId = weapon.Id,
                                    Armor = new Item()
                                    {
                                        Name = armor.Name,
                                        Type = armor.Type,
                                        Attack = armor.Attack,
                                        Defense = armor.Defense,
                                        Weight = armor.Weight,
                                        Value = armor.Value
                                    },
                                    ArmorId = armor.Id
                                };
                                newCharacter.Room = new Room()
                                {
                                    Name = room.Name,
                                    Description = room.Description,
                                    North = room.North,
                                    NorthId = room.NorthId,
                                    South = room.South,
                                    SouthId = room.SouthId,
                                    East = room.East,
                                    EastId = room.EastId,
                                    West = room.West,
                                    WestId = room.WestId,
                                    PlayerId = newCharacter.Id,
                                    Players = room.Players.ToList(),
                                    Monsters = room.Monsters.ToList()
                                };
                                newCharacter.Abilities = new List<Ability>()
                                {
                                    new Ability()
                                    {
                                        Name = abilityName,
                                        Description = description,
                                        AbilityType = abilityType,
                                        Damage = damage,
                                        Defense = defense,
                                        Distance = distance,
                                        Dodge = dodge,
                                        InUse = inUse,
                                        Players = new List<Player>()
                                    }
                                };
                            }
                            else if (abilityInput == 9 && weapon != null && armor == null)
                            {
                                newCharacter.Name = playerName;
                                newCharacter.Race = race;
                                newCharacter.Class = playerClass;
                                newCharacter.Experience = 0;
                                newCharacter.Health = health;
                                newCharacter.Modifier = modifier;
                                newCharacter.Equipment = new Equipment()
                                {
                                    Weapon = new Item()
                                    {
                                        Name = weapon.Name,
                                        Type = weapon.Type,
                                        Attack = weapon.Attack,
                                        Defense = weapon.Defense,
                                        Weight = weapon.Weight,
                                        Value = weapon.Value
                                    },
                                    WeaponId = weapon.Id
                                };
                                newCharacter.Room = new Room()
                                {
                                    Name = room.Name,
                                    Description = room.Description,
                                    North = room.North,
                                    NorthId = room.NorthId,
                                    South = room.South,
                                    SouthId = room.SouthId,
                                    East = room.East,
                                    EastId = room.EastId,
                                    West = room.West,
                                    WestId = room.WestId,
                                    PlayerId = newCharacter.Id,
                                    Players = room.Players.ToList(),
                                    Monsters = room.Monsters.ToList()
                                };
                                newCharacter.Abilities = new List<Ability>()
                                {
                                    new Ability()
                                    {
                                        Name = abilityName,
                                        Description = description,
                                        AbilityType = abilityType,
                                        Damage = damage,
                                        Defense = defense,
                                        Distance = distance,
                                        Dodge = dodge,
                                        InUse = inUse,
                                        Players = new List<Player>()
                                    }
                                };
                            }
                            if (abilityInput == 9 && weapon == null && armor != null)
                            {
                                newCharacter.Name = playerName;
                                newCharacter.Race = race;
                                newCharacter.Class = playerClass;
                                newCharacter.Experience = 0;
                                newCharacter.Health = health;
                                newCharacter.Modifier = modifier;
                                newCharacter.Equipment = new Equipment()
                                {
                                    Armor = new Item()
                                    {
                                        Name = armor.Name,
                                        Type = armor.Type,
                                        Attack = armor.Attack,
                                        Defense = armor.Defense,
                                        Weight = armor.Weight,
                                        Value = armor.Value
                                    },
                                    ArmorId = armor.Id
                                };
                                newCharacter.Room = new Room()
                                {
                                    Name = room.Name,
                                    Description = room.Description,
                                    North = room.North,
                                    NorthId = room.NorthId,
                                    South = room.South,
                                    SouthId = room.SouthId,
                                    East = room.East,
                                    EastId = room.EastId,
                                    West = room.West,
                                    WestId = room.WestId,
                                    PlayerId = newCharacter.Id,
                                    Players = room.Players.ToList(),
                                    Monsters = room.Monsters.ToList()
                                };
                                newCharacter.Abilities = new List<Ability>()
                                {
                                    new Ability()
                                    {
                                        Name = abilityName,
                                        Description = description,
                                        AbilityType = abilityType,
                                        Damage = damage,
                                        Defense = defense,
                                        Distance = distance,
                                        Dodge = dodge,
                                        InUse = inUse,
                                        Players = new List<Player>()
                                    }
                                };
                            }
                            else if (abilityInput == 9 && weapon == null && armor == null)
                            {
                                newCharacter.Name = playerName;
                                newCharacter.Race = race;
                                newCharacter.Class = playerClass;
                                newCharacter.Experience = 0;
                                newCharacter.Health = health;
                                newCharacter.Modifier = modifier;
                                newCharacter.Equipment = new Equipment();
                                newCharacter.Room = new Room()
                                {
                                    Name = room.Name,
                                    Description = room.Description,
                                    North = room.North,
                                    NorthId = room.NorthId,
                                    South = room.South,
                                    SouthId = room.SouthId,
                                    East = room.East,
                                    EastId = room.EastId,
                                    West = room.West,
                                    WestId = room.WestId,
                                    PlayerId = newCharacter.Id,
                                    Players = room.Players.ToList(),
                                    Monsters = room.Monsters.ToList()
                                };
                                newCharacter.Abilities = new List<Ability>()
                                {
                                    new Ability()
                                    {
                                        Name = abilityName,
                                        Description = description,
                                        AbilityType = abilityType,
                                        Damage = damage,
                                        Defense = defense,
                                        Distance = distance,
                                        Dodge = dodge,
                                        InUse = inUse,
                                        Players = new List<Player>()
                                    }
                                };
                            }
                            else if (abilityInput != 9 && weapon != null && armor != null)
                            {
                                newCharacter.Name = playerName;
                                newCharacter.Race = race;
                                newCharacter.Class = playerClass;
                                newCharacter.Experience = 0;
                                newCharacter.Health = health;
                                newCharacter.Modifier = modifier;
                                newCharacter.Equipment = new Equipment()
                                {
                                    Weapon = new Item()
                                    {
                                        Name = weapon.Name,
                                        Type = weapon.Type,
                                        Attack = weapon.Attack,
                                        Defense = weapon.Defense,
                                        Weight = weapon.Weight,
                                        Value = weapon.Value
                                    },
                                    WeaponId = weapon.Id,
                                    Armor = new Item()
                                    {
                                        Name = armor.Name,
                                        Type = armor.Type,
                                        Attack = armor.Attack,
                                        Defense = armor.Defense,
                                        Weight = armor.Weight,
                                        Value = armor.Value
                                    },
                                    ArmorId = armor.Id
                                };
                                newCharacter.Room = new Room()
                                {
                                    Name = room.Name,
                                    Description = room.Description,
                                    North = room.North,
                                    NorthId = room.NorthId,
                                    South = room.South,
                                    SouthId = room.SouthId,
                                    East = room.East,
                                    EastId = room.EastId,
                                    West = room.West,
                                    WestId = room.WestId,
                                    PlayerId = newCharacter.Id,
                                    Players = room.Players.ToList(),
                                    Monsters = room.Monsters.ToList()
                                };
                                newCharacter.Abilities = new List<Ability>()
                                {
                                    new Ability()
                                    {
                                        Name = abilityName,
                                        Description = description,
                                        AbilityType = abilityType,
                                        Damage = damage,
                                        Defense = defense,
                                        Dodge = dodge,
                                        InUse = inUse,
                                        Players = new List<Player>()
                                    }
                                };
                            }
                            else if (abilityInput != 9 && weapon != null && armor == null)
                            {
                                newCharacter.Name = playerName;
                                newCharacter.Race = race;
                                newCharacter.Class = playerClass;
                                newCharacter.Experience = 0;
                                newCharacter.Health = health;
                                newCharacter.Modifier = modifier;
                                newCharacter.Equipment = new Equipment()
                                {
                                    Weapon = new Item()
                                    {
                                        Name = weapon.Name,
                                        Type = weapon.Type,
                                        Attack = weapon.Attack,
                                        Defense = weapon.Defense,
                                        Weight = weapon.Weight,
                                        Value = weapon.Value
                                    },
                                    WeaponId = weapon.Id
                                };
                                newCharacter.Room = new Room()
                                {
                                    Name = room.Name,
                                    Description = room.Description,
                                    North = room.North,
                                    NorthId = room.NorthId,
                                    South = room.South,
                                    SouthId = room.SouthId,
                                    East = room.East,
                                    EastId = room.EastId,
                                    West = room.West,
                                    WestId = room.WestId,
                                    PlayerId = newCharacter.Id,
                                    Players = room.Players.ToList(),
                                    Monsters = room.Monsters.ToList()
                                };
                                newCharacter.Abilities = new List<Ability>()
                                {
                                    new Ability()
                                    {
                                        Name = abilityName,
                                        Description = description,
                                        AbilityType = abilityType,
                                        Damage = damage,
                                        Defense = defense,
                                        Dodge = dodge,
                                        InUse = inUse,
                                        Players = new List<Player>()
                                    }
                                };
                            }
                            else if (abilityInput != 9 && weapon == null && armor != null)
                            {
                                newCharacter.Name = playerName;
                                newCharacter.Race = race;
                                newCharacter.Class = playerClass;
                                newCharacter.Experience = 0;
                                newCharacter.Health = health;
                                newCharacter.Modifier = modifier;
                                newCharacter.Equipment = new Equipment()
                                {
                                    Armor = new Item()
                                    {
                                        Name = armor.Name,
                                        Type = armor.Type,
                                        Attack = armor.Attack,
                                        Defense = armor.Defense,
                                        Weight = armor.Weight,
                                        Value = armor.Value
                                    },
                                    ArmorId = armor.Id
                                };
                                newCharacter.Room = new Room()
                                {
                                    Name = room.Name,
                                    Description = room.Description,
                                    North = room.North,
                                    NorthId = room.NorthId,
                                    South = room.South,
                                    SouthId = room.SouthId,
                                    East = room.East,
                                    EastId = room.EastId,
                                    West = room.West,
                                    WestId = room.WestId,
                                    PlayerId = newCharacter.Id,
                                    Players = room.Players.ToList(),
                                    Monsters = room.Monsters.ToList()
                                };
                                newCharacter.Abilities = new List<Ability>()
                                {
                                    new Ability()
                                    {
                                        Name = abilityName,
                                        Description = description,
                                        AbilityType = abilityType,
                                        Damage = damage,
                                        Defense = defense,
                                        Dodge = dodge,
                                        InUse = inUse,
                                        Players = new List<Player>()
                                    }
                                };
                            }
                            else if (abilityInput != 9 && weapon == null && armor == null)
                            {
                                newCharacter.Name = playerName;
                                newCharacter.Race = race;
                                newCharacter.Class = playerClass;
                                newCharacter.Experience = 0;
                                newCharacter.Health = health;
                                newCharacter.Modifier = modifier;
                                newCharacter.Equipment = new Equipment();
                                newCharacter.Room = new Room()
                                {
                                    Name = room.Name,
                                    Description = room.Description,
                                    North = room.North,
                                    NorthId = room.NorthId,
                                    South = room.South,
                                    SouthId = room.SouthId,
                                    East = room.East,
                                    EastId = room.EastId,
                                    West = room.West,
                                    WestId = room.WestId,
                                    PlayerId = newCharacter.Id,
                                    Players = room.Players.ToList(),
                                    Monsters = room.Monsters.ToList()
                                };
                                newCharacter.Abilities = new List<Ability>()
                                {
                                    new Ability()
                                    {
                                        Name = abilityName,
                                        Description = description,
                                        AbilityType = abilityType,
                                        Damage = damage,
                                        Defense = defense,
                                        Dodge = dodge,
                                        InUse = inUse,
                                        Players = new List<Player>()
                                    }
                                };
                            }
                            _playerRepository.AddPlayer(newCharacter);
                            _outputManager.AddLogEntry($"{playerName} has been created.");
                            complete = true;
                            break;
                        case "2":
                            break;
                        default:
                            _outputManager.AddLogEntry("Invalid selection. Please choose 1 or 2.");
                            break;
                    }

                    if (finalizeCharacter == "1" || finalizeCharacter == "2")
                    {
                        break;
                    }
                }

                if (complete)
                {
                    for (int i = 0; i < players.Count; ++i)
                    {
                        players.ElementAt(i).Id = i + 1;
                        _playerRepository.UpdatePlayer(players.ElementAt(i));
                    }
                    break;
                }
            }

            // Result confirmation
            while (true)
            {
                _outputManager.AddLogEntry("Type 1 to continue.");
                var continueFromCharacter = _outputManager.GetUserInput("Continue:");

                if (continueFromCharacter == "1")
                {
                    break;
                }
                else
                {
                    _outputManager.AddLogEntry("Invalid input.");
                }
            }

            return newCharacter;
        }

        public void AbilityCreationLoop(Player player)
        {
            while (true)
            {
                _outputManager.AddLogEntry("Would you like to create another ability for your character?");
                _outputManager.AddLogEntry("1. Yes");
                _outputManager.AddLogEntry("2. No");
                var input = _outputManager.GetUserInput("Selection:");

                switch (input)
                {
                    case "1":
                        _abilityRepository.AddAbility(CreateAbility(player));
                        _playerRepository.UpdatePlayer(player);
                        break;
                    case "2":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please choose 1 or 2.");
                        break;
                }

                if (input == "2")
                {
                    break;
                }
            }
        }

        private Ability CreateAbility(Player player)
        {
            // Variables
            Ability ability = new Ability();
            string name = null;
            string description = null;
            string abilityType = null;
            int damage = 0;
            int defense = 0;
            int distance = 0;
            bool dodge = false;
            bool inUse = false;
            bool complete = false;
            int typeInput = 0;

            while (true)
            {

                // Type input
                while (true)
                {
                    _outputManager.AddLogEntry("Ability types:");
                    _outputManager.AddLogEntry("1. Bite");
                    _outputManager.AddLogEntry("2. Bubble");
                    _outputManager.AddLogEntry("3. Drain");
                    _outputManager.AddLogEntry("4. Fire");
                    _outputManager.AddLogEntry("5. Heal");
                    _outputManager.AddLogEntry("6. Lightning");
                    _outputManager.AddLogEntry("7. Mist");
                    _outputManager.AddLogEntry("8. Rage");
                    _outputManager.AddLogEntry("9. Shove");

                    try
                    {
                        _outputManager.AddLogEntry($"Create an ability for your character.\nRemember, they are a {player.Race.ToLower()} {player.Class.ToLower()}.");
                        typeInput = Convert.ToInt32(_outputManager.GetUserInput("Selection:"));

                        switch (typeInput)
                        {
                            case 1:
                                name = "Bite";
                                abilityType = "BiteAbility";
                                break;
                            case 2:
                                name = "Bubble";
                                abilityType = "BubbleAbility";
                                dodge = true;
                                break;
                            case 3:
                                name = "Drain";
                                abilityType = "DrainAbility";
                                break;
                            case 4:
                                name = "Fire";
                                abilityType = "FireAbility";
                                break;
                            case 5:
                                name = "Heal";
                                abilityType = "HealAbility";
                                break;
                            case 6:
                                name = "Lightning";
                                abilityType = "LightningAbility";
                                break;
                            case 7:
                                name = "Mist";
                                abilityType = "MistAbility";
                                dodge = true;
                                break;
                            case 8:
                                name = "Rage";
                                abilityType = "RageAbility";
                                break;
                            case 9:
                                name = "Shove";
                                abilityType = "ShoveAbility";
                                break;
                            default:
                                _outputManager.AddLogEntry("Invalid selection. Please choose between 0 and 10.");
                                break;
                        }

                        if (typeInput > 0 && typeInput < 10)
                        {
                            break;
                        }
                    }
                    catch (Exception e)
                    {
                        _outputManager.AddLogEntry(e.Message);
                    }
                }

                // Type confirmation
                while (true)
                {
                    _outputManager.AddLogEntry("Type 1 to continue.");
                    var continueFromType = _outputManager.GetUserInput("Continue:");

                    if (continueFromType == "1")
                    {
                        break;
                    }
                    else
                    {
                        _outputManager.AddLogEntry("Invalid input.");
                    }
                }

                // Description input
                while (true)
                {
                    _outputManager.AddLogEntry("Add a descriptive name to your ability.");
                    description = _outputManager.GetUserInput("Description:").ToString();

                    if (description.IsNullOrEmpty())
                    {
                        _outputManager.AddLogEntry("You must describe your ability before you can move forward.");
                    }
                    else
                    {
                        break;
                    }
                }

                // Description confirmation
                while (true)
                {
                    _outputManager.AddLogEntry("Type 1 to continue.");
                    var continueFromDescription = _outputManager.GetUserInput("Continue:");

                    if (continueFromDescription == "1")
                    {
                        break;
                    }
                    else
                    {
                        _outputManager.AddLogEntry("Invalid input.");
                    }
                }

                // Bonus input
                if (typeInput == 1 || (typeInput > 2 && typeInput < 5) || typeInput == 6 || (typeInput > 7 && typeInput < 9))
                {
                    while (true)
                    {
                        try
                        {
                            _outputManager.AddLogEntry("Set the ability\'s attack bonus.");
                            damage = Convert.ToInt32(_outputManager.GetUserInput("Attack Bonus:"));

                            if (damage > 0)
                            {
                                break;
                            }
                            else
                            {
                                _outputManager.AddLogEntry("Invalid input.");
                            }
                        }
                        catch (Exception e)
                        {
                            _outputManager.AddLogEntry(e.Message);
                        }
                    }

                    _outputManager.AddLogEntry($"You\'re ability\'s attack is {damage}.");

                    // Bonus confirmation
                    while (true)
                    {
                        _outputManager.AddLogEntry("Type 1 to continue.");
                        var continueFromBonus = _outputManager.GetUserInput("Continue:");

                        if (continueFromBonus == "1")
                        {
                            break;
                        }
                        else
                        {
                            _outputManager.AddLogEntry("Invalid input.");
                        }
                    }
                }
                else if (typeInput == 9)
                {
                    while (true)
                    {
                        try
                        {
                            _outputManager.AddLogEntry("Set the ability\'s attack bonus.");
                            damage = Convert.ToInt32(_outputManager.GetUserInput("Attack Bonus:"));

                            if (damage > 0)
                            {
                                break;
                            }
                            else
                            {
                                _outputManager.AddLogEntry("Invalid input.");
                            }
                        }
                        catch (Exception e)
                        {
                            _outputManager.AddLogEntry(e.Message);
                        }
                    }

                    while (true)
                    {
                        try
                        {
                            _outputManager.AddLogEntry("Set the amount of feet the target will got knocked back by.");
                            distance = Convert.ToInt32(_outputManager.GetUserInput("Distance:"));

                            if (distance >= 0)
                            {
                                break;
                            }
                            else
                            {
                                _outputManager.AddLogEntry("Invalid input.");
                            }
                        }
                        catch (Exception e)
                        {
                            _outputManager.AddLogEntry(e.Message);
                        }
                    }

                    _outputManager.AddLogEntry($"You\'re ability\'s attack is {damage} with a knockback of {distance} ft.");

                    // Bonus confirmation
                    while (true)
                    {
                        _outputManager.AddLogEntry("Type 1 to continue.");
                        var continueFromBonus = _outputManager.GetUserInput("Continue:");

                        if (continueFromBonus == "1")
                        {
                            break;
                        }
                        else
                        {
                            _outputManager.AddLogEntry("Invalid input.");
                        }
                    }
                }
                else if (typeInput == 5)
                {
                    if (player.Class.Equals("Sorcerer") || player.Class.Equals("Wizard"))
                    {
                        damage = 6 + player.Modifier;
                    }
                    else if (player.Class.Equals("Artificer") || player.Class.Equals("Bard") || player.Class.Equals("Cleric") || player.Class.Equals("Druid") || player.Class.Equals("Monk") || player.Class.Equals("Rogue") || player.Class.Equals("Therianthrope") || player.Class.Equals("Vampire") || player.Class.Equals("Warlock"))
                    {
                        damage = 8 + player.Modifier;
                    }
                    else if (player.Class.Equals("Fighter") || player.Class.Equals("Paladin") || player.Class.Equals("Ranger") || player.Class.Equals("Revenant"))
                    {
                        damage = 10 + player.Modifier;
                    }
                    else if (player.Class.Equals("Barbarain"))
                    {
                        damage = 12 + player.Modifier;
                    }

                    _outputManager.AddLogEntry($"You\'re ability\'s attack is {damage}.");
                }
                else if (typeInput == 2)
                {
                    while (true)
                    {
                        try
                        {
                            _outputManager.AddLogEntry("Set the ability\'s defense bonus.");
                            defense = Convert.ToInt32(_outputManager.GetUserInput("Defense Bonus:"));

                            if (defense > 0)
                            {
                                break;
                            }
                            else
                            {
                                _outputManager.AddLogEntry("Invalid input.");
                            }
                        }
                        catch (Exception e)
                        {
                            _outputManager.AddLogEntry(e.Message);
                        }
                    }

                    _outputManager.AddLogEntry($"You\'re ability\'s defense is {defense}.");

                    // Bonus confirmation
                    while (true)
                    {
                        _outputManager.AddLogEntry("Type 1 to continue.");
                        var continueFromBonus = _outputManager.GetUserInput("Continue:");

                        if (continueFromBonus == "1")
                        {
                            break;
                        }
                        else
                        {
                            _outputManager.AddLogEntry("Invalid input.");
                        }
                    }
                }

                // Results
                while (true)
                {
                    try
                    {
                        _outputManager.AddLogEntry("Ability results:");

                        if (typeInput == 9)
                        {
                            _outputManager.AddLogEntry($"     Name: {name}");
                            _outputManager.AddLogEntry($"     Description {description}");
                            _outputManager.AddLogEntry($"     Ability Type: {abilityType}");
                            _outputManager.AddLogEntry($"     Damage: {damage}");
                            _outputManager.AddLogEntry($"     Defense: {defense}");
                            _outputManager.AddLogEntry($"     Distance: {distance}");
                        }
                        else
                        {
                            _outputManager.AddLogEntry($"     Name: {name}");
                            _outputManager.AddLogEntry($"     Description {description}");
                            _outputManager.AddLogEntry($"     Ability Type: {abilityType}");
                            _outputManager.AddLogEntry($"     Damage: {damage}");
                            _outputManager.AddLogEntry($"     Defense: {defense}");
                        }

                        _outputManager.AddLogEntry($"\nAre you okay with this setup?");
                        _outputManager.AddLogEntry($"1. Yes");
                        _outputManager.AddLogEntry($"1. No");
                        var finalizeAbility = _outputManager.GetUserInput("Selection:");

                        switch (finalizeAbility)
                        {
                            case "1":

                                ability.Name = name;
                                ability.Description = description;
                                ability.AbilityType = abilityType;
                                ability.Damage = damage;
                                ability.Dodge = dodge;
                                ability.InUse = inUse;
                                ability.Players = new List<Player>()
                                    {
                                        player
                                    };
                                player.Abilities.Add(ability);
                                _outputManager.AddLogEntry($"{description} has been created.");
                                complete = true;
                                break;
                            case "2":
                                break;
                            default:
                                _outputManager.AddLogEntry("Invalid selection. Please choose 1 or 2.");
                                break;
                        }

                        if (finalizeAbility == "1" || finalizeAbility == "2")
                        {
                            break;
                        }
                    }
                    catch (Exception e)
                    {
                        _outputManager.AddLogEntry(e.Message);
                    }
                }

                if (complete)
                {
                    break;
                }
            }

            // Result confirmation
            while (true)
            {
                _outputManager.AddLogEntry("Type 1 to continue.");
                var continueFromAbility = _outputManager.GetUserInput("Continue:");

                if (continueFromAbility == "1")
                {
                    break;
                }
                else
                {
                    _outputManager.AddLogEntry("Invalid input.");
                }
            }

            return ability;
        }

        public void CreateRoom()
        {
            // Miscellaneous variables
            List<Room> openNorthRooms = _context.Rooms.Where(n => n.North == null).ToList();
            List<Room> openSouthRooms = _context.Rooms.Where(s => s.South == null).ToList();
            List<Room> openEastRooms = _context.Rooms.Where(e => e.East == null).ToList();
            List<Room> openWestRooms = _context.Rooms.Where(w => w.West == null).ToList();
            List<Player> players = _context.Players.ToList();
            List<Monster> monsters = _context.Monsters.ToList();
            List<Player> sameNamePlayers = new List<Player>();
            Random rand = new Random();
            bool complete = false;
            bool roomComplete = false;
            int southRoomNumber = 0;
            int northRoomNumber = 0;
            int westRoomNumber = 0;
            int eastRoomNumber = 0;

            // Input variables
            // Room
            List<Player> roomPlayers = new List<Player>();
            List<Monster> roomMonsters = new List<Monster>();
            Room room = new Room();
            string name = null;
            string description = null;
            Room north = new Room();
            Room south = new Room();
            Room east = new Room();
            Room west = new Room();
            Player chosenPlayer = new Player();

            // Other
            string addCharacter = null;

            // Room creation
            int randomPlayer = 0;
            if (players.Count > 1)
            {
                randomPlayer = rand.Next(0, players.Count);
            }
            Room oldRoom = new Room();
            int chosenMonster = rand.Next(0, monsters.Count);
            var randomMonster = monsters.ElementAt(chosenMonster);
            roomMonsters.Add(randomMonster);

            while (true)
            {
                while (true)
                {
                    // Name input
                    while (true)
                    {
                        _outputManager.AddLogEntry("Enter the name of your room.");
                        name = _outputManager.GetUserInput("Name:").ToString();

                        if (name.IsNullOrEmpty())
                        {
                            _outputManager.AddLogEntry("You must name your room to continue.");
                        }
                        else
                        {
                            _outputManager.AddLogEntry($"You\'re room\'s name is the {name}.");
                            break;
                        }
                    }

                    // Name confirmation
                    while (true)
                    {
                        _outputManager.AddLogEntry("Type 1 to continue.");
                        var continueFromName = _outputManager.GetUserInput("Continue:");

                        if (continueFromName == "1")
                        {
                            break;
                        }
                        else
                        {
                            _outputManager.AddLogEntry("Invalid input.");
                        }
                    }

                    // Description input
                    while (true)
                    {
                        _outputManager.AddLogEntry("Enter a brief descripiton of the contents of your room.");
                        description = _outputManager.GetUserInput("Description:").ToString();

                        if (description.IsNullOrEmpty())
                        {
                            _outputManager.AddLogEntry("You must described your room to continue.");
                        }
                        else
                        {
                            if (!description.EndsWith("."))
                            {
                                description += ".";
                            }

                            _outputManager.AddLogEntry($"You\'re room\'s description is: {description}");
                            break;
                        }
                    }

                    // Name confirmation
                    while (true)
                    {
                        _outputManager.AddLogEntry("Type 1 to continue.");
                        var continueFromDescription = _outputManager.GetUserInput("Continue:");

                        if (continueFromDescription == "1")
                        {
                            break;
                        }
                        else
                        {
                            _outputManager.AddLogEntry("Invalid input.");
                        }
                    }


                    // Determining room location
                    while (true)
                    {
                        // North of room
                        if (openSouthRooms.Count > 1)
                        {
                            while (true)
                            {
                                try
                                {
                                    for (int i = 0; i < openSouthRooms.Count; ++i)
                                    {
                                        _outputManager.AddLogEntry($"{i + 1}. {openSouthRooms.ElementAt(i).Name}");
                                    }

                                    _outputManager.AddLogEntry("Choose which room your room is south of.\nInput 0 if there isn't a room.");
                                    southRoomNumber = Convert.ToInt32(_outputManager.GetUserInput("Selection:"));

                                    if (southRoomNumber < 0 || southRoomNumber > openSouthRooms.Count)
                                    {
                                        _outputManager.AddLogEntry($"Invalid selection. Please choose between -1 and {openNorthRooms.Count + 1}.");
                                    }
                                    else if (southRoomNumber == 0)
                                    {
                                        _outputManager.AddLogEntry($"{name} does not connect south to another room.");
                                        north = null;
                                        break;
                                    }
                                    else if (southRoomNumber > 0 && southRoomNumber <= openSouthRooms.Count)
                                    {
                                        _outputManager.AddLogEntry($"{name} is south of {openSouthRooms.ElementAt(southRoomNumber - 1).Name}.");
                                        north = openSouthRooms.ElementAt(southRoomNumber - 1);
                                        break;
                                    }
                                }
                                catch (Exception e)
                                {
                                    _outputManager.AddLogEntry(e.Message);
                                }
                            }
                        }
                        else if (openSouthRooms.Count == 1)
                        {
                            _outputManager.AddLogEntry($"{name} is south of {openSouthRooms.ElementAt(0).Name}.");
                            north = openSouthRooms.ElementAt(0);
                        }
                        else if (openSouthRooms.Count == 0)
                        {
                            _outputManager.AddLogEntry($"There are no available rooms for your room to connect south to.");
                        }

                        // North room confirmation
                        while (true)
                        {
                            _outputManager.AddLogEntry("Type 1 to continue.");
                            var continueFromNorth = _outputManager.GetUserInput("Continue:");

                            if (continueFromNorth == "1")
                            {
                                break;
                            }
                            else
                            {
                                _outputManager.AddLogEntry("Invalid input.");
                            }
                        }

                        // South of room
                        if (openNorthRooms.Count > 1)
                        {
                            while (true)
                            {
                                try
                                {
                                    for (int i = 0; i < openNorthRooms.Count; ++i)
                                    {
                                        _outputManager.AddLogEntry($"{i + 1}. {openNorthRooms.ElementAt(i).Name}");
                                    }

                                    _outputManager.AddLogEntry("Choose which room your room is north of.\nInput 0 if there isn't a room.");
                                    northRoomNumber = Convert.ToInt32(_outputManager.GetUserInput("Selection:"));

                                    if (northRoomNumber < 0 || northRoomNumber > openNorthRooms.Count)
                                    {
                                        _outputManager.AddLogEntry($"Invalid selection. Please choose between -1 and {openNorthRooms.Count + 1}.");
                                    }
                                    else if (northRoomNumber == 0)
                                    {
                                        _outputManager.AddLogEntry($"{name} does not connect north to another room.");
                                        south = null;
                                        break;
                                    }
                                    else if (northRoomNumber > 0 && northRoomNumber <= openNorthRooms.Count)
                                    {
                                        _outputManager.AddLogEntry($"{name} is north of {openNorthRooms.ElementAt(southRoomNumber - 1).Name}.");
                                        south = openNorthRooms.ElementAt(northRoomNumber - 1);
                                        break;
                                    }
                                }
                                catch (Exception e)
                                {
                                    _outputManager.AddLogEntry(e.Message);
                                }
                            }
                        }
                        else if (openNorthRooms.Count == 1)
                        {
                            _outputManager.AddLogEntry($"{name} is north of {openNorthRooms.ElementAt(0).Name}.");
                            south = openNorthRooms.ElementAt(0);
                        }
                        else if (openNorthRooms.Count == 0)
                        {
                            _outputManager.AddLogEntry($"There are no available rooms for your room to connect north to.");
                        }

                        // South room confirmation
                        while (true)
                        {
                            _outputManager.AddLogEntry("Type 1 to continue.");
                            var continueFromSouth = _outputManager.GetUserInput("Continue:");

                            if (continueFromSouth == "1")
                            {
                                break;
                            }
                            else
                            {
                                _outputManager.AddLogEntry("Invalid input.");
                            }
                        }

                        // East of room
                        if (openWestRooms.Count > 1)
                        {
                            while (true)
                            {
                                try
                                {
                                    for (int i = 0; i < openWestRooms.Count; ++i)
                                    {
                                        _outputManager.AddLogEntry($"{i + 1}. {openWestRooms.ElementAt(i).Name}");
                                    }

                                    _outputManager.AddLogEntry("Choose which room your room is west of.\nInput 0 if there isn't a room.");
                                    westRoomNumber = Convert.ToInt32(_outputManager.GetUserInput("Selection:"));

                                    if (westRoomNumber < 0 || westRoomNumber > openWestRooms.Count)
                                    {
                                        _outputManager.AddLogEntry($"Invalid selection. Please choose between 0 and {openWestRooms.Count + 1}.");
                                    }
                                    else if (westRoomNumber == 0)
                                    {
                                        _outputManager.AddLogEntry($"{name} does not connect west to another room.");
                                        east = null;
                                        break;
                                    }
                                    else if (westRoomNumber > 0 && westRoomNumber <= openWestRooms.Count)
                                    {
                                        _outputManager.AddLogEntry($"{openWestRooms.ElementAt(westRoomNumber - 1).Name} is west of {name}.");
                                        east = openWestRooms.ElementAt(westRoomNumber - 1);
                                        break;
                                    }
                                }
                                catch (Exception e)
                                {
                                    _outputManager.AddLogEntry(e.Message);
                                }
                            }
                        }
                        else if (openWestRooms.Count == 1)
                        {
                            _outputManager.AddLogEntry($"{name} is west of {openWestRooms.ElementAt(0).Name}.");
                            east = openWestRooms.ElementAt(0);
                        }
                        else if (openWestRooms.Count == 0)
                        {
                            _outputManager.AddLogEntry($"There are no available rooms for your room to connect west to.");
                        }

                        // East room confirmation
                        while (true)
                        {
                            _outputManager.AddLogEntry("Type 1 to continue.");
                            var continueFromEast = _outputManager.GetUserInput("Continue:");

                            if (continueFromEast == "1")
                            {
                                break;
                            }
                            else
                            {
                                _outputManager.AddLogEntry("Invalid input.");
                            }
                        }

                        // West of room
                        if (openEastRooms.Count > 1)
                        {
                            while (true)
                            {
                                try
                                {
                                    for (int i = 0; i < openEastRooms.Count; ++i)
                                    {
                                        _outputManager.AddLogEntry($"{i + 1}. {openEastRooms.ElementAt(i).Name}");
                                    }

                                    _outputManager.AddLogEntry("Choose which room your room is east of.\nInput 0 if there isn't a room.");
                                    eastRoomNumber = Convert.ToInt32(_outputManager.GetUserInput("Selection:"));

                                    if (eastRoomNumber < 0 || eastRoomNumber > openEastRooms.Count)
                                    {
                                        _outputManager.AddLogEntry($"Invalid selection. Please choose between 0 and {openEastRooms.Count + 1}.");
                                    }
                                    else if (eastRoomNumber == 0)
                                    {
                                        _outputManager.AddLogEntry($"{name} does not connect east to another room.");
                                        west = null;
                                        break;
                                    }
                                    else if (eastRoomNumber > 0 && eastRoomNumber <= openEastRooms.Count)
                                    {
                                        _outputManager.AddLogEntry($"{openWestRooms.ElementAt(eastRoomNumber - 1).Name} is west of {name}.");
                                        west = openEastRooms.ElementAt(eastRoomNumber - 1);
                                        break;
                                    }
                                }
                                catch (Exception e)
                                {
                                    _outputManager.AddLogEntry(e.Message);
                                }
                            }
                        }
                        else if (openEastRooms.Count == 1)
                        {
                            _outputManager.AddLogEntry($"{name} is east of {openEastRooms.ElementAt(0).Name}.");
                            west = openEastRooms.ElementAt(0);
                        }
                        else if (openEastRooms.Count == 0)
                        {
                            _outputManager.AddLogEntry($"There are no available rooms for your room to connect east to.");
                        }

                        // West room confirmation
                        while (true)
                        {
                            _outputManager.AddLogEntry("Type 1 to continue.");
                            var continueFromEast = _outputManager.GetUserInput("Continue:");

                            if (continueFromEast == "1")
                            {
                                break;
                            }
                            else
                            {
                                _outputManager.AddLogEntry("Invalid input.");
                            }
                        }

                        // Connection output
                        if (southRoomNumber != 0 && northRoomNumber != 0 && westRoomNumber != 0 && eastRoomNumber != 0)
                        {
                            // All four
                            _outputManager.AddLogEntry($"North of your room is {north.Name}, south of your room is {south.Name}, east of your room is {east.Name}, and west of your room is {west.Name}.");
                            break;
                        }
                        else if (southRoomNumber != 0 && northRoomNumber == 0 && westRoomNumber == 0 && eastRoomNumber == 0)
                        {
                            // North
                            _outputManager.AddLogEntry($"North of your room is {north.Name}.");
                            break;
                        }
                        else if (southRoomNumber == 0 && northRoomNumber != 0 && westRoomNumber == 0 && eastRoomNumber == 0)
                        {
                            // South
                            _outputManager.AddLogEntry($"South of your room is {south.Name}.");
                            break;
                        }
                        else if (southRoomNumber == 0 && northRoomNumber == 0 && westRoomNumber != 0 && eastRoomNumber == 0)
                        {
                            // East
                            _outputManager.AddLogEntry($"East of your room is {east.Name}.");
                            break;
                        }
                        else if (southRoomNumber == 0 && northRoomNumber == 0 && westRoomNumber == 0 && eastRoomNumber != 0)
                        {
                            // West
                            _outputManager.AddLogEntry($"West of your room is {west.Name}.");
                            break;
                        }
                        else if (southRoomNumber != 0 && northRoomNumber != 0 && westRoomNumber == 0 && eastRoomNumber == 0)
                        {
                            // North & south
                            _outputManager.AddLogEntry($"North of your room is {north.Name} and south of your room is {south.Name}.");
                            break;
                        }
                        else if (southRoomNumber != 0 && northRoomNumber == 0 && westRoomNumber != 0 && eastRoomNumber == 0)
                        {
                            // North & east
                            _outputManager.AddLogEntry($"North of your room is {north.Name} and east of your room is {east.Name}.");
                            break;
                        }
                        else if (southRoomNumber != 0 && northRoomNumber == 0 && westRoomNumber == 0 && eastRoomNumber != 0)
                        {
                            // North & west
                            _outputManager.AddLogEntry($"North of your room is {north.Name} and west of your room is {west.Name}.");
                            break;
                        }
                        else if (southRoomNumber == 0 && northRoomNumber != 0 && westRoomNumber != 0 && eastRoomNumber == 0)
                        {
                            // South & east
                            _outputManager.AddLogEntry($"South of your room is {south.Name} and east of your room is {east.Name}.");
                            break;
                        }
                        else if (southRoomNumber == 0 && northRoomNumber != 0 && westRoomNumber == 0 && west != null)
                        {
                            // South & west
                            _outputManager.AddLogEntry($"South of your room is {south.Name} and west of your room is {west.Name}.");
                            break;
                        }
                        else if (southRoomNumber == 0 && northRoomNumber == 0 && westRoomNumber != 0 && eastRoomNumber != 0)
                        {
                            // East & west
                            _outputManager.AddLogEntry($"East of your room is {east.Name} and west of your room is {west.Name}.");
                            break;
                        }
                        else if (southRoomNumber != 0 && northRoomNumber != 0 && westRoomNumber != 0 && eastRoomNumber == 0)
                        {
                            // North, south, and east
                            _outputManager.AddLogEntry($"North of your room is {north.Name}, south of your room is {south.Name}, and east of your room is {east.Name}.");
                            break;
                        }
                        else if (southRoomNumber != 0 && northRoomNumber != 0 && westRoomNumber == 0 && eastRoomNumber != 0)
                        {
                            // North, south, and west
                            _outputManager.AddLogEntry($"North of your room is {north.Name}, south of your room is {south.Name}, and west of your room is {west.Name}.");
                            break;
                        }
                        else if (southRoomNumber != 0 && northRoomNumber == 0 && westRoomNumber != 0 && eastRoomNumber != 0)
                        {
                            // North, east, and west
                            _outputManager.AddLogEntry($"North of your room is {north.Name}, east of your room is {east.Name}, and west of your room is {west.Name}.");
                            break;
                        }
                        else if (southRoomNumber == 0 && northRoomNumber != 0 && westRoomNumber != 0 && eastRoomNumber != 0)
                        {
                            // South, east, and west
                            _outputManager.AddLogEntry($"South of your room is {south.Name}, east of your room is {east.Name}, and west of your room is {west.Name}.");
                            break;
                        }
                        else
                        {
                            // None
                            _outputManager.AddLogEntry("You\'re room must connect with another.");
                        }
                    }

                    // Room connection(s) confirmation
                    while (true)
                    {
                        _outputManager.AddLogEntry("Type 1 to continue.");
                        var continueFromName = _outputManager.GetUserInput("Continue:");

                        if (continueFromName == "1")
                        {
                            break;
                        }
                        else
                        {
                            _outputManager.AddLogEntry("Invalid input.");
                        }
                    }

                    // Character input
                    while (true)
                    {
                        _outputManager.AddLogEntry("Would you like to add a character to the room?");
                        _outputManager.AddLogEntry("1. Yes");
                        _outputManager.AddLogEntry("2. No");
                        addCharacter = _outputManager.GetUserInput("Seclection:");

                        switch (addCharacter)
                        {
                            case "1":
                                break;
                            case "2":
                                break;
                            default:
                                _outputManager.AddLogEntry("Invalid input. Please choose 1 or 2.");
                                break;
                        }

                        if (addCharacter == "1" || addCharacter == "2")
                        {
                            break;
                        }
                    }

                    if (addCharacter == "1")
                    {
                        for (int i = 0; i < players.Count; ++i)
                        {
                            _outputManager.AddLogEntry($"{i + 1}. {players.ElementAt(i).Name} - {players.ElementAt(i).Race} {players.ElementAt(i).Class}");
                        }

                        while (true)
                        {
                            _outputManager.AddLogEntry("Type the name of the character you want to add to your room.");
                            string playerName = _outputManager.GetUserInput("Seclection:").ToString();

                            if (playerName.IsNullOrEmpty())
                            {
                                _outputManager.AddLogEntry("You must name your character in order to continue.");
                            }
                            else
                            {
                                foreach (Player namedPlayer in players)
                                {
                                    if (namedPlayer.Name.Equals(playerName))
                                    {
                                        sameNamePlayers.Add(namedPlayer);
                                    }
                                }

                                if (sameNamePlayers.Count > 1)
                                {
                                    for (int j = 0; j < sameNamePlayers.Count; ++j)
                                    {
                                        _outputManager.AddLogEntry($"{j + 1}. {players.ElementAt(j).Name} - {players.ElementAt(j).Race} {players.ElementAt(j).Class}");
                                    }

                                    while (true)
                                    {
                                        try
                                        {
                                            _outputManager.AddLogEntry($"There are {sameNamePlayers.Count} characters by the name of \"{playerName}.\" Select the one you want.");
                                            int iChooseYou = Convert.ToInt32(_outputManager.GetUserInput("Seclection:"));

                                            if (iChooseYou > 0 && iChooseYou <= sameNamePlayers.Count)
                                            {
                                                chosenPlayer = sameNamePlayers.ElementAt(iChooseYou - 1);
                                                roomPlayers.Add(chosenPlayer);
                                                _outputManager.AddLogEntry($"{playerName} has been added to your room.");
                                                break;
                                            }
                                            else
                                            {
                                                _outputManager.AddLogEntry($"Invalid input. Please choose between 0 and {sameNamePlayers.Count}.");
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            _outputManager.AddLogEntry(e.Message);
                                        }
                                    }

                                    break;
                                }
                                else if (sameNamePlayers.Count == 1)
                                {
                                    chosenPlayer = sameNamePlayers.ElementAt(0);
                                    roomPlayers.Add(chosenPlayer);
                                    _outputManager.AddLogEntry($"{playerName} has been added to your room.");
                                    break;
                                }
                                else if (sameNamePlayers.Count == 0)
                                {
                                    _outputManager.AddLogEntry($"There is no character by the name of \"{playerName}.\"");
                                }
                            }
                        }

                        // Confirmation on character added to room
                        while (true)
                        {
                            _outputManager.AddLogEntry("Type 1 to continue.");
                            var continueFromAddCharacter = _outputManager.GetUserInput("Continue:");

                            if (continueFromAddCharacter == "1")
                            {
                                break;
                            }
                            else
                            {
                                _outputManager.AddLogEntry("Invalid input.");
                            }
                        }
                    }
                    else if (addCharacter == "2")
                    {
                        chosenPlayer = players.ElementAt(randomPlayer);
                        oldRoom = chosenPlayer.Room;
                        roomPlayers.Add(chosenPlayer);
                    }

                    // Results
                    while (true)
                    {
                        _outputManager.AddLogEntry("Room results:");
                        _outputManager.AddLogEntry($"     Name: {name}");
                        _outputManager.AddLogEntry($"     Description: {description}");

                        // Room connection output
                        if (southRoomNumber != 0 && northRoomNumber != 0 && westRoomNumber != 0 && eastRoomNumber != 0)
                        {
                            // All four
                            _outputManager.AddLogEntry($"     North Room: {north.Name}");
                            _outputManager.AddLogEntry($"     South Room: {south.Name}");
                            _outputManager.AddLogEntry($"     East Room: {east.Name}");
                            _outputManager.AddLogEntry($"     West Room: {west.Name}");
                        }
                        else if (southRoomNumber != 0 && northRoomNumber == 0 && westRoomNumber == 0 && eastRoomNumber == 0)
                        {
                            // North
                            _outputManager.AddLogEntry($"     North Room: {north.Name}");
                        }
                        else if (southRoomNumber == 0 && northRoomNumber != 0 && westRoomNumber == 0 && eastRoomNumber == 0)
                        {
                            // South
                            _outputManager.AddLogEntry($"     South Room: {south.Name}");
                        }
                        else if (southRoomNumber == 0 && northRoomNumber == 0 && westRoomNumber != 0 && eastRoomNumber == 0)
                        {
                            // East
                            _outputManager.AddLogEntry($"     East Room: {east.Name}");
                        }
                        else if (southRoomNumber == 0 && northRoomNumber == 0 && westRoomNumber == 0 && eastRoomNumber != 0)
                        {
                            // West
                            _outputManager.AddLogEntry($"     West Room: {west.Name}");
                        }
                        else if (southRoomNumber != 0 && northRoomNumber != 0 && westRoomNumber == 0 && eastRoomNumber == 0)
                        {
                            // North & south
                            _outputManager.AddLogEntry($"     North Room: {north.Name}");
                            _outputManager.AddLogEntry($"     South Room: {south.Name}");
                        }
                        else if (southRoomNumber != 0 && northRoomNumber == 0 && westRoomNumber != 0 && eastRoomNumber == 0)
                        {
                            // North & east
                            _outputManager.AddLogEntry($"     North Room: {north.Name}");
                            _outputManager.AddLogEntry($"     East Room: {east.Name}");
                        }
                        else if (southRoomNumber != 0 && northRoomNumber == 0 && westRoomNumber == 0 && eastRoomNumber != 0)
                        {
                            // North & west
                            _outputManager.AddLogEntry($"     North Room: {north.Name}");
                            _outputManager.AddLogEntry($"     West Room: {west.Name}");
                        }
                        else if (southRoomNumber == 0 && northRoomNumber != 0 && westRoomNumber != 0 && eastRoomNumber == 0)
                        {
                            // South & east
                            _outputManager.AddLogEntry($"     South Room: {south.Name}");
                            _outputManager.AddLogEntry($"     East Room: {east.Name}");
                        }
                        else if (southRoomNumber == 0 && northRoomNumber != 0 && westRoomNumber != 0 && eastRoomNumber == 0)
                        {
                            // South & west
                            _outputManager.AddLogEntry($"     South Room: {south.Name}");
                            _outputManager.AddLogEntry($"     West Room: {west.Name}");
                        }
                        else if (southRoomNumber == 0 && northRoomNumber == 0 && westRoomNumber != 0 && eastRoomNumber != 0)
                        {
                            // East & west
                            _outputManager.AddLogEntry($"     East Room: {east.Name}");
                            _outputManager.AddLogEntry($"     West Room: {west.Name}");
                        }
                        else if (southRoomNumber != 0 && northRoomNumber != 0 && westRoomNumber != 0 && eastRoomNumber == 0)
                        {
                            // North, south, and east
                            _outputManager.AddLogEntry($"     North Room: {north.Name}");
                            _outputManager.AddLogEntry($"     South Room: {south.Name}");
                            _outputManager.AddLogEntry($"     East Room: {east.Name}");
                        }
                        else if (southRoomNumber != 0 && northRoomNumber != 0 && westRoomNumber == 0 && eastRoomNumber != 0)
                        {
                            // North, south, and west
                            _outputManager.AddLogEntry($"     North Room: {north.Name}");
                            _outputManager.AddLogEntry($"     South Room: {south.Name}");
                            _outputManager.AddLogEntry($"     West Room: {west.Name}");
                        }
                        else if (southRoomNumber != 0 && northRoomNumber == 0 && westRoomNumber != 0 && eastRoomNumber != 0)
                        {
                            // North, east, and west
                            _outputManager.AddLogEntry($"     North Room: {north.Name}");
                            _outputManager.AddLogEntry($"     East Room: {east.Name}");
                            _outputManager.AddLogEntry($"     West Room: {west.Name}");
                        }
                        else if (southRoomNumber == 0 && northRoomNumber != 0 && westRoomNumber != 0 && eastRoomNumber != 0)
                        {
                            // South, east, and west
                            _outputManager.AddLogEntry($"     South Room: {south.Name}");
                            _outputManager.AddLogEntry($"     East Room: {east.Name}");
                            _outputManager.AddLogEntry($"     West Room: {west.Name}");
                        }

                        _outputManager.AddLogEntry("\nAre you okay with this setup?");
                        _outputManager.AddLogEntry("1. Yes");
                        _outputManager.AddLogEntry("2. No");
                        var finalizeRoom = _outputManager.GetUserInput("Selection:");

                        switch (finalizeRoom)
                        {
                            case "1":
                                room.Name = name;
                                room.Description = description;
                                room.North = north;
                                room.South = south;
                                room.East = east;
                                room.West = west;
                                room.Players = roomPlayers.ToList();
                                room.Monsters = roomMonsters.ToList();
                                roomComplete = true;
                                break;
                            case "2":
                                break;
                            default:
                                _outputManager.AddLogEntry("Invalid selection. Please choose 1 or 2.");
                                break;
                        }

                        if (finalizeRoom == "1" || finalizeRoom == "2")
                        {
                            break;
                        }
                    }

                    if (roomComplete)
                    {
                        _roomRepository.AddRoom(room);
                        _outputManager.AddLogEntry($"{name} has been created.");
                        room.Monsters.Remove(randomMonster);
                        _roomRepository.UpdateRoom(room);
                        if (addCharacter == "1")
                        {
                            chosenPlayer.Room = room;
                            _playerRepository.UpdatePlayer(chosenPlayer);
                        }
                        else if (addCharacter == "2")
                        {
                            room.Players.Remove(chosenPlayer);
                            _roomRepository.UpdateRoom(room);
                            chosenPlayer.Room = oldRoom;
                            _playerRepository.UpdatePlayer(chosenPlayer);
                        }
                        complete = true;
                        break;
                    }
                }

                if (complete)
                {
                    break;
                }
            }

            // Result confirmation
            while (true)
            {
                _outputManager.AddLogEntry("Type 1 to continue.");
                var continueFromRoom = _outputManager.GetUserInput("Continue:");

                if (continueFromRoom == "1")
                {
                    break;
                }
                else
                {
                    _outputManager.AddLogEntry("Invalid input.");
                }
            }
        }
    }
}
