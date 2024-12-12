using ConsoleRpg.Helpers;
using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Characters.Monsters;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Equipments;
using ConsoleRpgEntities.Models.Rooms;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;
using System.Diagnostics;

namespace ConsoleRpg.Services
{
    public class SearchService
    {
        private readonly GameContext _context;
        private readonly OutputManager _outputManager;
        private Table _logTable;
        private Panel _mapPanel;

        public SearchService(GameContext context, OutputManager outputManager)
        {
            _outputManager = outputManager;
            _context = context;
        }

        public void FindItem()
        {
            List<Item> items = _context.Items.ToList();
            List<Player> players = _context.Players.ToList();
            Item chosen = new Item();
            Player wielder = new Player();
            bool itemFound = false;
            bool characterFound = false;

            while (true)
            {
                try
                {
                    _outputManager.AddLogEntry("Type the name of the item you want to find.");
                    var input = _outputManager.GetUserInput("Name:");

                    if (input.Equals("") || input == null)
                    {
                        _outputManager.AddLogEntry("You must name the item to continue.");
                    }
                    else
                    {
                        foreach (Item item in items)
                        {
                            if (item.Name.ToLower().Equals(input.ToLower()))
                            {
                                chosen = item;
                                itemFound = true;
                            }
                        }

                        foreach (Player player in players)
                        {
                            if (player.Equipment.WeaponId == chosen.Id || player.Equipment.ArmorId == chosen.Id)
                            {
                                wielder = player;
                                characterFound = true;
                            }
                        }

                        if (itemFound && characterFound)
                        {
                            _outputManager.AddLogEntry($"{input} is wielded by {wielder.Name}, who is found in the {wielder.Room.Name.ToLower()}");
                            break;
                        }
                        else if (itemFound && characterFound)
                        {
                            _outputManager.AddLogEntry($"No one is currently wielding {input}.");
                            break;
                        }
                        else if (!itemFound && !characterFound)
                        {
                            _outputManager.AddLogEntry($"No one is currently wielding an item called \"{input}.\"");
                            break;
                        }
                    }
                }
                catch (Exception e)
                {
                    _outputManager.AddLogEntry(e.Message);
                }
            }
        }

