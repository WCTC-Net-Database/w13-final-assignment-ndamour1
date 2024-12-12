using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models.Equipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpgEntities.Repository
{
    public class EquipmentRepository
    {
        private readonly GameContext _context;
        public EquipmentRepository(GameContext context) { _context = context; }

        // Update an equipment list
        public void UpdateEquipmentList(Equipment equipment)
        {
            _context.Equipments.Update(equipment);
            _context.SaveChanges();
        }
    }
}
