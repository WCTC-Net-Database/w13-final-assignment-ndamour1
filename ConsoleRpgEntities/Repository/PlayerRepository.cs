using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models.Characters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpgEntities.Repository
{
    public class PlayerRepository
    {
        private readonly GameContext _context;

        public PlayerRepository(GameContext context) { _context = context; }

        // Create a new player
        public void AddPlayer(Player player)
        {
            _context.Players.Add(player);
            _context.SaveChanges();
        }

        // Read (Get a player by ID)
        public Player GetPlayerById(int id)
        {
            return _context.Players
                .Include(p => p.Inventory)
                .Include(p => p.Equipment)
                .FirstOrDefault(p => p.Id == id);
        }

        // Read (Get a player by Name)
        public Player GetPlayerByName(string name)
        {
            return _context.Players
                .Include(p => p.Inventory)
                .Include(p => p.Equipment)
                .FirstOrDefault(p => p.Name == name);
        }

        // Update a player
        public void UpdatePlayer(Player player)
        {
            _context.Players.Update(player);
            _context.SaveChanges();
        }

        // Delete a player
        public void DeletePlayer(int id)
        {
            var player = GetPlayerById(id);
            if (player != null)
            {
                _context.Players.Remove(player);
                _context.SaveChanges();
            }
        }
    }
}