        public void FindCharactersInRoom(Room currentRoom)
        {
            // Variables
            List<Player> playersInRoom = currentRoom.Players.ToList();
            List<Player> matchingPlayers = new List<Player>();
            List<Ability> playerAbilities = new List<Ability>();
            List<Monster> monstersInRoom = currentRoom.Monsters.ToList();
            List<Monster> matchingMonsters = new List<Monster>();
            Player chosenPlayer = new Player();
            int input = 0;
            int totalAttack = 0;
            int totalDefense = 0;
            int monsterStat = 0;
            string playerOrMonsterInput = null;
            bool complete = false;

            // Input
            while (true)
            {
                while (true)
                {
                    try
                    {
                        _outputManager.AddLogEntry("1. Name");
                        _outputManager.AddLogEntry("2. Race");
                        _outputManager.AddLogEntry("3. Class");
                        _outputManager.AddLogEntry("4. Health");
                        _outputManager.AddLogEntry("5. Attack");
                        _outputManager.AddLogEntry("6. Defense");
                        _outputManager.AddLogEntry("7. Exit");
                        _outputManager.AddLogEntry("Select a method to search for characters in the room.");
                        input = Convert.ToInt32(_outputManager.GetUserInput("Selection:"));

                        switch (input)
                        {
                            case 1:
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            case 4:
                                break;
                            case 5:
                                break;
                            case 6:
                                break;
                            case 7:
                                break;
                            default:
                                _outputManager.AddLogEntry("Invalid selection. Please choose between 0 and 8.");
                                break;
                        }

                        if (input > 0 && input <= 7)
                        {
                            break;
                        }
                    }
                    catch (Exception e)
                    {
                        _outputManager.AddLogEntry(e.Message);
                    }
                }

                if (input != 3 && input != 7)
                {
                    while (true)
                    {
                        _outputManager.AddLogEntry("Select a which type of character to searh for.");
                        _outputManager.AddLogEntry("1. Player Character");
                        _outputManager.AddLogEntry("2. Enemy");
                        playerOrMonsterInput = _outputManager.GetUserInput("Selection:");

                        switch (playerOrMonsterInput)
                        {
                            case "1":
                                break;
                            case "2":
                                break;
                            default:
                                _outputManager.AddLogEntry("Invalid selection. Please choose 1 or 2.");
                                break;
                        }
                    }
                }

                if (input == 1 && playerOrMonsterInput == "1")
                {
                    while (true)
                    {
                        _outputManager.AddLogEntry("Type in the name of the character(s) you want.");
                        string name = _outputManager.GetUserInput("Name:").ToString();

                        if (name.IsNullOrEmpty())
                        {
                            _outputManager.AddLogEntry("You must input the character's name in order to continue.");
                        }
                        else
                        {
                            matchingPlayers = playersInRoom.Where(n => n.Name.Equals(name)).ToList();

                            if (matchingPlayers.Count == 0)
                            {
                                _outputManager.AddLogEntry($"There is no character by the name of \"{name}\" in this room.");
                            }
                            else if (matchingPlayers.Count > 0)
                            {
                                for (int i = 0; i < matchingPlayers.Count; ++i)
                                {
                                    _outputManager.AddLogEntry($"{i + 1}. {matchingPlayers.ElementAt(i).Name}, {matchingPlayers.ElementAt(i).Race} {matchingPlayers.ElementAt(i).Class}");
                                }

                                if (matchingPlayers.Count > 1)
                                {
                                    _outputManager.AddLogEntry($"Total: {matchingPlayers.Count}");
                                }
                            }
                            break;
                        }
                    }
                }
                else if (input == 1 && playerOrMonsterInput == "2")
                {
                    while (true)
                    {
                        _outputManager.AddLogEntry("Type in the name of the character(s) you want.");
                        string name = _outputManager.GetUserInput("Name:").ToString();

                        if (name.IsNullOrEmpty())
                        {
                            _outputManager.AddLogEntry("You must input the character's name in order to continue.");
                        }
                        else
                        {
                            matchingMonsters = monstersInRoom.Where(n => n.Name.Equals(name)).ToList();

                            if (matchingMonsters.Count == 0)
                            {
                                _outputManager.AddLogEntry($"There is no character by the name of \"{name}\" in this room.");
                            }
                            else if (matchingMonsters.Count > 0)
                            {
                                for (int i = 0; i < matchingMonsters.Count; ++i)
                                {
                                    _outputManager.AddLogEntry($"{i + 1}. {matchingMonsters.ElementAt(i).Name}, {matchingMonsters.ElementAt(i).Race}");
                                }

                                if (matchingMonsters.Count > 1)
                                {
                                    _outputManager.AddLogEntry($"Total: {matchingMonsters.Count}");
                                }
                            }
                            break;
                        }
                    }
                }
                else if (input == 2 && playerOrMonsterInput == "1")
                {
                    while (true)
                    {
                        _outputManager.AddLogEntry("Type in the name of the race of the character(s) you want.");
                        string race = _outputManager.GetUserInput("Race:");

                        if (race.Equals("") || race == null)
                        {
                            _outputManager.AddLogEntry($"You must input the name of the race you want to continue.");
                        }
                        else
                        {
                            matchingPlayers.Where(r => r.Race.Equals(race)).ToList();

                            if (matchingPlayers.Count == 0)
                            {
                                _outputManager.AddLogEntry($"There is no race by the name of \"{race}\" in this room.");
                            }
                            else if (matchingPlayers.Count > 0)
                            {
                                for (int i = 0; i < matchingPlayers.Count; ++i)
                                {
                                    _outputManager.AddLogEntry($"{i + 1}. {matchingPlayers.ElementAt(i).Name}, {matchingPlayers.ElementAt(i).Race} {matchingPlayers.ElementAt(i).Class}");
                                }

                                if (matchingPlayers.Count > 1)
                                {
                                    _outputManager.AddLogEntry($"Total: {matchingPlayers.Count}");
                                }
                            }
                            break;
                        }
                    }
                }
                else if (input == 2 && playerOrMonsterInput == "2")
                {
                    while (true)
                    {
                        _outputManager.AddLogEntry("Type in the name of the race of the character(s) you want.");
                        string race = _outputManager.GetUserInput("Race:");

                        if (race.Equals("") || race == null)
                        {
                            _outputManager.AddLogEntry($"You must input the name of the race you want to continue.");
                        }
                        else
                        {
                            matchingMonsters = matchingMonsters.Where(r => r.Race.Equals(race)).ToList();

                            if (matchingMonsters.Count == 0)
                            {
                                _outputManager.AddLogEntry($"There is no race by the name of \"{race}\" in this room.");
                            }
                            else if (matchingMonsters.Count > 0)
                            {
                                for (int i = 0; i < matchingMonsters.Count; ++i)
                                {
                                    _outputManager.AddLogEntry($"{i + 1}. {matchingMonsters.ElementAt(i).Name}, {matchingMonsters.ElementAt(i).Race}");
                                }

                                if (matchingMonsters.Count > 1)
                                {
                                    _outputManager.AddLogEntry($"Total: {matchingMonsters.Count}");
                                }
                            }
                            break;
                        }
                    }
                }
                else if (input == 4 && playerOrMonsterInput == "1")
                {
                    while (true)
                    {
                        try
                        {
                            _outputManager.AddLogEntry("Input the amount of health to search for.");
                            int health = Convert.ToInt32(_outputManager.GetUserInput("Health:"));

                            matchingPlayers = playersInRoom.Where(h => h.Health == health).ToList();

                            if (matchingPlayers.Count == 0)
                            {
                                _outputManager.AddLogEntry($"There is no character with {health} health in this room.");
                                break;
                            }
                            else if (matchingPlayers.Count > 0)
                            {
                                for (int i = 0; i < matchingPlayers.Count; ++i)
                                {
                                    _outputManager.AddLogEntry($"{i + 1}. {matchingPlayers.ElementAt(i).Name}, {matchingPlayers.ElementAt(i).Race} {matchingPlayers.ElementAt(i).Class}");
                                }

                                if (matchingPlayers.Count > 1)
                                {
                                    _outputManager.AddLogEntry($"Total: {matchingPlayers.Count}");
                                }

                                break;
                            }
                        }
                        catch (Exception e)
                        {
                            _outputManager.AddLogEntry(e.Message);
                        }
                    }
                }
                else if (input == 4 && playerOrMonsterInput == "2")
                {
                    while (true)
                    {
                        try
                        {
                            _outputManager.AddLogEntry("Input the amount of health to search for.");
                            int health = Convert.ToInt32(_outputManager.GetUserInput("Health:"));

                            matchingMonsters = monstersInRoom.Where(h => h.Health == health).ToList();

                            if (matchingMonsters.Count == 0)
                            {
                                _outputManager.AddLogEntry($"There is no character with {health} health in this room.");
                                break;
                            }
                            else if (matchingMonsters.Count > 0)
                            {
                                for (int i = 0; i < matchingMonsters.Count; ++i)
                                {
                                    _outputManager.AddLogEntry($"{i + 1}. {matchingMonsters.ElementAt(i).Name}, {matchingMonsters.ElementAt(i).Race}");
                                }

                                if (matchingMonsters.Count > 1)
                                {
                                    _outputManager.AddLogEntry($"Total: {matchingMonsters.Count}");
                                }

                                break;
                            }
                        }
                        catch (Exception e)
                        {
                            _outputManager.AddLogEntry(e.Message);
                        }
                    }
                }
                else if (input == 5 && playerOrMonsterInput == "1")
                {
                    while (true)
                    {
                        _outputManager.AddLogEntry("Type in the attack power of the character(s) you want.");
                        int attack = Convert.ToInt32(_outputManager.GetUserInput("Attack:"));

                        if (attack <= 0)
                        {
                            _outputManager.AddLogEntry("The attack power must be higher than 0.");
                        }
                        else
                        {
                            foreach (Player player in playersInRoom)
                            {
                                playerAbilities = player.Abilities.ToList();

                                if (player.Equipment.Weapon != null && player.Equipment.Armor != null)
                                {
                                    totalAttack += player.Equipment.Weapon.Attack;
                                    totalAttack += player.Equipment.Armor.Attack;
                                }
                                else if (player.Equipment.Weapon != null && player.Equipment.Armor == null)
                                {
                                    totalAttack += player.Equipment.Weapon.Attack;
                                }
                                else if (player.Equipment.Weapon == null && player.Equipment.Armor != null)
                                {
                                    totalAttack += player.Equipment.Armor.Attack;
                                }

                                if (playerAbilities.Count > 0)
                                {
                                    foreach (Ability ability in playerAbilities)
                                    {
                                        totalAttack += ability.Damage;
                                    }
                                }

                                if (attack == totalAttack)
                                {
                                    matchingPlayers.Add(player);
                                }
                            }

                            if (matchingPlayers.Count == 0)
                            {
                                _outputManager.AddLogEntry($"There is no character with an attack power of {attack} in this room.");
                            }
                            else if (matchingPlayers.Count > 0)
                            {
                                for (int i = 0; i < matchingPlayers.Count; ++i)
                                {
                                    _outputManager.AddLogEntry($"{i + 1}. {matchingPlayers.ElementAt(i).Name}, {matchingPlayers.ElementAt(i).Race} {matchingPlayers.ElementAt(i).Class}");
                                }

                                if (matchingPlayers.Count > 1)
                                {
                                    _outputManager.AddLogEntry($"Total: {matchingPlayers.Count}");
                                }
                            }
                            break;
                        }
                    }
                }
                else if (input == 5 && playerOrMonsterInput == "2")
                {
                    while (true)
                    {
                        try
                        {
                            _outputManager.AddLogEntry("Type in the attack power of the character(s) you want.");
                            int attack = Convert.ToInt32(_outputManager.GetUserInput("Attack:"));

                            if (attack <= 0)
                            {
                                _outputManager.AddLogEntry("The attack power must be higher than 0.");
                            }

                            foreach (Monster monster in monstersInRoom)
                            {
                                if (monster.Race.Equals("Balrog"))
                                {
                                    monsterStat = 14;
                                }
                                else if (monster.Race.Equals("Fallen"))
                                {
                                    monsterStat = 20;
                                }
                                else if (monster.Race.Equals("Ghoul"))
                                {
                                    monsterStat = 10;
                                }
                                else if (monster.Race.Equals("Goblin"))
                                {
                                    monsterStat = 4;
                                }
                                else if (monster.Race.Equals("Kumiho"))
                                {
                                    monsterStat = 12;
                                }
                                else if (monster.Race.Equals("Ogre"))
                                {
                                    monsterStat = 8;
                                }
                                else if (monster.Race.Equals("Orrok"))
                                {
                                    monsterStat = 9;
                                }
                                else if (monster.Race.Equals("Rakshasa") || monster.Race.Equals("Troll"))
                                {
                                    monsterStat = 7;
                                }
                                else if (monster.Race.Equals("Uruk"))
                                {
                                    monsterStat = 5;
                                }

                                if (attack == monsterStat)
                                {
                                    matchingMonsters.Add(monster);
                                }
                            }

                            if (matchingMonsters.Count == 0)
                            {
                                _outputManager.AddLogEntry($"There is no character with an attack power of {attack} in this room.");
                                break;
                            }
                            else if (matchingMonsters.Count > 0)
                            {
                                for (int i = 0; i < matchingMonsters.Count; ++i)
                                {
                                    _outputManager.AddLogEntry($"{i + 1}. {matchingMonsters.ElementAt(i).Name}, {matchingMonsters.ElementAt(i).Race}");
                                }

                                if (matchingMonsters.Count > 1)
                                {
                                    _outputManager.AddLogEntry($"Total: {matchingMonsters.Count}");
                                }

                                break;
                            }
                        }
                        catch (Exception e)
                        {
                            _outputManager.AddLogEntry(e.Message);
                        }
                    }
                }
                else if (input == 6 && playerOrMonsterInput == "1")
                {
                    while (true)
                    {
                        _outputManager.AddLogEntry("Type in the defense of the character(s) you want.");
                        int defense = Convert.ToInt32(_outputManager.GetUserInput("Defense:"));

                        if (defense < 0)
                        {
                            _outputManager.AddLogEntry("The defense must be a positive number.");
                        }
                        else
                        {
                            foreach (Player player in playersInRoom)
                            {
                                playerAbilities = player.Abilities.ToList();

                                if (player.Equipment.Weapon != null && player.Equipment.Armor != null)
                                {
                                    totalDefense += player.Equipment.Weapon.Defense;
                                    totalDefense += player.Equipment.Armor.Defense;
                                }
                                else if (player.Equipment.Weapon != null && player.Equipment.Armor == null)
                                {
                                    totalDefense += player.Equipment.Weapon.Defense;
                                }
                                else if (player.Equipment.Weapon == null && player.Equipment.Armor != null)
                                {
                                    totalDefense += player.Equipment.Armor.Defense;
                                }

                                if (playerAbilities.Count > 0)
                                {
                                    foreach (Ability ability in playerAbilities)
                                    {
                                        totalDefense += ability.Defense;
                                    }
                                }

                                if (defense == totalDefense)
                                {
                                    matchingPlayers.Add(player);
                                }
                            }

                            if (matchingPlayers.Count == 0)
                            {
                                _outputManager.AddLogEntry($"There is no character with a defense of {defense} in this room.");
                            }
                            else if (matchingPlayers.Count > 0)
                            {
                                for (int i = 0; i < matchingPlayers.Count; ++i)
                                {
                                    _outputManager.AddLogEntry($"{i + 1}. {matchingPlayers.ElementAt(i).Name}, {matchingPlayers.ElementAt(i).Race} {matchingPlayers.ElementAt(i).Class}");
                                }

                                if (matchingPlayers.Count > 1)
                                {
                                    _outputManager.AddLogEntry($"Total: {matchingPlayers.Count}");
                                }
                            }
                            break;
                        }
                    }
                }
                else if (input == 6 && playerOrMonsterInput == "2")
                {
                    while (true)
                    {
                        try
                        {
                            _outputManager.AddLogEntry("Type in the defense of the character(s) you want.");
                            int defense = Convert.ToInt32(_outputManager.GetUserInput("Defense:"));

                            if (defense < 0)
                            {
                                _outputManager.AddLogEntry("The defense must be a positive number.");
                            }

                            foreach (Monster monster in monstersInRoom)
                            {
                                if (monster.Race.Equals("Balrog"))
                                {
                                    monsterStat = 19;
                                }
                                else if (monster.Race.Equals("Fallen"))
                                {
                                    monsterStat = 18;
                                }
                                else if (monster.Race.Equals("Goblin"))
                                {
                                    monsterStat = 5;
                                }
                                else if (monster.Race.Equals("Kumiho"))
                                {
                                    monsterStat = 10;
                                }
                                else if (monster.Race.Equals("Ogre"))
                                {
                                    monsterStat = 11;
                                }
                                else if (monster.Race.Equals("Orrok") || monster.Race.Equals("Troll"))
                                {
                                    monsterStat = 15;
                                }
                                else if (monster.Race.Equals("Orrok"))
                                {
                                    monsterStat = 16;
                                }
                                else if (monster.Race.Equals("Uruk"))
                                {
                                    monsterStat = 7;
                                }

                                if (defense == monsterStat)
                                {
                                    matchingMonsters.Add(monster);
                                }
                            }

                            if (matchingMonsters.Count == 0)
                            {
                                _outputManager.AddLogEntry($"There is no character with a defense of {defense} in this room.");
                                break;
                            }
                            else if (matchingMonsters.Count > 0)
                            {
                                for (int i = 0; i < matchingMonsters.Count; ++i)
                                {
                                    _outputManager.AddLogEntry($"{i + 1}. {matchingMonsters.ElementAt(i).Name}, {matchingMonsters.ElementAt(i).Race}");
                                }

                                if (matchingMonsters.Count > 1)
                                {
                                    _outputManager.AddLogEntry($"Total: {matchingMonsters.Count}");
                                }

                                break;
                            }
                        }
                        catch (Exception e)
                        {
                            _outputManager.AddLogEntry(e.Message);
                        }
                    }
                }
                else if (input == 3)
                {
                    while (true)
                    {
                        _outputManager.AddLogEntry("Type in the name of the class you\'re the character(s) you want belong to.");
                        string characterClass = _outputManager.GetUserInput("Class:").ToString();

                        if (characterClass.Equals("") || characterClass == null)
                        {
                            _outputManager.AddLogEntry("You must input the character's class in order to continue.");
                        }
                        else
                        {
                            matchingPlayers = playersInRoom.Where(c => c.Class.Equals(characterClass)).ToList();

                            if (matchingPlayers.Count == 0)
                            {
                                _outputManager.AddLogEntry($"There is no characters of the class \"{characterClass.ToLower()}\" in this room.");
                            }
                            else if (matchingPlayers.Count > 0)
                            {
                                for (int i = 0; i < matchingPlayers.Count; ++i)
                                {
                                    _outputManager.AddLogEntry($"{i + 1}. {matchingPlayers.ElementAt(i).Name}, {matchingPlayers.ElementAt(i).Race} {matchingPlayers.ElementAt(i).Class}");
                                }

                                if (matchingPlayers.Count > 1)
                                {
                                    _outputManager.AddLogEntry($"Total: {matchingPlayers.Count}");
                                }
                            }
                            break;
                        }
                    }
                }

                while (true)
                {
                    _outputManager.AddLogEntry("Select if you want to exit.");
                    _outputManager.AddLogEntry("1. Yes");
                    _outputManager.AddLogEntry("2. No");
                    var exitMethod = _outputManager.GetUserInput("Selection:");

                    switch (exitMethod)
                    {
                        case "1":
                            break;
                        case "2":
                            break;
                        default:
                            _outputManager.AddLogEntry("Invalid selection. Please choose 1 or 2.");
                            break;
                    }

                    if (exitMethod == "1")
                    {
                        complete = true;
                        break;
                    }
                }

                if (complete)
                {
                    break;
                }
            }
        }

