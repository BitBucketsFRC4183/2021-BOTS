using System;
using Godot;
using Godot.Collections;

public class PlayerData : Node
{
    public static PlayerData Instance;

    public InventoryResources resources = new InventoryResources();

    public RoverUpgrades roverUpgrades = new RoverUpgrades();
    public RoverUpgradeLevels roverLevels = new RoverUpgradeLevels();

    public override void _Ready()
    {
        Instance = this;
    }

    public bool CanUpgrade(Enums.RoverUpgradeType rover, Enums.ShipUpgradeType ship)
    {
        Upgrade u = GetUpgrade(rover, ship);

        foreach (Enums.GameResources r in Enum.GetValues(typeof(Enums.GameResources)))
        {
            if (u.Cost[r] != 0 && resources[r] < u.Cost[r]) return false;
        }

        return true;
    }

    public void UpgradeTech(Enums.RoverUpgradeType rover, Enums.ShipUpgradeType ship)
    {
        //Assumes CanUpgrade() returns true
        Upgrade u = GetUpgrade(rover, ship);

        if (rover != Enums.RoverUpgradeType.NULL) roverLevels[rover]++;
        
        foreach (Enums.GameResources r in Enum.GetValues(typeof(Enums.GameResources)))
        {
            resources[r] -= u.Cost[r];
        }
    }

    private Upgrade GetUpgrade(Enums.RoverUpgradeType rover, Enums.ShipUpgradeType ship)
    {
        Upgrade[] uT = rover != Enums.RoverUpgradeType.NULL ? roverUpgrades[rover] : null;
        Upgrade u = rover != Enums.RoverUpgradeType.NULL ? uT[roverLevels[rover]] : null;
        return u;
    }

}

