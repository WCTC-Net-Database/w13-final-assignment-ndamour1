using Castle.Core.Internal;
using ConsoleRpg.Helpers;
using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Characters.Monsters;
using ConsoleRpgEntities.Models.Equipments;
using ConsoleRpgEntities.Models.Rooms;
using ConsoleRpgEntities.Repository;
using ConsoleRpgEntities.Services;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using System;

namespace ConsoleRpg.Services;

public class GameEngine
{
    private readonly GameContext _context;
    private readonly GameGuide _guide;
    private readonly MenuManager _menuManager;
    private readonly MapManager _mapManager;
    private readonly AssetCreationService _assetCreationService;
    private readonly SearchService _searchService;
    private readonly PlayerService _playerService;
    private readonly MonsterService _monsterService;
    private readonly AbilityService _abilityService;
    private readonly PlayerRepository _playerRepository;
    private readonly MonsterRepository _monsterRepository;
    private readonly AbilityRepository _abilityRepository;
    private readonly ItemRepository _itemRepository;
    private readonly RoomRepository _roomRepository;
    private readonly OutputManager _outputManager;
    private Table _logTable;
    private Panel _mapPanel;

    private Player _player;
    private List<Monster> _monsters;

    public GameEngine(GameContext context, GameGuide guide, MenuManager menuManager, MapManager mapManager, AssetCreationService assetCreationService, SearchService searchService, PlayerService playerService, MonsterService monsterService, AbilityService abilityService, PlayerRepository playerRepository, MonsterRepository monsterRepository, AbilityRepository abilityRepository, ItemRepository itemRepository, RoomRepository roomRepository, OutputManager outputManager)
    {
        _menuManager = menuManager;
        _mapManager = mapManager;
        _assetCreationService = assetCreationService;
        _searchService = searchService;
        _playerService = playerService;
        _monsterService = monsterService;
        _abilityService = abilityService;
        _playerRepository = playerRepository;
        _monsterRepository = monsterRepository;
        _abilityRepository = abilityRepository;
        _itemRepository = itemRepository;
        _roomRepository = roomRepository;
        _outputManager = outputManager;
        _context = context;
        _guide = guide;
    }

    public void Run()
    {
        if (_menuManager.ShowMainMenu())
        {
            SetupGame();
        }
    }

    private void GameLoop(string name)
    {
        int restorePlayerHealth = _player.Health;
        RestoreMonsterHealth();

        while (true)
        {
            if (_player.Health > 0)
            {
                // Check if there are characters in the current room to attack
                if (_player.Room.Monsters.Count > 0 && !name.Equals("Nathaniel D\'Amour"))
                {
                    _outputManager.AddLogEntry("1. Move North");
                    _outputManager.AddLogEntry("2. Move South");
                    _outputManager.AddLogEntry("3. Move East");
                    _outputManager.AddLogEntry("4. Move West");
                    _outputManager.AddLogEntry("5. Change Equipment");
                    _outputManager.AddLogEntry("6. Find Item");
                    _outputManager.AddLogEntry("7. Consult Guide");
                    _outputManager.AddLogEntry("8. Attack");
                    _outputManager.AddLogEntry("11. Quit");
                }
                else if (_player.Room.Monsters.Count <= 0 && !name.Equals("Nathaniel D\'Amour"))
                {
                    _outputManager.AddLogEntry("1. Move North");
                    _outputManager.AddLogEntry("2. Move South");
                    _outputManager.AddLogEntry("3. Move East");
                    _outputManager.AddLogEntry("4. Move West");
                    _outputManager.AddLogEntry("5. Change Equipment");
                    _outputManager.AddLogEntry("6. Find Item");
                    _outputManager.AddLogEntry("7. Consult Guide");
                    _outputManager.AddLogEntry("11. Quit");
                }
                else if (_player.Room.Monsters.Count <= 0 && name.Equals("Nathaniel D\'Amour"))
                {
                    _outputManager.AddLogEntry("1. Move North");
                    _outputManager.AddLogEntry("2. Move South");
                    _outputManager.AddLogEntry("3. Move East");
                    _outputManager.AddLogEntry("4. Move West");
                    _outputManager.AddLogEntry("5. Change Equipment");
                    _outputManager.AddLogEntry("6. Find Item");
                    _outputManager.AddLogEntry("7. Consult Guide");
                    _outputManager.AddLogEntry("9. Find Characters in Room");
                    _outputManager.AddLogEntry("10. Group Search for Characters by Room");
                    _outputManager.AddLogEntry("11. Quit");
                }
                else if (_player.Room.Monsters.Count > 0 && name.Equals("Nathaniel D\'Amour"))
                {
                    _outputManager.AddLogEntry("1. Move North");
                    _outputManager.AddLogEntry("2. Move South");
                    _outputManager.AddLogEntry("3. Move East");
                    _outputManager.AddLogEntry("4. Move West");
                    _outputManager.AddLogEntry("5. Change Equipment");
                    _outputManager.AddLogEntry("6. Find Item");
                    _outputManager.AddLogEntry("7. Consult Guide");
                    _outputManager.AddLogEntry("8. Attack");
                    _outputManager.AddLogEntry("9. Find Characters in Room");
                    _outputManager.AddLogEntry("10. Group Search for Characters by Room");
                    _outputManager.AddLogEntry("11. Quit");
                }

                var input = _outputManager.GetUserInput("Choose an action:");
                Room currentRoom = _context.Rooms.FirstOrDefault(i => i.Id == _player.RoomId);
                int currentRoomId = currentRoom.Id;
                string? direction = null;

                switch (input)
                {
                    case "1":
                        direction = "north";
                        break;
                    case "2":
                        direction = "south";
                        break;
                    case "3":
                        direction = "east";
                        break;
                    case "4":
                        direction = "west";
                        break;
                    case "5":
                        EquipItem(_player);
                        _playerRepository.UpdatePlayer(_player);
                        break;
                    case "6":
                        _searchService.FindItem();
                        break;
                    case "7":
                        _guide.ChapterIndex();
                        break;
                    case "8":
                        if (_player.Room.Monsters.Count > 0)
                        {
                            AttackCharacter(restorePlayerHealth);
                        }
                        else
                        {
                            _outputManager.AddLogEntry("No enemies to attack.");
                        }
                        break;
                    case "9":
                        _searchService.FindCharactersInRoom(_player.Room);
                        break;
                    case "10":
                        _searchService.GroupCharactersByRoom();
                        break;
                    case "11":
                        _outputManager.AddLogEntry("Exiting game...");
                        _player.Health = restorePlayerHealth;
                        _playerRepository.UpdatePlayer(_player);
                        RestoreMonsterHealth();
                        Environment.Exit(0);
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please choose between 0 and 12.");
                        break;
                }

                try
                {
                    // Update map manager with the current room after movement
                    if (!string.IsNullOrEmpty(direction))
                    {
                        if (direction == "north")
                        {
                            currentRoomId = Convert.ToInt32(currentRoom.NorthId);
                        }
                        else if (direction == "south")
                        {
                            currentRoomId = Convert.ToInt32(currentRoom.SouthId);
                        }
                        else if (direction == "east")
                        {
                            currentRoomId = Convert.ToInt32(currentRoom.EastId);
                        }
                        else if (direction == "west")
                        {
                            currentRoomId = Convert.ToInt32(currentRoom.WestId);
                        }

                        _playerService.Move(_player, direction);
                        _mapManager.UpdateCurrentRoom(currentRoomId);
                        _mapManager.DisplayMap();
                    }
                }
                catch (Exception e)
                {
                    _outputManager.AddLogEntry($"There is no room in that direction.");
                }
            }
            else
            {
                _outputManager.AddLogEntry($"{_player.Name} has fallen in battle.");
                _outputManager.AddLogEntry("Would you like to play again?");
                _outputManager.AddLogEntry("1. Yes");
                _outputManager.AddLogEntry("2. No");

                var input = _outputManager.GetUserInput("Selection:");

                switch (input)
                {
                    case "1":
                        _player.Health = restorePlayerHealth;
                        _playerRepository.UpdatePlayer(_player);
                        RestoreMonsterHealth();
                        break;
                    case "2":
                        _outputManager.AddLogEntry("Exiting game...");
                        _player.Health = restorePlayerHealth;
                        _playerRepository.UpdatePlayer(_player);
                        RestoreMonsterHealth();
                        Environment.Exit(0);
                        break;
                    default:
                        _outputManager.AddLogEntry("Invalid selection. Please choose 1 or 2.");
                        break;
                }
            }
        }
    }

