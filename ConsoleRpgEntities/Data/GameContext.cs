using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Characters.Monsters;
using ConsoleRpgEntities.Models.Equipments;
using ConsoleRpgEntities.Models.Rooms;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System;

namespace ConsoleRpgEntities.Data
{
    public class GameContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Monster> Monsters { get; set; }
        public DbSet<Ability> Abilities { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public GameContext(DbContextOptions<GameContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure TPH for Character hierarchy
            modelBuilder.Entity<Player>()
                .HasDiscriminator<string>(r => r.Race)
                .HasValue("Cambion")
                .HasValue("Changeling")
                .HasValue("Demon")
                .HasValue("Draconian")
                .HasValue("Dwarf")
                .HasValue("Elf")
                .HasValue("Fairy")
                .HasValue("Garuda")
                .HasValue("Goblin")
                .HasValue("Golem")
                .HasValue("Half-Dwarf")
                .HasValue("Half-Elf")
                .HasValue("Half-Goblin")
                .HasValue("Half-Ogre")
                .HasValue("Half-Troll")
                .HasValue("Half-Uruk")
                .HasValue("Hobbit")
                .HasValue("Human")
                .HasValue("Kitsune")
                .HasValue("Lamia")
                .HasValue("Merman")
                .HasValue("Minotaur")
                .HasValue("Naga")
                .HasValue("Ogre")
                .HasValue("Orrok")
                .HasValue("Saurian")
                .HasValue("Troll")
                .HasValue("Uruk");

            modelBuilder.Entity<Player>()
                .HasDiscriminator<string>(c => c.Class)
                .HasValue<Artificer>("Artificer")
                .HasValue<Bard>("Bard")
                .HasValue<Barbarian>("Barbarian")
                .HasValue<Cleric>("Cleric")
                .HasValue<Druid>("Druid")
                .HasValue<Fighter>("Fighter")
                .HasValue<Monk>("Monk")
                .HasValue<Paladin>("Paladin")
                .HasValue<Ranger>("Ranger")
                .HasValue<Revenant>("Revenant")
                .HasValue<Rogue>("Rogue")
                .HasValue<Sorcerer>("Sorcerer")
                .HasValue<Therianthrope>("Therianthrope")
                .HasValue<Vampire>("Vampire")
                .HasValue<Warlock>("Warlock")
                .HasValue<Wizard>("Wizard");

            modelBuilder.Entity<Monster>()
                .HasDiscriminator<string>(m => m.Race)
                .HasValue<Balrog>("Balrog")
                .HasValue<Fallen>("Fallen")
                .HasValue<Ghoul>("Ghoul")
                .HasValue<Goblin>("Goblin")
                .HasValue<Kumiho>("Kumiho")
                .HasValue<Ogre>("Ogre")
                .HasValue<Oni>("Oni")
                .HasValue<Orrok>("Orrok")
                .HasValue<Rakshasa>("Rakshasa")
                .HasValue<Troll>("Troll")
                .HasValue<Uruk>("Uruk");

            // Configure TPH for Ability hierarchy
            modelBuilder.Entity<Ability>()
                .HasDiscriminator<string>(pa => pa.AbilityType)
                .HasValue<BiteAbility>("BiteAbility")
                .HasValue<BubbleAbility>("BubbleAbility")
                .HasValue<DrainAbility>("DrainAbility")
                .HasValue<FireAbility>("FireAbility")
                .HasValue<HealAbility>("HealAbility")
                .HasValue<LightningAbility>("LightningAbility")
                .HasValue<MistAbility>("MistAbility")
                .HasValue<RageAbility>("RageAbility")
                .HasValue<ShoveAbility>("ShoveAbility");

            // Configure many-to-many relationship
            modelBuilder.Entity<Player>()
                .HasMany(p => p.Abilities)
                .WithMany(a => a.Players)
                .UsingEntity(j => j.ToTable("PlayerAbilities"));

            // Call the separate configuration method to set up Equipment entity relationships
            ConfigureEquipmentRelationships(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void ConfigureEquipmentRelationships(ModelBuilder modelBuilder)
        {
            // Configuring the Equipment entity to handle relationships with Item entities (Weapon and Armor)
            // without causing multiple cascade paths in SQL Server.

            // Equipment has a nullable foreign key WeaponId, pointing to the Item entity.
            // Setting DeleteBehavior.Restrict ensures that deleting an Item (Weapon) 
            // will NOT cascade delete any Equipment rows that reference it.
            // This prevents conflicts that arise with multiple cascading paths.
            modelBuilder.Entity<Equipment>()
                .HasOne(e => e.Weapon)  // Define the relationship to the Weapon item
                .WithMany()             // Equipment doesn't need to navigate back to Item
                .HasForeignKey(e => e.WeaponId)  // Specifies the foreign key column in Equipment
                //.OnDelete(DeleteBehavior.Restrict)  // Prevents cascading deletes, avoids multiple paths
                .IsRequired(false);

            // Similar configuration for ArmorId, also pointing to the Item entity.
            // Here we are using DeleteBehavior.Restrict for the Armor foreign key relationship as well.
            // The goal is to avoid cascade paths from both WeaponId and ArmorId foreign keys.
            modelBuilder.Entity<Equipment>()
                .HasOne(e => e.Armor)  // Define the relationship to the Armor item
                .WithMany()            // No need for reverse navigation back to Equipment
                .HasForeignKey(e => e.ArmorId)  // Sets ArmorId as the foreign key in Equipment
                //.OnDelete(DeleteBehavior.Restrict)  // Prevents cascading deletes to avoid conflict
                .IsRequired(false);

            // Explanation of Why DeleteBehavior.Restrict:
            // Cascade paths occur when there are multiple relationships in one table pointing to another,
            // each with cascading delete behavior. SQL Server restricts such configurations to prevent 
            // accidental recursive deletions. Here, by setting DeleteBehavior.Restrict, deleting an Item
            // (Weapon or Armor) will simply nullify the WeaponId or ArmorId in Equipment rather than 
            // cascading a delete through multiple paths.
        }
    }
}
