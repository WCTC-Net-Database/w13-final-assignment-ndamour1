using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpgEntities.Repository
{
    public class AbilityRepository
    {
        private readonly GameContext _context;
        public AbilityRepository(GameContext context) { _context = context; }

        // Create a new ability
        public void AddAbility(Ability ability)
        {
            _context.Abilities.Add(ability);
            _context.SaveChanges();
        }

        // Update an ability
        public void UpdateAbility(Ability ability)
        {
            _context.Abilities.Update(ability);
            _context.SaveChanges();
        }
    }
}