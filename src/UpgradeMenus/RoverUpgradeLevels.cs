using System;

public class RoverUpgradeLevels
{
    public int Speed { get; set; } = 0;
    public int Tank { get; set; } = 0;
    public int Regen { get; set; } = 0;
    public int Inventory { get; set; } = 0;
    
    public int this[Enums.RoverUpgradeType index]
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

        set
        {
            switch (index)
            {
                case Enums.RoverUpgradeType.SPEED: Speed = value;
                    break;
                case Enums.RoverUpgradeType.TANK: Tank = value;
                    break;
                case Enums.RoverUpgradeType.REGEN: Regen = value;
                    break;
                case Enums.RoverUpgradeType.INVENTORY: Inventory = value;
                    break;
                default: throw new IndexOutOfRangeException($"Index {index} out of range for {this.GetType().ToString()}");
            }
        }
    }
}