        public void GroupCharactersByRoom()
        {
            List<Player> players = _context.Players.ToList();
            List<Monster> monsters = _context.Monsters.ToList();
            int playerOrMonsterInput = 0;

            while (true)
            {
                try
                {
                    _outputManager.AddLogEntry("Select whether the character(s) is/are playable or (an) enem(y/ies).");
                    _outputManager.AddLogEntry("1. Player Character");
                    _outputManager.AddLogEntry("2. Enemy");
                    playerOrMonsterInput = Convert.ToInt32(_outputManager.GetUserInput("Selection:"));

                    switch (playerOrMonsterInput)
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                        default:
                            _outputManager.AddLogEntry("Invalid input. Please choose 1 or 2.");
                            break;
                    }

                    if (playerOrMonsterInput > 0 && playerOrMonsterInput <= 2)
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    _outputManager.AddLogEntry(e.Message);
                }
            }

            while (true)
            {
                if (playerOrMonsterInput == 1)
                {
                    var playerQuery = players.GroupBy(r => r.Room.Name);

                    foreach (var result in playerQuery)
                    {
                        _outputManager.AddLogEntry(result.Key);

                        var groupedPlayers = players.Where(p => p.Room.Name.Equals(result.Key));
                        foreach (Player groupedPlayer in groupedPlayers)
                        {
                            if (groupedPlayer.Equipment.Weapon != null && groupedPlayer.Equipment.Armor != null)
                            {
                                _outputManager.AddLogEntry($"     {groupedPlayer.Name} - {groupedPlayer.Race} {groupedPlayer.Class}, Weapon: {groupedPlayer.Equipment.Weapon.Name}, Armor {groupedPlayer.Equipment.Armor.Name}");
                            }
                            else if (groupedPlayer.Equipment.Weapon != null && groupedPlayer.Equipment.Armor == null)
                            {
                                _outputManager.AddLogEntry($"     {groupedPlayer.Name} - {groupedPlayer.Race} {groupedPlayer.Class}, Weapon: {groupedPlayer.Equipment.Weapon.Name}");
                            }
                            else if (groupedPlayer.Equipment.Weapon == null && groupedPlayer.Equipment.Armor != null)
                            {
                                _outputManager.AddLogEntry($"     {groupedPlayer.Name} - {groupedPlayer.Race} {groupedPlayer.Class}, Armor {groupedPlayer.Equipment.Armor.Name}");
                            }
                            else
                            {
                                _outputManager.AddLogEntry($"     {groupedPlayer.Name} - {groupedPlayer.Race} {groupedPlayer.Class}");
                            }
                        }
                    }
                }
                else if (playerOrMonsterInput == 2)
                {
                    var monsterQuery = monsters.GroupBy(r => r.Room.Name);

                    foreach (var result in monsterQuery)
                    {
                        _outputManager.AddLogEntry(result.Key);

                        var groupedMonsters = monsters.Where(m => m.Room.Name.Equals(result.Key));
                        foreach (Monster groupedMonster in groupedMonsters)
                        {
                            _outputManager.AddLogEntry($"     {groupedMonster.Name}, {groupedMonster.Race}");
                        }
                    }
                }

                _outputManager.AddLogEntry("\nInput 1 to exit.");
                var input = _outputManager.GetUserInput("Done:");

                switch (input)
                {
                    case "1":
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please input 1.");
                        break;
                }

                if (input == "1")
                {
                    break;
                }
            }
        }
    }
}