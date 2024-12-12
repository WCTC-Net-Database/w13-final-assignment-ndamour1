using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Characters.Monsters;

namespace ConsoleRpgEntities.Services
{
    public class MonsterService
    {
        private readonly IOutputService _outputService;

        public MonsterService(IOutputService outputService)
        {
            _outputService = outputService;
        }

        public void Attack(IMonster monster, ITargetable target)
        {
            // "Dice roll"
            Random rand = new Random();
            int roll = rand.Next(1, 7);

            // Equipment stats
            int attack = 0;

            // Monster attack stats
            if (monster.Race.Equals("Balrog"))
            {
                attack = 14;
            }
            else if (monster.Race.Equals("Fallen"))
            {
                attack = 20;
            }
            else if (monster.Race.Equals("Ghoul"))
            {
                attack = 10;
            }
            else if (monster.Race.Equals("Goblin"))
            {
                attack = 4;
            }
            else if (monster.Race.Equals("Kumiho"))
            {
                attack = 12;
            }
            else if (monster.Race.Equals("Ogre"))
            {
                attack = 8;
            }
            else if (monster.Race.Equals("Orrok"))
            {
                attack = 9;
            }
            else if (monster.Race.Equals("Rakshasa") || monster.Race.Equals("Troll"))
            {
                attack = 7;
            }
            else if (monster.Race.Equals("Uruk"))
            {
                attack = 5;
            }

            // Damage calculations
            decimal damage = 0;

            if (monster is Orrok waaagh)
            {
                if (target.Equipment.Armor != null)
                {
                    damage = ((roll + attack) * (decimal)waaagh.Waaagh) / ((target.Equipment.Armor.Defense + 100) / 100);
                }
                else
                {
                    damage = (roll + attack) * (decimal)waaagh.Waaagh;
                }
            }
            else
            {
                if (target.Equipment.Armor != null)
                {
                    damage = (roll + attack) / ((target.Equipment.Armor.Defense + 100) / 100);
                }
                else
                {
                    damage = roll + attack;
                }
            }

            // Total damage
            int totalDamage = (int)Math.Round(damage, MidpointRounding.AwayFromZero);
            int overkill = totalDamage + (target.Health - totalDamage);

            // Monster-specific attack logic
            if (monster is Goblin goblin)
            {
                _outputService.WriteLine($"{target.Name} sneaks up and attacks {target.Name}!");
            }
            else if (monster is Balrog balrog)
            {
                _outputService.WriteLine($"{target.Name} attacks {target.Name} in a fiery rage!");
            }
            else if (monster is Fallen fallen)
            {
                _outputService.WriteLine($"With Hell's fury, {target.Name} attacks {target.Name}!");
            }
            else if (monster is Kumiho kumiho)
            {
                _outputService.WriteLine($"{target.Name} sneaks up and attacks {target.Name}!");
            }
            else if (monster is Ogre ogre)
            {
                _outputService.WriteLine($"{target.Name} attacks {target.Name} with ravenous hunger!");
            }
            else if (monster is Oni oni)
            {
                _outputService.WriteLine($"{target.Name} attacks {target.Name}, letting out a deep, sinsiter laugh.");
            }
            else if (monster is Orrok orrok)
            {
                _outputService.WriteLine($"{target.Name} attacks {target.Name} with manic energy!");
            }
            else if (monster is Rakshasa rakshasa)
            {
                _outputService.WriteLine($"{target.Name} attacks {target.Name}, letting out a deep, sinsiter laugh.");
            }
            else if (monster is Troll troll)
            {
                _outputService.WriteLine($"{target.Name} charges towards and attacks {target.Name}!");
            }
            else
            {
                _outputService.WriteLine($"{target.Name} attacks {target.Name}!");
            }

            // Final results
            target.Health -= totalDamage;

            if (monster is Orrok waaaghIncrease)
            {
                waaaghIncrease.Waaagh *= 2;
            }

            if (target.Health > 0)
            {
                _outputService.WriteLine($"{target.Name} took {totalDamage} damage.\n{target.Name}'s HP is at {target.Health}.");
            }
            else
            {
                int finishThem = rand.Next(1, 3);

                if ((target.Health - totalDamage < 0) && !target.Class.Equals("Vampire") && !target.Race.Equals("Troll"))
                {
                    _outputService.WriteLine($"{target.Name} took {overkill} damage.\n{target.Name} has been defeated.");
                }
                else if ((target.Health - totalDamage < 0) && target.Class.Equals("Vampire") && !target.Race.Equals("Troll"))
                {
                    _outputService.WriteLine($"{target.Name} was staked, receiving {overkill} damage.\n{target.Name} has been defeated.");
                }
                else if ((target.Health - totalDamage < 0) && !target.Class.Equals("Vampire") && target.Race.Equals("Troll"))
                {
                    _outputService.WriteLine($"{target.Name} took {overkill} damage, obliterating their head.\n{target.Name} has been defeated.");
                }
                else if ((target.Health - totalDamage < 0) && target.Class.Equals("Vampire") && target.Race.Equals("Troll"))
                {
                    switch (finishThem)
                    {
                        case 1:
                            _outputService.WriteLine($"{target.Name} was staked, receiving {overkill} damage.\n{target.Name} has been defeated.");
                            break;
                        case 2:
                            _outputService.WriteLine($"{target.Name} took {overkill} damage, obliterating their head.\n{target.Name} has been defeated.");
                            break;
                    }
                }
                else if ((target.Health - totalDamage == 0) && !target.Class.Equals("Vampire") && !target.Race.Equals("Troll"))
                {
                    _outputService.WriteLine($"{target.Name} took {totalDamage} damage.\n{target.Name} has been defeated.");
                }
                else if ((target.Health - totalDamage == 0) && target.Class.Equals("Vampire") && !target.Race.Equals("Troll"))
                {
                    _outputService.WriteLine($"{target.Name} was staked, receiving {totalDamage} damage.\n{target.Name} has been defeated.");
                }
                else if ((target.Health - totalDamage == 0) && !target.Class.Equals("Vampire") && target.Race.Equals("Troll"))
                {
                    _outputService.WriteLine($"{target.Name} took {totalDamage} damage, obliterating their head.\n{target.Name} has been defeated.");
                }
                else if ((target.Health - totalDamage == 0) && target.Class.Equals("Vampire") && target.Race.Equals("Troll"))
                {
                    switch (finishThem)
                    {
                        case 1:
                            _outputService.WriteLine($"{target.Name} was staked, receiving {totalDamage} damage.\n{target.Name} has been defeated.");
                            break;
                        case 2:
                            _outputService.WriteLine($"{target.Name} took {totalDamage} damage, obliterating their head.\n{target.Name} has been defeated.");
                            break;
                    }
                }
            }
        }
    }
}
