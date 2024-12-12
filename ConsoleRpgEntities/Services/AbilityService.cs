using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;


namespace ConsoleRpgEntities.Services
{
    public class AbilityService
    {
        private readonly IOutputService _outputService;

        public AbilityService(IOutputService outputService)
        {
            _outputService = outputService;
        }

        public void Activate(IAbility ability, IPlayer user, ITargetable target)
        {
            // "Dice roll"
            Random rand = new Random();
            int roll = rand.Next(1, 7);

            // Defense stat
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

            // Damage calculations
            decimal damage = (roll + ability.Damage) / ((monsterDefense + 100) / 100);

            // Total damage
            int totalDamage = (int)Math.Round(damage, MidpointRounding.AwayFromZero);
            int overkill = totalDamage + (target.Health - totalDamage);

            if (ability is BiteAbility biteAbility)
            {
                // Bite ability logic
                _outputService.WriteLine($"{user.Name} bites down on {target.Name}, dealing {totalDamage} damage!");
                biteAbility.InUse = true;
                target.Health -= totalDamage;

                if (target.Health > 0)
                {
                    _outputService.WriteLine($"{target.Name}'s HP is at {target.Health}.");
                }
                else
                {
                    _outputService.WriteLine($"{target.Name} has been defeated.");
                }
            }
            else if (ability is BubbleAbility bubbleAbility)
            {
                // Bubble ability logic
                _outputService.WriteLine($"{user.Name} erects a magical dome around themself!");
                bubbleAbility.InUse = true;
            }
            else if (ability is FireAbility fireAbility)
            {
                // Fire ability logic
                _outputService.WriteLine($"{user.Name} projects fire at {target.Name}, dealing {totalDamage} damage!");
                fireAbility.InUse = true;
                target.Health -= totalDamage;

                if (target.Health > 0)
                {
                    _outputService.WriteLine($"{target.Name}'s HP is at {target.Health}.");
                }
                else
                {
                    _outputService.WriteLine($"{target.Name} has been defeated.");
                }
            }
            else if (ability is HealAbility healAbility)
            {
                // Heal ability logic
                _outputService.WriteLine($"{user.Name} heals {healAbility.Damage} HP!");
                user.Health += healAbility.Damage;
                healAbility.InUse = true;
            }
            else if (ability is LightningAbility lightningAbility)
            {
                // Lightning ability logic
                _outputService.WriteLine($"{user.Name} projects lightning at {target.Name}, dealing {totalDamage} damage!");
                lightningAbility.InUse = true;
                target.Health -= totalDamage;

                if (target.Health > 0)
                {
                    _outputService.WriteLine($"{target.Name}'s HP is at {target.Health}.");
                }
                else
                {
                    _outputService.WriteLine($"{target.Name} has been defeated.");
                }
            }
            else if (ability is MistAbility mistAbility)
            {
                // Mist ability logic
                _outputService.WriteLine($"{user.Name} turns themself into mist!");
                mistAbility.InUse = true;
            }
            else if (ability is RageAbility rageAbility)
            {
                // Rage ability logic
                _outputService.WriteLine($"{user.Name} strikes {target.Name} with immense fury, dealing {totalDamage} damage!");
                rageAbility.InUse = true;
                target.Health -= totalDamage;

                if (target.Health > 0)
                {
                    _outputService.WriteLine($"{target.Name}'s HP is at {target.Health}.");
                }
                else
                {
                    _outputService.WriteLine($"{target.Name} has been defeated.");
                }
            }
            else if (ability is ShoveAbility shoveAbility)
            {
                // Shove ability logic
                _outputService.WriteLine($"{user.Name} shoves {target.Name} back {shoveAbility.Distance} feet, dealing {totalDamage} damage!");
                shoveAbility.InUse = true;
                target.Health -= totalDamage;

                if (target.Health > 0)
                {
                    _outputService.WriteLine($"{target.Name}'s HP is at {target.Health}.");
                }
                else
                {
                    _outputService.WriteLine($"{target.Name} has been defeated.");
                }
            }
        }

        public void Deactivate(IAbility ability)
        {
            ability.InUse = false;
        }
    }
}
