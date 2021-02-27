using System;

public class ShipUpgradeLevels
{
    public int Shields { get; set; } = 0;
    public int Tank { get; set; } = 0;
    public int Speed { get; set; } = 0;

    public int this[Enums.ShipUpgradeType index]
    {
        get
        {
            switch (index)
            {
                case Enums.ShipUpgradeType.SHIELDS: return Shields;
		case Enums.ShipUpgradeType.TANK: return Tank;
		case Enums.ShipUpgradeType.SPEED: return Speed;
                default: throw new IndexOutOfRangeException($"Index {index} out of range for {this.GetType().ToString()}");
            }
        }

        set
        {
            switch (index)
            {
                case Enums.ShipUpgradeType.SHIELDS: Shields = value;
                    break;
		case Enums.ShipUpgradeType.TANK: Tank = value;
		    break;
		case Enums.ShipUpgradeType.SPEED: Speed = value;
		    break;
                default: throw new IndexOutOfRangeException($"Index {index} out of range for {this.GetType().ToString()}");
            }
        }
    }
}