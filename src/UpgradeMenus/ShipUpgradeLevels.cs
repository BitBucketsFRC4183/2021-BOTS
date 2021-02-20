using System;

public class ShipUpgradeLevels
{
    public int Shields { get; set; } = 0;
    
    public int this[Enums.ShipUpgradeType index]
    {
        get
        {
            switch (index)
            {
                case Enums.ShipUpgradeType.SHIELDS: return Shields;
                default: throw new IndexOutOfRangeException($"Index {index} out of range for {this.GetType().ToString()}");
            }
        }

        set
        {
            switch (index)
            {
                case Enums.ShipUpgradeType.SHIELDS: Shields = value;
                    break;
                default: throw new IndexOutOfRangeException($"Index {index} out of range for {this.GetType().ToString()}");
            }
        }
    }
}