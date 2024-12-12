using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models.Characters.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpgEntities.Repository
{
    public class MonsterRepository
    {
        private readonly GameContext _context;
        public MonsterRepository(GameContext context) { _context = context; }

        // Update a monster
        public void UpdateMonster(Monster monster)
        {
            _context.Monsters.Update(monster);
            _context.SaveChanges();
        }
    }
}