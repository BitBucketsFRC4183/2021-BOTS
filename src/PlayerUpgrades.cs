using System;
using System.Collections.Generic;
using System.Diagnostics;

public class PlayerUpgrades
{
    public Dictionary<RoverUpgrades, int> RoverTech;
    public Dictionary<ShipUpgrades, int> ShipTech;

    public Dictionary<RoverUpgrades, string[]> RoverTechInfo;

    //TODO: Ship Tech is NYI
    public PlayerUpgrades()
    {
        RoverTech = new Dictionary<RoverUpgrades, int>();
        ShipTech = new Dictionary<ShipUpgrades, int>();

        RoverTechInfo = new Dictionary<RoverUpgrades, string[]>();

        RoverTech.Add(RoverUpgrades.SPEED, 0);
        RoverTech.Add(RoverUpgrades.TANK, 0);
        RoverTech.Add(RoverUpgrades.REGEN, 0);
        RoverTech.Add(RoverUpgrades.INVENTORY, 0);

        //TODO: Better names lol
        RoverTechInfo.Add(RoverUpgrades.SPEED, new[]{"Rover Speed T1", "Rover Speed T2", "Rover Speed T3"});
        RoverTechInfo.Add(RoverUpgrades.TANK, new[]{"Rover Tank T1", "Rover Tank T2", "Rover Tank T3"});
        RoverTechInfo.Add(RoverUpgrades.REGEN, new[]{"Rover Fluid Regeneration T1", "Rover Fluid Regeneration T2", "Rover Fluid Regeneration T3"});
        RoverTechInfo.Add(RoverUpgrades.INVENTORY, new[]{"Rover Inventory T1", "Rover Inventory T2", "Rover Inventory T3"});
    }

    public void UpgradeRover(RoverUpgrades u)
    {
        RoverTech[u]++;
    }

    public void UpgradeShip(ShipUpgrades u)
    {
        ShipTech[u]++;
    }
    
    public enum RoverUpgrades
    {
        SPEED,
        TANK,
        REGEN,
        INVENTORY
    }

    public enum ShipUpgrades
    {
        //TBD
    }
}