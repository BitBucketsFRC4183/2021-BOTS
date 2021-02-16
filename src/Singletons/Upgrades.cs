using System;

public class RoverUpgrades
{
    public static Upgrade[] Speed { get; } =
    {
        new Upgrade("Rover Speed T1"),
        new Upgrade("Rover Speed T2"),
        new Upgrade("Rover Speed T3")
    };
    public static Upgrade[] Tank { get; } =
    {
        new Upgrade("Rover Tank T1"),
        new Upgrade("Rover Tank T2"),
        new Upgrade("Rover Tank T3")
    };
    public static Upgrade[] Regen { get; } =
    {
        new Upgrade("Rover Fluid Regeneration T1"),
        new Upgrade("Rover Fluid Regeneration T2"),
        new Upgrade("Rover Fluid Regeneration T3")
    };
    public static Upgrade[] Inventory { get; } =
    {
        new Upgrade("Rover Inventory T1"),
        new Upgrade("Rover Inventory T2"),
        new Upgrade("Rover Inventory T3")
    };

    public Upgrade[] this[Enums.RoverUpgradeType index]
    {
        get
        {
            switch (index)
            {
                case Enums.RoverUpgradeType.SPEED: return Speed;
                case Enums.RoverUpgradeType.TANK: return Tank;
                case Enums.RoverUpgradeType.REGEN: return Regen;
                case Enums.RoverUpgradeType.INVENTORY: return Inventory;
                default: throw new IndexOutOfRangeException($"Index {index} out of range for {this.GetType().ToString()}");
            }
        }
    }
}

public class Upgrade
{
    public string Name { get; set; }
    public InventoryResources Cost { get; set; }
    
    public Upgrade(string name, InventoryResources cost = null)
    {
        this.Name = name;
        this.Cost = cost ?? new InventoryResources();
    }
}