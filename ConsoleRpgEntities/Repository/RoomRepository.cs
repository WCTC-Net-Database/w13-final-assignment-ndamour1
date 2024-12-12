using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpgEntities.Repository
{
    public class RoomRepository
    {
        private readonly GameContext _context;
        public RoomRepository(GameContext context) { _context = context; }

        // Create a new room
        public void AddRoom(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
        }

        // Update a room
        public void UpdateRoom(Room room)
        {
            _context.Rooms.Update(room);
            _context.SaveChanges();
        }
    }
}