using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models.Equipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpgEntities.Repsitory
{
    public class ItemRepository
    {
        private readonly GameContext _context;
        public ItemRepository(GameContext context) { _context = context; }

        // Update an item
        public void UpdateItem(Item item)
        {
            _context.Items.Update(item);
            _context.SaveChanges();
        }
    }
}
