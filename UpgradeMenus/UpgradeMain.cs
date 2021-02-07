using Godot;
using System;

public class UpgradeMain : MarginContainer
{
    public override void _Ready()
    {
        ShipMenu().Visible = true;
        RoverMenu().Visible = false;
    }
    
    public void OnSwitchMenuPressed()
    {
        //If the Ship Menu is open, open the Rover Menu and vice versa
        bool isShipOpen = ShipMenu().Visible;

        ShipMenu().Visible = !isShipOpen;
        RoverMenu().Visible = isShipOpen;
    }

    private UpgradeShip ShipMenu()
    {
        return GetNode<Panel>("Panel").GetNode<UpgradeShip>("ShipUpgradeMenu");
    }

    private UpgradeRover RoverMenu()
    {
        return GetNode<Panel>("Panel").GetNode<UpgradeRover>("RoverUpgradeMenu");
    }
}
