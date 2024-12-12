using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Equipments;
using ConsoleRpgEntities.Models.Rooms;

namespace ConsoleRpgEntities.Models.Characters.Monsters
{
    public class Monster : IMonster, ITargetable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public int AggressionLevel { get; set; }
        public string Race { get; set; }
        public int? Sneakiness { get; set; }
        public int? Waaagh { get; set; }

        // Foreign key
        public int? EquipmentId { get; set; }

        // Navigational properties
        public virtual string? Class { get; set; }
        public virtual ICollection<Ability>? Abilities { get; set; }
        public virtual Equipment? Equipment { get; set; }
        public virtual Room Room { get; set; }
        public virtual int? RoomId { get; set; }

        protected Monster()
        {

        }
    }
}