    private void AttackCharacter(int restorePlayerHealth)
    {
        Random rand = new Random();
        List<Player> players = _context.Players.ToList();
        List<Ability> abilities = _player.Abilities.ToList();
        List<Ability> sameNameAbilities = new List<Ability>();
        List<Item> items = _context.Items.ToList();
        List<Item> untakenItems = _context.Items.ToList();
        List<Item> takenItems = new List<Item>();
        Ability chosenAbility = new Ability();
        Item item = new Item();
        int chosenEnemy = 0;
        var target = _player.Room.Monsters.ElementAt(0);

        if (_player.Room.Monsters.Count > 1)
        {
            for (int i = 0; i < _player.Room.Monsters.Count; ++i)
            {
                _outputManager.AddLogEntry($"{i + 1}. {_player.Room.Monsters.ElementAt(i).Name}, {_player.Room.Monsters.ElementAt(i).Race}");
            }

            while (true)
            {
                try
                {
                    _outputManager.AddLogEntry("Which enemy would you like to attack?");
                    chosenEnemy = Convert.ToInt32(_outputManager.GetUserInput("Selection:"));

                    if (chosenEnemy > 0 && chosenEnemy <= _player.Room.Monsters.Count)
                    {
                        target = _player.Room.Monsters.ElementAt(chosenEnemy - 1);
                        break;
                    }
                    else
                    {
                        _outputManager.AddLogEntry($"Invalid input. Please choose between 0 and {_player.Room.Monsters.Count}");
                    }
                }
                catch (Exception e)
                {
                    _outputManager.AddLogEntry(e.Message);
                }
            }

            if (target is ITargetable targetableEnemy)
            {
                _playerService.Attack(_player, targetableEnemy);

                if (targetableEnemy.Health > 0)
                {
                    foreach (Ability playerAbility in abilities)
                    {
                        if (playerAbility.Name.Equals("Heal"))
                        {
                            _outputManager.AddLogEntry($"{playerAbility.Description}, Type: {playerAbility.Name}, HP Recovery: {playerAbility.Damage}, Defense: {playerAbility.Defense}");
                        }
                        else
                        {
                            _outputManager.AddLogEntry($"{playerAbility.Description}, Type: {playerAbility.Name}, Damage: {playerAbility.Damage}, Defense: {playerAbility.Defense}");
                        }
                    }
                    
                    while (true)
                    {
                        try
                        {
                            _outputManager.AddLogEntry($"Describe the ability you want to use.");
                            string description = _outputManager.GetUserInput("Description:");
                            sameNameAbilities = abilities.Where(n => n.Description.Equals(description)).ToList();

                            if (description.IsNullOrEmpty())
                            {
                                _outputManager.AddLogEntry("You must describe the ability you want.");
                            }
                            else
                            {
                                sameNameAbilities = abilities.Where(n => n.Description.Equals(description)).ToList();
                                if (sameNameAbilities.Count == 1)
                                {
                                    chosenAbility = sameNameAbilities.ElementAt(0);
                                    _playerService.UseAbility(_player, chosenAbility, targetableEnemy);
                                    break;
                                }
                                else if (sameNameAbilities.Count > 1)
                                {
                                    for (int i = 0; i < sameNameAbilities.Count; ++i)
                                    {
                                        if (sameNameAbilities.ElementAt(i).Name.Equals("Heal"))
                                        {
                                            _outputManager.AddLogEntry($"{i + 1}. {sameNameAbilities.ElementAt(i).Description}, Type: {sameNameAbilities.ElementAt(i).Name}, HP Recovery: {sameNameAbilities.ElementAt(i).Damage}, Defense: {sameNameAbilities.ElementAt(i).Defense}");
                                        }
                                        else
                                        {
                                            _outputManager.AddLogEntry($"{i + 1}. {sameNameAbilities.ElementAt(i).Description}, Type: {sameNameAbilities.ElementAt(i).Name}, Damage: {sameNameAbilities.ElementAt(i).Damage}, Defense: {sameNameAbilities.ElementAt(i).Defense}");
                                        }
                                    }

                                    while (true)
                                    {
                                        try
                                        {
                                            _outputManager.AddLogEntry($"There's more than one ability with the description \"{description}\".\nChoose which one you want.");
                                            int iChooseYou = Convert.ToInt32(_outputManager.GetUserInput("Selection:"));

                                            if (iChooseYou > 0 && iChooseYou <= sameNameAbilities.Count)
                                            {
                                                chosenAbility = sameNameAbilities.ElementAt(iChooseYou - 1);
                                                _playerService.UseAbility(_player, chosenAbility, targetableEnemy);
                                            }
                                            else
                                            {
                                                _outputManager.AddLogEntry($"Invalid selection. Please choose between 0 and {sameNameAbilities.Count + 1}.");
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            _outputManager.AddLogEntry(e.Message);
                                        }
                                    }
                                }
                                else if (sameNameAbilities.Count == 0)
                                {
                                    _outputManager.AddLogEntry($"{_player.Name} doesn\'t have an ability described as {description}.");
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            _outputManager.AddLogEntry(e.Message);
                        }
                    }
                }
            }

            if (_player.Room.Monsters.ElementAt(chosenEnemy - 1).Health > 0)
            {
                _monsterService.Attack(target, _player);
                _abilityService.Deactivate(chosenAbility);
            }
            else
            {
                _abilityService.Deactivate(chosenAbility);
                target.Room = null;
                _player.Room.Monsters.Remove(target);
                _monsterRepository.UpdateMonster(target);
                _player.Health = restorePlayerHealth;
                _player.Experience += 10;
                _player.Health = CalculateHealth(_player);
                restorePlayerHealth = _player.Health;

                foreach (Item takenItem in items)
                {
                    foreach (Player player in players)
                    {
                        if (player.Equipment.Weapon == takenItem || player.Equipment.Armor == takenItem)
                        {
                            untakenItems.Remove(takenItem);
                        }
                    }
                }

                if (untakenItems.Count > 0)
                {
                    int itemId = rand.Next(0, untakenItems.Count);
                    item = untakenItems.ElementAt(itemId);

                    _outputManager.AddLogEntry($"{_player.Name} looted a {item.Name} from {target.Name}.");
                    _player.Inventory.Items.Add(item);
                    _playerRepository.UpdatePlayer(_player);
                }
            }
        }
        else if (_player.Room.Monsters.Count == 1)
        {
            target = _player.Room.Monsters.ElementAt(0);

            if (target is ITargetable targetableEnemy)
            {
                _playerService.Attack(_player, targetableEnemy);

                if (targetableEnemy.Health > 0)
                {
                    foreach (Ability playerAbility in abilities)
                    {
                        if (playerAbility.Name.Equals("Heal"))
                        {
                            _outputManager.AddLogEntry($"{playerAbility.Description}, Type: {playerAbility.Name}, HP Recovery: {playerAbility.Damage}, Defense: {playerAbility.Defense}");
                        }
                        else
                        {
                            _outputManager.AddLogEntry($"{playerAbility.Description}, Type: {playerAbility.Name}, Damage: {playerAbility.Damage}, Defense: {playerAbility.Defense}");
                        }
                    }

                    while (true)
                    {
                        try
                        {
                            _outputManager.AddLogEntry($"Describe the ability you want to use.\nRemember to get the description right.");
                            string description = _outputManager.GetUserInput("Description:");
                            sameNameAbilities = abilities.Where(n => n.Description.Equals(description)).ToList();

                            if (description.IsNullOrEmpty())
                            {
                                _outputManager.AddLogEntry("You must describe the ability you want.");
                            }
                            else
                            {
                                sameNameAbilities = abilities.Where(n => n.Description.Equals(description)).ToList();
                                if (sameNameAbilities.Count == 1)
                                {
                                    chosenAbility = sameNameAbilities.ElementAt(0);
                                    _playerService.UseAbility(_player, chosenAbility, targetableEnemy);
                                    break;
                                }
                                else if (sameNameAbilities.Count > 1)
                                {
                                    for (int i = 0; i < sameNameAbilities.Count; ++i)
                                    {
                                        if (sameNameAbilities.ElementAt(i).Name.Equals("Heal"))
                                        {
                                            _outputManager.AddLogEntry($"{i + 1}. {sameNameAbilities.ElementAt(i).Description}, Type: {sameNameAbilities.ElementAt(i).Name}, HP Recovery: {sameNameAbilities.ElementAt(i).Damage}, Defense: {sameNameAbilities.ElementAt(i).Defense}");
                                        }
                                        else
                                        {
                                            _outputManager.AddLogEntry($"{i + 1}. {sameNameAbilities.ElementAt(i).Description}, Type: {sameNameAbilities.ElementAt(i).Name}, Damage: {sameNameAbilities.ElementAt(i).Damage}, Defense: {sameNameAbilities.ElementAt(i).Defense}");
                                        }
                                    }

                                    while (true)
                                    {
                                        try
                                        {
                                            _outputManager.AddLogEntry($"There's more than one ability with the description \"{description}\".\nChoose which one you want.");
                                            int iChooseYou = Convert.ToInt32(_outputManager.GetUserInput("Selection:"));

                                            if (iChooseYou > 0 && iChooseYou <= sameNameAbilities.Count)
                                            {
                                                chosenAbility = sameNameAbilities.ElementAt(iChooseYou - 1);
                                                _playerService.UseAbility(_player, chosenAbility, targetableEnemy);
                                            }
                                            else
                                            {
                                                _outputManager.AddLogEntry($"Invalid selection. Please choose between 0 and {sameNameAbilities.Count + 1}.");
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            _outputManager.AddLogEntry(e.Message);
                                        }
                                    }
                                }
                                else if (sameNameAbilities.Count == 0)
                                {
                                    _outputManager.AddLogEntry($"{_player.Name} doesn\'t have an ability described as {description}.");
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            _outputManager.AddLogEntry(e.Message);
                        }
                    }
                }

                if (_player.Room.Monsters.ElementAt(0).Health > 0)
                {
                    _monsterService.Attack(target, _player);
                    _abilityService.Deactivate(chosenAbility);
                }
                else
                {
                    _abilityService.Deactivate(chosenAbility);
                    target.Room = null;
                    _player.Room.Monsters.Remove(target);
                    _abilityService.Deactivate(chosenAbility);
                    _player.Health = restorePlayerHealth;
                    _player.Experience += 10;
                    _player.Health = CalculateHealth(_player);
                    restorePlayerHealth = _player.Health;

                    foreach (Item takenItem in items)
                    {
                        foreach (Player player in players)
                        {
                            if (player.Equipment.Weapon == takenItem || player.Equipment.Armor == takenItem)
                            {
                                untakenItems.Remove(takenItem);
                            }
                        }
                    }

                    if (untakenItems.Count > 0)
                    {
                        int itemId = rand.Next(0, untakenItems.Count);
                        item = untakenItems.ElementAt(itemId);
                        _itemRepository.UpdateItem(item);

                        _outputManager.AddLogEntry($"{_player.Name} looted a {item.Name} from {target.Name}.");
                        _player.Inventory.Items.Add(item);
                        _playerRepository.UpdatePlayer(_player);
                    }
                }
            }
        }
    }

    private void SetupGame()
    {
        Random rand = new Random();
        List<Player> characters = _context.Players.ToList();
        _player = _context.Players.FirstOrDefault();

        while (true)
        {
            _outputManager.AddLogEntry("Would you like to select, create, or edit a character or create a room? If not, input 5 if you want to consult your guide, or 6 to play.");
            _outputManager.AddLogEntry("1. Select Character");
            _outputManager.AddLogEntry("2. Create Character");
            _outputManager.AddLogEntry("3. Edit Character");
            _outputManager.AddLogEntry("4. Create Room");
            _outputManager.AddLogEntry("5. Consult Guide");
            _outputManager.AddLogEntry("6. Play");
            var createCharacterInput = _outputManager.GetUserInput("Selection:");

            switch (createCharacterInput)
            {
                case "1":
                    _player = SelectCharacter();
                    break;
                case "2":
                    int modifier = rand.Next(-5, 11);
                    _player = _assetCreationService.CreateCharacter(modifier);
                    Ability ability = _player.Abilities.ElementAt(0);
                    ability.Players.Add(_player);
                    _abilityRepository.UpdateAbility(ability);
                    if (_player.Equipment.Weapon != null && _player.Equipment.Armor != null)
                    {
                        _player.Inventory = new Inventory
                        {
                            Items = new List<Item>()
                        {
                            _player.Equipment.Weapon,
                            _player.Equipment.Armor
                        },
                            Player = _player
                        };
                        _itemRepository.UpdateItem(_player.Equipment.Weapon);
                        _itemRepository.UpdateItem(_player.Equipment.Armor);
                    }
                    else if (_player.Equipment.Weapon != null && _player.Equipment.Armor == null)
                    {
                        _player.Inventory = new Inventory
                        {
                            Items = new List<Item>()
                            {
                                _player.Equipment.Weapon
                            },
                            Player = _player
                        };
                        _itemRepository.UpdateItem(_player.Equipment.Weapon);
                    }
                    else if (_player.Equipment.Weapon == null && _player.Equipment.Armor != null)
                    {
                        _player.Inventory = new Inventory
                        {
                            Items = new List<Item>()
                            {
                                _player.Equipment.Armor
                            },
                            Player = _player
                        };
                        _itemRepository.UpdateItem(_player.Equipment.Armor);
                    }
                    else
                    {
                        _player.Inventory = new Inventory
                        {
                            Items = new List<Item>(),
                            Player = _player
                        };
                    }
                    _playerRepository.UpdatePlayer(_player);
                    _player.Room.PlayerId = _player.Id;
                    _roomRepository.UpdateRoom(_player.Room);
                    _assetCreationService.AbilityCreationLoop(_player);
                    break;
                case "3":
                    EditCharacter();
                    break;
                case "4":
                    _assetCreationService.CreateRoom();
                    break;
                case "5":
                    _guide.ChapterIndex();
                    break;
                case "6":
                    break;
                default:
                    _outputManager.AddLogEntry("Invalid selection. Please choose one of the numbers listed.");
                    break;
            }

            if (createCharacterInput == "6" && characters.Count > 0)
            {
                break;
            }
            else if (createCharacterInput == "6" && characters.Count == 0)
            {
                _outputManager.AddLogEntry("There are currently no available characters to play as.\nInput 2 to create a character.");
            }
        }

        string name = SignIn();
        _outputManager.AddLogEntry($"{_player.Name} has entered the game.");

        // Load monsters into random rooms 
        LoadMonsters();

        // Load map
        _mapManager.LoadInitialRoom((int)_player.RoomId);
        _mapManager.DisplayMap();

        // Pause before starting the game loop
        Thread.Sleep(500);
        GameLoop(name);
    }

    private string SignIn()
    {
        int i = 0;
        string name = null;

        while (true)
        {
            try
            {
                _outputManager.AddLogEntry("Enter your name.");
                name = _outputManager.GetUserInput("Name:");
                bool result = int.TryParse(name, out i);

                if (!result && !(name.IsNullOrEmpty()))
                {
                    break;
                }
                else if (!result && (name.IsNullOrEmpty()))
                {
                    _outputManager.AddLogEntry("You enter your name in order to continue.");
                }
                else
                {
                    _outputManager.AddLogEntry("Invalid input. Try again.");
                }
            }
            catch (Exception e)
            {
                _outputManager.AddLogEntry(e.Message);
            }
        }

        return name;
    }

    private void RestoreMonsterHealth()
    {
        List<Monster> monsters = _context.Monsters.ToList();
        foreach (Monster monster in monsters)
        {
            if (monster.Name.Equals("Bob Goblin") || monster.Name.Equals("Zurk") || monster.Name.Equals("Pushkrimp"))
            {
                monster.Health = 20;
            }
            else if (monster.Name.Equals("Pounk"))
            {
                monster.Health = 10;
            }
            else if (monster.Name.Equals("Bis"))
            {
                monster.Health = 14;
            }
            else if (monster.Name.Equals("Bis"))
            {
                monster.Health = 13;
            }
            else if (monster.Name.Equals("Marka"))
            {
                monster.Health = 13;
            }
            else if (monster.Name.Equals("Floffo"))
            {
                monster.Health = 12;
            }
            else if (monster.Name.Equals("Moorch"))
            {
                monster.Health = 17;
            }
            else if (monster.Name.Equals("Krovno"))
            {
                monster.Health = 11;
            }
            else if (monster.Name.Equals("Puglak"))
            {
                monster.Health = 16;
            }
            else if (monster.Name.Equals("Facetrashah") || monster.Name.Equals("Bomikor"))
            {
                monster.Health = 70;
            }
            else if (monster.Name.Equals("Edkrakah"))
            {
                monster.Health = 77;
            }
            else if (monster.Name.Equals("Doomchoppah"))
            {
                monster.Health = 74;
            }
            else if (monster.Name.Equals("Balgrug"))
            {
                monster.Health = 78;
            }
            else if (monster.Name.Equals("Urkrakh"))
            {
                monster.Health = 80;
            }
            else if (monster.Name.Equals("Algrozan"))
            {
                monster.Health = 200;
            }
            else if (monster.Name.Equals("Nozcar"))
            {
                monster.Health = 175;
            }
            else if (monster.Race.Equals("Ghoul"))
            {
                monster.Health = 100;
            }
            else if (monster.Name.Equals("Bolg"))
            {
                monster.Health = 25;
            }
            else if (monster.Name.Equals("Ashgarn"))
            {
                monster.Health = 21;
            }
            else if (monster.Name.Equals("Tuhorn"))
            {
                monster.Health = 24;
            }
            else if (monster.Name.Equals("Grisha"))
            {
                monster.Health = 28;
            }
            else if (monster.Name.Equals("Shag"))
            {
                monster.Health = 26;
            }
            else if (monster.Name.Equals("Snafu"))
            {
                monster.Health = 27;
            }
            else if (monster.Name.Equals("Ur-Edin"))
            {
                monster.Health = 64;
            }
            else if (monster.Name.Equals("Az-Bror"))
            {
                monster.Health = 60;
            }
            else if (monster.Name.Equals("Ar-Lisu"))
            {
                monster.Health = 66;
            }
            else if (monster.Name.Equals("Az-Laar"))
            {
                monster.Health = 68;
            }
            else if (monster.Name.Equals("Zoruk"))
            {
                monster.Health = 65;
            }
            else if (monster.Name.Equals("Nuderg"))
            {
                monster.Health = 63;
            }
            else if (monster.Name.Equals("Ozroth"))
            {
                monster.Health = 80;
            }
            else if (monster.Name.Equals("Ju Kwang"))
            {
                monster.Health = 90;
            }
            else if (monster.Name.Equals("Amemaru"))
            {
                monster.Health = 35;
            }
            _monsterRepository.UpdateMonster(monster);
        }
    }

    private Player SelectCharacter()
    {
        List<Player> characters = _context.Players.ToList();
        Player chosen = new Player();
        int counterThree = 0;
        int search = 0;

        if (characters.Count > 1)
        {
            while (true)
            {
                try
                {
                    _outputManager.AddLogEntry("Select a search method:");
                    _outputManager.AddLogEntry("1. Name");
                    _outputManager.AddLogEntry("2. Race");
                    _outputManager.AddLogEntry("3. Class");
                    _outputManager.AddLogEntry("4. Experience");
                    _outputManager.AddLogEntry("5. Health");
                    search = Convert.ToInt32(_outputManager.GetUserInput("Selection:"));

                    switch (search)
                    {
                        case 1:
                            characters = characters.OrderBy(n => n.Name).ToList();
                            break;
                        case 2:
                            var raceQuery = characters.OrderBy(r => r.Race).GroupBy(r => r.Race);
                            foreach (var result in raceQuery)
                            {
                                int counterOne = 0;
                                _outputManager.AddLogEntry($"\n{result.Key}");
                                var groupByRace = characters.Where(r => r.Race.Equals(result.Key)).OrderBy(n => n.Name);
                                foreach (var characterByRace in groupByRace)
                                {
                                    counterOne++;
                                    if (characterByRace.Equipment == null)
                                    {
                                        _outputManager.AddLogEntry($"{characterByRace.Name} - {characterByRace.Race} {characterByRace.Class}, Health: {characterByRace.Health}, Experience: {characterByRace.Experience}");
                                    }
                                    else if (characterByRace.Equipment.WeaponId != null && characterByRace.Equipment.ArmorId == null)
                                    {
                                        _outputManager.AddLogEntry($"{characterByRace.Name} - {characterByRace.Race} {characterByRace.Class}, Health: {characterByRace.Health}, Experience: {characterByRace.Experience}, Weapon: {characterByRace.Equipment.Weapon.Name}");
                                    }
                                    else if (characterByRace.Equipment.WeaponId == null && characterByRace.Equipment.ArmorId != null)
                                    {
                                        _outputManager.AddLogEntry($"{characterByRace.Name} - {characterByRace.Race} {characterByRace.Class}, Health: {characterByRace.Health}, Experience: {characterByRace.Experience}, Armor: {characterByRace.Equipment.Armor.Name}");
                                    }
                                    else
                                    {
                                        _outputManager.AddLogEntry($"{characterByRace.Name} - {characterByRace.Race} {characterByRace.Class}, Health: {characterByRace.Health}, Experience: {characterByRace.Experience}, Weapon: {characterByRace.Equipment.Weapon.Name}, Armor: {characterByRace.Equipment.Armor.Name}");
                                    }
                                }
                                _outputManager.AddLogEntry($"Total: {counterOne}");
                            }
                            break;
                        case 3:
                            var classQuery = characters.OrderBy(c => c.Class).GroupBy(c => c.Class);
                            foreach (var result in classQuery)
                            {
                                int counterTwo = 0;
                                _outputManager.AddLogEntry($"\n{result.Key}");
                                var groupByRace = characters.Where(c => c.Class.Equals(result.Key)).OrderBy(n => n.Name);
                                foreach (var characterByClass in groupByRace)
                                {
                                    counterTwo++;
                                    if (characterByClass.Equipment == null)
                                    {
                                        _outputManager.AddLogEntry($"{characterByClass.Name} - {characterByClass.Race} {characterByClass.Class}, Health: {characterByClass.Health}, Experience: {characterByClass.Experience}");
                                    }
                                    else if (characterByClass.Equipment.WeaponId != null && characterByClass.Equipment.ArmorId == null)
                                    {
                                        _outputManager.AddLogEntry($"{characterByClass.Name} - {characterByClass.Race} {characterByClass.Class}, Health: {characterByClass.Health}, Experience: {characterByClass.Experience}\nWeapon: {characterByClass.Equipment.Weapon.Name}");
                                    }
                                    else if (characterByClass.Equipment.WeaponId == null && characterByClass.Equipment.ArmorId != null)
                                    {
                                        _outputManager.AddLogEntry($"{characterByClass.Name} - {characterByClass.Race} {characterByClass.Class}, Health: {characterByClass.Health}, Experience: {characterByClass.Experience}, Armor: {characterByClass.Equipment.Armor.Name}");
                                    }
                                    else
                                    {
                                        _outputManager.AddLogEntry($"{characterByClass.Name} - {characterByClass.Race} {characterByClass.Class}, Health: {characterByClass.Health}, Experience: {characterByClass.Experience}, Weapon: {characterByClass.Equipment.Weapon.Name}, Armor: {characterByClass.Equipment.Armor.Name}");
                                    }
                                }
                                _outputManager.AddLogEntry($"Total: {counterTwo}");
                            }
                            break;
                        case 4:
                            characters = _context.Players.OrderByDescending(e => e.Experience).OrderBy(n => n.Name).ToList();
                            break;
                        case 5:
                            characters = _context.Players.OrderByDescending(h => h.Health).OrderBy(n => n.Name).ToList();
                            break;
                        default:
                            _outputManager.AddLogEntry("Invalid selection. Please choose between 0 and 6.");
                            break;
                    }

                    if (search > 0 && search < 6)
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    _outputManager.AddLogEntry(e.Message);
                }
            }

            if (search == 1 || search == 4 || search == 5)
            {
                for (int i = 0; i < characters.Count; i++)
                {
                    ++counterThree;
                    if (characters.ElementAt(i).Equipment == null)
                    {
                        _outputManager.AddLogEntry($"{characters.ElementAt(i).Name} - {characters.ElementAt(i).Race} {characters.ElementAt(i).Class}, Health: {characters.ElementAt(i).Health}, Experience: {characters.ElementAt(i).Experience}");
                    }
                    else if (characters.ElementAt(i).Equipment.WeaponId != null && characters.ElementAt(i).Equipment.ArmorId == null)
                    {
                        _outputManager.AddLogEntry($"{characters.ElementAt(i).Name} - {characters.ElementAt(i).Race} {characters.ElementAt(i).Class}, Health: {characters.ElementAt(i).Health}, Experience: {characters.ElementAt(i).Experience}, Weapon: {characters.ElementAt(i).Equipment.Weapon.Name}");
                    }
                    else if (characters.ElementAt(i).Equipment.WeaponId == null && characters.ElementAt(i).Equipment.ArmorId != null)
                    {
                        _outputManager.AddLogEntry($"{characters.ElementAt(i).Name} - {characters.ElementAt(i).Race} {characters.ElementAt(i).Class}, Health: {characters.ElementAt(i).Health}, Experience: {characters.ElementAt(i).Experience}, Armor: {characters.ElementAt(i).Equipment.Armor.Name}");
                    }
                    else
                    {
                        _outputManager.AddLogEntry($"{characters.ElementAt(i).Name} - {characters.ElementAt(i).Race} {characters.ElementAt(i).Class}, Health: {characters.ElementAt(i).Health}, Experience: {characters.ElementAt(i).Experience}, Weapon: {characters.ElementAt(i).Equipment.Weapon.Name}, Armor: {characters.ElementAt(i).Equipment.Armor.Name}");
                    }
                }

                if (characters.Count > 1)
                {
                    _outputManager.AddLogEntry($"Total: {counterThree}");
                }
            }

            // Input
            while (true)
            {
                try
                {
                    _outputManager.AddLogEntry("\nType the name of the character you want.");
                    string chosenName = _outputManager.GetUserInput("Name:");
                    int nameCounter = 0;
                    List<Player> sameNameCharacters = characters.Where(n => n.Name == chosenName).ToList();

                    if (sameNameCharacters.Count == 1)
                    {
                        _outputManager.AddLogEntry($"You have chosen {chosenName}.");
                        chosen = _playerRepository.GetPlayerByName(chosenName);

                        // Result confirmation
                        while (true)
                        {
                            _outputManager.AddLogEntry("Type 1 to continue.");
                            var continueFromCharacterOne = _outputManager.GetUserInput("Continue:");

                            if (continueFromCharacterOne == "1")
                            {
                                break;
                            }
                            else
                            {
                                _outputManager.AddLogEntry("Invalid input.");
                            }
                        }
                    }
                    else if (sameNameCharacters.Count > 1)
                    {
                        for (int i = 0; i < sameNameCharacters.Count; i++)
                        {
                            if (sameNameCharacters.ElementAt(i).Equipment == null)
                            {
                                _outputManager.AddLogEntry($"{sameNameCharacters.ElementAt(i).Name} - {sameNameCharacters.ElementAt(i).Race} {sameNameCharacters.ElementAt(i).Class}, Health: {sameNameCharacters.ElementAt(i).Health}, Experience: {sameNameCharacters.ElementAt(i).Experience}");
                            }
                            else if (sameNameCharacters.ElementAt(i).Equipment.WeaponId != null && sameNameCharacters.ElementAt(i).Equipment.ArmorId == null)
                            {
                                _outputManager.AddLogEntry($"{sameNameCharacters.ElementAt(i).Name} - {sameNameCharacters.ElementAt(i).Race} {sameNameCharacters.ElementAt(i).Class}, Health: {sameNameCharacters.ElementAt(i).Health}, Experience: {sameNameCharacters.ElementAt(i).Experience}, Weapon: {sameNameCharacters.ElementAt(i).Equipment.Weapon.Name}");
                            }
                            else if (sameNameCharacters.ElementAt(i).Equipment.WeaponId == null && sameNameCharacters.ElementAt(i).Equipment.ArmorId != null)
                            {
                                _outputManager.AddLogEntry($"{sameNameCharacters.ElementAt(i).Name} - {sameNameCharacters.ElementAt(i).Race} {sameNameCharacters.ElementAt(i).Class}, Health: {sameNameCharacters.ElementAt(i).Health}, Experience: {sameNameCharacters.ElementAt(i).Experience}, Armor: {sameNameCharacters.ElementAt(i).Equipment.Armor.Name}");
                            }
                            else
                            {
                                _outputManager.AddLogEntry($"{sameNameCharacters.ElementAt(i).Name} - {sameNameCharacters.ElementAt(i).Race} {sameNameCharacters.ElementAt(i).Class}, Health: {sameNameCharacters.ElementAt(i).Health}, Experience: {sameNameCharacters.ElementAt(i).Experience}, Weapon: {sameNameCharacters.ElementAt(i).Equipment.Weapon.Name}, Armor: {sameNameCharacters.ElementAt(i).Equipment.Armor.Name}");
                            }
                        }

                        while (true)
                        {
                            try
                            {
                                _outputManager.AddLogEntry($"There are {nameCounter} characters with the name \"{chosenName}\".\nSelect which one of them you\'d like to play as.");
                                int iChooseYou = Convert.ToInt32(_outputManager.GetUserInput("Selection:"));

                                if (iChooseYou > 0 && iChooseYou <= sameNameCharacters.Count)
                                {
                                    _outputManager.AddLogEntry($"You have chosen {chosenName}.");
                                    chosen = sameNameCharacters.ElementAt(iChooseYou - 1);
                                    break;
                                }
                                else
                                {
                                    _outputManager.AddLogEntry($"Invalid selection. Please choose between 0 and {sameNameCharacters.Count + 1}.");
                                }
                            }
                            catch (Exception e)
                            {
                                _outputManager.AddLogEntry(e.Message);
                            }
                        }

                        // Result confirmation
                        while (true)
                        {
                            _outputManager.AddLogEntry("Type 1 to continue.");
                            var continueFromCharacterTwo = _outputManager.GetUserInput("Continue:");

                            if (continueFromCharacterTwo == "1")
                            {
                                break;
                            }
                            else
                            {
                                _outputManager.AddLogEntry("Invalid input.");
                            }
                        }
                    }
                    else
                    {
                        _outputManager.AddLogEntry($"No character exists with the name \"{chosenName}\".");
                    }
                }
                catch (Exception e)
                {
                    _outputManager.AddLogEntry(e.Message);
                }

                return chosen;
            }
        }
        else if (characters.Count == 1)
        {
            _outputManager.AddLogEntry("There is currently only one character.");
            if (characters.ElementAt(0).Equipment == null)
            {
                _outputManager.AddLogEntry($"{characters.ElementAt(0).Name} - {characters.ElementAt(0).Race} {characters.ElementAt(0).Class}");
            }
            else if (characters.ElementAt(0).Equipment.WeaponId != null && characters.ElementAt(0).Equipment.ArmorId == null)
            {
                _outputManager.AddLogEntry($"{characters.ElementAt(0).Name} - {characters.ElementAt(0).Race} {characters.ElementAt(0).Class}, Weapon: {characters.ElementAt(0).Equipment.Weapon.Name}");
            }
            else if (characters.ElementAt(0).Equipment.WeaponId == null && characters.ElementAt(0).Equipment.ArmorId != null)
            {
                _outputManager.AddLogEntry($"{characters.ElementAt(0).Name} - {characters.ElementAt(0).Race} {characters.ElementAt(0).Class}, Armor: {characters.ElementAt(0).Equipment.Armor.Name}");
            }
            else
            {
                _outputManager.AddLogEntry($"{characters.ElementAt(0).Name} - {characters.ElementAt(0).Race} {characters.ElementAt(0).Class}, Weapon: {characters.ElementAt(0).Equipment.Weapon.Name}, Armor: {characters.ElementAt(0).Equipment.Armor.Name}");
            }

            chosen = _context.Players.FirstOrDefault();
            return chosen;
        }
        else
        {
            _outputManager.AddLogEntry("There are currently no available characters to edit.\nInput 2 to create a character.");
            return null;
        }
    }

    private void EditCharacter()
    {
        // Variables
        List<Player> characters = _context.Players.ToList();
        Player chosen = new Player();

        if (characters.Count == 0)
        {
            _outputManager.AddLogEntry("There are currently no available characters to edit.\nInput 2 to create a character.");
        }
        else if (characters.Count > 0)
        {
            if (characters.Count > 1)
            {
                // Input
                for (int i = 0; i < characters.Count; ++i)
                {
                    if (characters.ElementAt(i).Equipment == null)
                    {
                        _outputManager.AddLogEntry($"{i + 1}. {characters.ElementAt(i).Name} - {characters.ElementAt(i).Race} {characters.ElementAt(i).Class}");
                    }
                    else if (characters.ElementAt(i).Equipment.WeaponId != null && characters.ElementAt(i).Equipment.ArmorId == null)
                    {
                        _outputManager.AddLogEntry($"{i + 1}. {characters.ElementAt(i).Name} - {characters.ElementAt(i).Race} {characters.ElementAt(i).Class}, Weapon: {characters.ElementAt(i).Equipment.Weapon.Name}");
                    }
                    else if (characters.ElementAt(i).Equipment.WeaponId == null && characters.ElementAt(i).Equipment.ArmorId != null)
                    {
                        _outputManager.AddLogEntry($"{i + 1}. {characters.ElementAt(i).Name} - {characters.ElementAt(i).Race} {characters.ElementAt(i).Class}, Armor: {characters.ElementAt(i).Equipment.Armor.Name}");
                    }
                    else
                    {
                        _outputManager.AddLogEntry($"{i + 1}. {characters.ElementAt(i).Name} - {characters.ElementAt(i).Race} {characters.ElementAt(i).Class}, Weapon: {characters.ElementAt(i).Equipment.Weapon.Name}, Armor: {characters.ElementAt(i).Equipment.Armor.Name}");
                    }
                }

                while (true)
                {
                    try
                    {
                        _outputManager.AddLogEntry("Select the character you want to edit.");
                        int chosenNumber = Convert.ToInt32(_outputManager.GetUserInput("Selection:"));

                        if (chosenNumber > 0 && chosenNumber <= characters.Count)
                        {
                            chosen = characters.ElementAt(chosenNumber - 1);
                            _outputManager.AddLogEntry($"You have chosen {characters.ElementAt(chosenNumber - 1).Name}.");
                            break;
                        }
                        else
                        {
                            _outputManager.AddLogEntry($"Invalid selection. Please choose between 0 and {characters.Count + 1}.");
                        }
                    }
                    catch (Exception e)
                    {
                        _outputManager.GetUserInput(e.Message);
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
            }
            else if (characters.Count == 1)
            {
                _outputManager.AddLogEntry("There is currently only one character.");
                if (characters.ElementAt(0).Equipment == null)
                {
                    _outputManager.AddLogEntry($"{characters.ElementAt(0).Name} - {characters.ElementAt(0).Race} {characters.ElementAt(0).Class}");
                }
                else if (characters.ElementAt(0).Equipment.WeaponId != null && characters.ElementAt(0).Equipment.ArmorId == null)
                {
                    _outputManager.AddLogEntry($"{characters.ElementAt(0).Name} - {characters.ElementAt(0).Race} {characters.ElementAt(0).Class}, Weapon: {characters.ElementAt(0).Equipment.Weapon.Name}");
                }
                else if (characters.ElementAt(0).Equipment.WeaponId == null && characters.ElementAt(0).Equipment.ArmorId != null)
                {
                    _outputManager.AddLogEntry($"{characters.ElementAt(0).Name} - {characters.ElementAt(0).Race} {characters.ElementAt(0).Class}, Armor: {characters.ElementAt(0).Equipment.Armor.Name}");
                }
                else
                {
                    _outputManager.AddLogEntry($"{characters.ElementAt(0).Name} - {characters.ElementAt(0).Race} {characters.ElementAt(0).Class}, Weapon: {characters.ElementAt(0).Equipment.Weapon.Name}, Armor: {characters.ElementAt(0).Equipment.Armor.Name}");
                }

                chosen = characters.ElementAt(0);
            }

            while (true)
            {
                try
                {
                    _outputManager.AddLogEntry("Choose which attribute you would like to change. Otherwise, input 4 to exit.");
                    _outputManager.AddLogEntry("1. Name");
                    _outputManager.AddLogEntry("2. Health");
                    _outputManager.AddLogEntry("3. Equipment");
                    _outputManager.AddLogEntry("4. Done");
                    var input = _outputManager.GetUserInput("Selection:");

                    switch (input)
                    {
                        case "1":
                            chosen.Name = ChangeName(chosen);
                            _playerRepository.UpdatePlayer(chosen);
                            break;
                        case "2":
                            chosen.Modifier = ChangeModifier(chosen);
                            chosen.Health = ChangeHealth(chosen);
                            _playerRepository.UpdatePlayer(chosen);
                            break;
                        case "3":
                            EquipItem(chosen);
                            _playerRepository.UpdatePlayer(chosen);
                            break;
                        case "4":
                            break;
                        default:
                            _outputManager.AddLogEntry("Invaled input. Please choose between 0 and 5.");
                            break;
                    }

                    if (input == "4")
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    _outputManager.AddLogEntry(e.Message);
                }
            }
        }
    }

    private string ChangeName(Player player)
    {
        string name = null;

        while (true)
        {
            try
            {
                _outputManager.AddLogEntry("Enter the new name of your character.");
                name = _outputManager.GetUserInput("Name:").ToString();

                if (name.IsNullOrEmpty())
                {
                    _outputManager.AddLogEntry("You cannot move forward unless you name your character.");
                }
                else
                {
                    _outputManager.AddLogEntry($"Your character\'s name is now {name}.");
                    break;
                }
            }
            catch (Exception e)
            {
                _outputManager.AddLogEntry(e.Message);
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

        return name;
    }

    private int ChangeModifier(Player player)
    {
        Random rand = new Random();
        int roll = rand.Next(-5, 11);
        return roll;
    }

    private int ChangeHealth(Player player)
    {
        // Variables
        int health = 0;
        double totalHealth = 0;

        // Selection input
        while (true)
        {
            _outputManager.AddLogEntry("Select how to input new health.");
            _outputManager.AddLogEntry("1. Manually");
            _outputManager.AddLogEntry("2. Automatically");
            var input = _outputManager.GetUserInput("Selection:");

            switch (input)
            {
                case "1":
                    break;
                case "2":
                    break;
                default:
                    _outputManager.AddLogEntry("Invaled input. Please choose 1 or 2.");
                    break;
            }

            if (input == "1")
            {
                while (true)
                {
                    try
                    {
                        health = Convert.ToInt32(_outputManager.GetUserInput("Health:"));

                        if (health > 0)
                        {
                            _outputManager.AddLogEntry($"Your character\'s health is now {health}.");
                            break;
                        }
                        else if (health <= 0)
                        {
                            _outputManager.AddLogEntry("Invaled input. Health must be greater than 0.");
                        }
                    }
                    catch (Exception e)
                    {
                        _outputManager.AddLogEntry(e.Message);
                    }
                }

                break;
            }
            else if (input == "2")
            {
                // Calculate character level
                double experience = Convert.ToDouble(player.Experience);
                int level = 0;

                if (experience < 300)
                {
                    level = 1;
                }
                else if (experience >= 300 || experience < 900)
                {
                    level = 2;
                }
                else if (experience >= 900 || experience < 2700)
                {
                    level = 3;
                }
                else if (experience >= 2700 || experience < 6500)
                {
                    level = 4;
                }
                else if (experience >= 6500 || experience < 14000)
                {
                    level = 5;
                }
                else if (experience >= 14000 || experience < 23000)
                {
                    level = 6;
                }
                else if (experience >= 23000 || experience < 34000)
                {
                    level = 7;
                }
                else if (experience >= 34000 || experience < 48000)
                {
                    level = 8;
                }
                else if (experience >= 48000 || experience < 64000)
                {
                    level = 9;
                }
                else if (experience >= 64000 || experience < 85000)
                {
                    level = 10;
                }
                else if (experience >= 85000 || experience < 100000)
                {
                    level = 11;
                }
                else if (experience >= 100000 || experience < 120000)
                {
                    level = 12;
                }
                else if (experience >= 120000 || experience < 140000)
                {
                    level = 13;
                }
                else if (experience >= 140000 || experience < 165000)
                {
                    level = 14;
                }
                else if (experience >= 165000 || experience < 195000)
                {
                    level = 15;
                }
                else if (experience >= 195000 || experience < 225000)
                {
                    level = 16;
                }
                else if (experience >= 225000 || experience < 265000)
                {
                    level = 17;
                }
                else if (experience >= 265000 || experience < 305000)
                {
                    level = 18;
                }
                else if (experience >= 305000 || experience < 355000)
                {
                    level = 19;
                }
                else if (experience >= 35500)
                {
                    level = 20;
                }

                if (player.Class == "Sorcerer" || player.Class == "Wizard")
                {
                    totalHealth = (6 + (level + 6) + (2 * level) + (2 * level * player.Modifier) - 2) / 2;
                    health = (int)Math.Round(totalHealth, MidpointRounding.AwayFromZero);
                }
                else if (player.Class == "Artificer" || player.Class == "Bard" || player.Class == "Cleric" || player.Class == "Druid" || player.Class == "Monk" || player.Class == "Rogue" || player.Class == "Vampire" || player.Class == "Warlock" || player.Class == "Werewolf")
                {
                    totalHealth = (8 + (level + 8) + (2 * level) + (2 * level * player.Modifier) - 2) / 2;
                    health = (int)Math.Round(totalHealth, MidpointRounding.AwayFromZero);
                }
                else if (player.Class == "Fighter" || player.Class == "Paladin" || player.Class == "Ranger" || player.Class == "Revenant")
                {
                    totalHealth = (10 + (level + 10) + (2 * level) + (2 * level * player.Modifier) - 2) / 2;
                    health = (int)Math.Round(totalHealth, MidpointRounding.AwayFromZero);
                }
                else if (player.Class == "Barbarain")
                {
                    totalHealth = (12 + (level + 12) + (2 * level) + (2 * level * player.Modifier) - 2) / 2;
                    health = (int)Math.Round(totalHealth, MidpointRounding.AwayFromZero);
                }

                _outputManager.AddLogEntry($"Your character\'s health is now at {health}.");
                break;
            }
        }

        // Result confirmation
        while (true)
        {
            _outputManager.AddLogEntry("Type 1 to continue.");
            var continueFromHealth = _outputManager.GetUserInput("Continue:");

            if (continueFromHealth == "1")
            {
                break;
            }
            else
            {
                _outputManager.AddLogEntry("Invalid input.");
            }
        }

        return health;
    }

    private void EquipItem(Player player)
    {
        // Variables
        List<Item> items = _context.Items.ToList();
        Item chosen = new Item();
        bool found = false;

        // Input
        while (true)
        {
            try
            {
                _outputManager.AddLogEntry("Type the name of the item you want to equip.");
                string input = _outputManager.GetUserInput("Name:").ToString();

                foreach (var item in items)
                {
                    if (item.Name.ToLower().Equals(input.ToLower()))
                    {
                        found = true;
                        chosen = item;
                    }
                }

                if (found && !input.IsNullOrEmpty())
                {
                    _playerService.EquipItemFromInventory(player, chosen);
                    break;
                }
                else if (!found && !input.IsNullOrEmpty())
                {
                    _outputManager.AddLogEntry($"There's no item by the name of \"{input}\".");
                    break;
                }
                else if (!found && input.IsNullOrEmpty())
                {
                    _outputManager.AddLogEntry("You must name the item you want.");
                }
            }
            catch (Exception e)
            {
                _outputManager.AddLogEntry(e.Message);
            }
        }

        // Result confirmation
        while (true)
        {
            _outputManager.AddLogEntry("Type 1 to continue.");
            var continueFromItem = _outputManager.GetUserInput("Continue:");

            if (continueFromItem == "1")
            {
                break;
            }
            else
            {
                _outputManager.AddLogEntry("Invalid input.");
            }
        }
    }

    private void LoadMonsters()
    {
        Random rand = new Random();
        List<Room> rooms = _context.Rooms.ToList();
        _monsters = _context.Monsters.ToList();

        foreach (Monster monster in _monsters)
        {
            int roomId = rand.Next(0, rooms.Count);
            monster.Room = rooms.ElementAt(roomId);
            rooms.ElementAt(roomId).Monsters.Add(monster);
        }
    }

    private int CalculateHealth(Player player)
    {
        int health = 0;
        double totalHealth = 0;
        int level = 0;

        // Calculate character level
        double experience = player.Experience;

        if (experience < 300)
        {
            level = 1;
        }
        else if (experience >= 300 || experience < 900)
        {
            level = 2;
        }
        else if (experience >= 900 || experience < 2700)
        {
            level = 3;
        }
        else if (experience >= 2700 || experience < 6500)
        {
            level = 4;
        }
        else if (experience >= 6500 || experience < 14000)
        {
            level = 5;
        }
        else if (experience >= 14000 || experience < 23000)
        {
            level = 6;
        }
        else if (experience >= 23000 || experience < 34000)
        {
            level = 7;
        }
        else if (experience >= 34000 || experience < 48000)
        {
            level = 8;
        }
        else if (experience >= 48000 || experience < 64000)
        {
            level = 9;
        }
        else if (experience >= 64000 || experience < 85000)
        {
            level = 10;
        }
        else if (experience >= 85000 || experience < 100000)
        {
            level = 11;
        }
        else if (experience >= 100000 || experience < 120000)
        {
            level = 12;
        }
        else if (experience >= 120000 || experience < 140000)
        {
            level = 13;
        }
        else if (experience >= 140000 || experience < 165000)
        {
            level = 14;
        }
        else if (experience >= 165000 || experience < 195000)
        {
            level = 15;
        }
        else if (experience >= 195000 || experience < 225000)
        {
            level = 16;
        }
        else if (experience >= 225000 || experience < 265000)
        {
            level = 17;
        }
        else if (experience >= 265000 || experience < 305000)
        {
            level = 18;
        }
        else if (experience >= 305000 || experience < 355000)
        {
            level = 19;
        }
        else if (experience >= 35500)
        {
            level = 20;
        }

        if (player.Class == "Sorcerer" || player.Class == "Wizard")
        {
            totalHealth = (6 + (level + 6) + (2 * level) + (2 * level * player.Modifier) - 2) / 2;
            health = (int)Math.Round(totalHealth, MidpointRounding.AwayFromZero);
        }
        else if (player.Class == "Artificer" || player.Class == "Bard" || player.Class == "Cleric" || player.Class == "Druid" || player.Class == "Monk" || player.Class == "Rogue" || player.Class == "Vampire" || player.Class == "Warlock" || player.Class == "Werewolf")
        {
            totalHealth = (8 + (level + 8) + (2 * level) + (2 * level * player.Modifier) - 2) / 2;
            health = (int)Math.Round(totalHealth, MidpointRounding.AwayFromZero);
        }
        else if (player.Class == "Fighter" || player.Class == "Paladin" || player.Class == "Ranger" || player.Class == "Revenant")
        {
            totalHealth = (10 + (level + 10) + (2 * level) + (2 * level * player.Modifier) - 2) / 2;
            health = (int)Math.Round(totalHealth, MidpointRounding.AwayFromZero);
        }
        else if (player.Class == "Barbarain")
        {
            totalHealth = (12 + (level + 12) + (2 * level) + (2 * level * player.Modifier) - 2) / 2;
            health = (int)Math.Round(totalHealth, MidpointRounding.AwayFromZero);
        }

        _outputManager.AddLogEntry($"Your character\'s health is now at {health}.");
        return health;
    }
}
