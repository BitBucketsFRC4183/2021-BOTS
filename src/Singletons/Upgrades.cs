using System;

public class RoverUpgrades
{
    public static Upgrade[] Speed { get; } =
    {
        new Upgrade("Speed T1", new InventoryResources{Graprofium = 10}),
        new Upgrade("Speed T2", new InventoryResources{Graprofium = 50}),
        new Upgrade("Speed T3", new InventoryResources{Graprofium = 100})
    };
    public static Upgrade[] Tank { get; } =
    {
        new Upgrade("Tank T1", new InventoryResources{Graprofium = 10, Kamenium = 40}),
        new Upgrade("Tank T2", new InventoryResources{Graprofium = 50, Kamenium = 40}),
        new Upgrade("Tank T3", new InventoryResources{Graprofium = 100, Kamenium = 40})
    };
    public static Upgrade[] Regen { get; } =
    {
        new Upgrade("Fluid Regen T1", new InventoryResources{Graprofium = 10, Kamenium = 40, Wooflowium = 50}),
        new Upgrade("Fluid Regen T2", new InventoryResources{Graprofium = 50, Kamenium = 40, Wooflowium = 150}),
        new Upgrade("Fluid Regen T3", new InventoryResources{Graprofium = 100, Kamenium = 40, Wooflowium = 250})
    };
    public static Upgrade[] Inventory { get; } =
    {
        new Upgrade("Inventory T1", new InventoryResources{Graprofium = 10, Kamenium = 40}),
        new Upgrade("Inventory T2", new InventoryResources{Graprofium = 50, Kamenium = 40}),
        new Upgrade("Inventory T3", new InventoryResources{Graprofium = 100, Kamenium = 40})
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

public class ShipUpgrades
{
    public static Upgrade[] Shields { get; } =
    {
        new Upgrade("Shields T1", new InventoryResources{Graprofium = 10}),
        new Upgrade("Shields T2", new InventoryResources{Graprofium = 50, Kamenium = 100}),
        new Upgrade("Shields T3", new InventoryResources{Graprofium = 100, Kamenium = 100, Wooflowium = 250})
    };

    public static Upgrade[] Tank { get; } =
    {
        new Upgrade("Tank T1", new InventoryResources(){Graprofium = 10}),
        new Upgrade("Tank T2", new InventoryResources(){Graprofium = 50, Kamenium = 200}),
        new Upgrade("Tank T3", new InventoryResources(){Graprofium = 100, Kamenium = 200})
    };

    public static Upgrade[] Speed { get; } =
    {
        new Upgrade("Speed T1", new InventoryResources() {Graprofium = 10}),
        new Upgrade("Speed T2", new InventoryResources() {Graprofium = 50, Kamenium = 200}),
        new Upgrade("Speed T3", new InventoryResources() {Graprofium = 100, Kamenium = 200})
    };

    public Upgrade[] this[Enums.ShipUpgradeType index]
    {
        get
        {
            switch (index)
            {
                case Enums.ShipUpgradeType.SHIELDS: return Shields;
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