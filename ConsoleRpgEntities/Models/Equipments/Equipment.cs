﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleRpgEntities.Models.Equipments;

public class Equipment
{
    public int Id { get; set; }

    // SQL Server enforces cascading delete rules strictly
    // so Entity Framework will assume DeleteBehavior.Cascade for relationships
    public int? WeaponId { get; set; }  // Nullable to avoid cascade issues
    public int? ArmorId { get; set; }   // Nullable to avoid cascade issues

    // Navigation properties
    [ForeignKey("WeaponId")]
    public virtual Item Weapon { get; set; }

    [ForeignKey("ArmorId")]
    public virtual Item Armor { get; set; }

    public void EquipItem(Item item)
    {
        if (item.Type == "Weapon")
        {
            Weapon = item;
            Console.WriteLine($"You have equipped the {item.Name}.");
        }
        else if (item.Type == "Armor")
        {
            Armor = item;
            Console.WriteLine($"You have equipped the {item.Name}.");
        }
        else
        {
            Console.WriteLine("You cannot equip this item.");
        }
    }
}
