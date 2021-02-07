using Godot;
using System;

public class UpgradeMain : MarginContainer
{
    private UpgradeShip shipMenu;
    private UpgradeRover roverMenu;
    
    public override void _Ready()
    {
        this.shipMenu = GetNode<Panel>("Panel").GetNode<UpgradeShip>("ShipUpgradeMenu");
        this.roverMenu = GetNode<Panel>("Panel").GetNode<UpgradeRover>("RoverUpgradeMenu");

        this.shipMenu.Visible = true;
        this.roverMenu.Visible = false;
    }
    
    public void OnSwitchMenuPressed()
    {
        //If the Ship Menu is open, open the Rover Menu and vice versa
        bool isShipOpen = this.shipMenu.Visible;

        this.shipMenu.Visible = !isShipOpen;
        this.roverMenu.Visible = isShipOpen;
    }
}
