using Godot;
using System;

public class UpgradeMain : MarginContainer
{
    private UpgradeShip shipMenu;
    private UpgradeRover roverMenu;
    
    public override void _Ready()
    {
        shipMenu = GetNode<Panel>("Panel").GetNode<UpgradeShip>("ShipUpgradeMenu");
        roverMenu = GetNode<Panel>("Panel").GetNode<UpgradeRover>("RoverUpgradeMenu");

        shipMenu.Visible = true;
        roverMenu.Visible = false;
        
        UpdateButton();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_focus_next")) OnSwitchMenuPressed();
    }

    private void UpdateButton()
    {
        GetNode<Panel>("Panel").GetNode<Button>("SwitchMenu").Text =
            "Show " + (shipMenu.Visible ? "Rover" : "Ship") + " Upgrades";
    }

    public void OnSwitchMenuPressed()
    {
        //If the Ship Menu is open, open the Rover Menu and vice versa
        bool isShipOpen = shipMenu.Visible;

        shipMenu.Visible = !isShipOpen;
        roverMenu.Visible = isShipOpen;
        
        UpdateButton();
    }
}
