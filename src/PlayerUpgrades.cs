using System;
using System.Collections.Generic;
using System.Diagnostics;

public class PlayerUpgrades
{
    public Dictionary<RoverUpgrades, int> RoverTech;
    public Dictionary<ShipUpgrades, int> ShipTech;

    public Dictionary<RoverUpgrades, string[]> RoverTechInfo;
    public Dictionary<ShipUpgrades, string[]> ShipTechInfo;

    public Dictionary<RoverUpgrades, UpgradeCost[]> RoverTechCosts;
    public Dictionary<ShipUpgrades, UpgradeCost[]> ShipTechCosts;

    //TODO: Ship Tech is NYI
    public PlayerUpgrades()
    {
        //Init Dictionaries
        RoverTech = new Dictionary<RoverUpgrades, int>();
        ShipTech = new Dictionary<ShipUpgrades, int>();

        RoverTechInfo = new Dictionary<RoverUpgrades, string[]>();
        ShipTechInfo = new Dictionary<ShipUpgrades, string[]>();

        RoverTechCosts = new Dictionary<RoverUpgrades, UpgradeCost[]>();
        ShipTechCosts = new Dictionary<ShipUpgrades, UpgradeCost[]>();
        
        //Init Upgrade Tiers
        RoverTech.Add(RoverUpgrades.SPEED, 0);
        RoverTech.Add(RoverUpgrades.TANK, 0);
        RoverTech.Add(RoverUpgrades.REGEN, 0);
        RoverTech.Add(RoverUpgrades.INVENTORY, 0);
        
        //Init Info
        //TODO: Better names lol
        RoverTechInfo.Add(RoverUpgrades.SPEED, new[]{"Rover Speed T1", "Rover Speed T2", "Rover Speed T3"});
        RoverTechInfo.Add(RoverUpgrades.TANK, new[]{"Rover Tank T1", "Rover Tank T2", "Rover Tank T3"});
        RoverTechInfo.Add(RoverUpgrades.REGEN, new[]{"Rover Fluid Regeneration T1", "Rover Fluid Regeneration T2", "Rover Fluid Regeneration T3"});
        RoverTechInfo.Add(RoverUpgrades.INVENTORY, new[]{"Rover Inventory T1", "Rover Inventory T2", "Rover Inventory T3"});
        
        //Init Costs
        //TODO: Balancing
        RoverTechCosts.Add(RoverUpgrades.SPEED, new[]{new UpgradeCost(), new UpgradeCost(), new UpgradeCost()});
        RoverTechCosts.Add(RoverUpgrades.TANK, new[]{new UpgradeCost(), new UpgradeCost(), new UpgradeCost()});
        RoverTechCosts.Add(RoverUpgrades.REGEN, new[]{new UpgradeCost(), new UpgradeCost(), new UpgradeCost()});
        RoverTechCosts.Add(RoverUpgrades.INVENTORY, new[]{new UpgradeCost(), new UpgradeCost(), new UpgradeCost()});
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
        INVENTORY,
        NULL
    }

    public enum ShipUpgrades
    {
        //TBD
        NULL
    }
}